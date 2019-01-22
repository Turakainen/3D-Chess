using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum PieceColor { Black, White };
    public enum PieceType { Pawn, Knight, Bishop, Queen, King,
        Rook
    }

    public List<GameObject> piecePrefabs;

    public struct Point {
        public int x, y;

        public Point(int p1, int p2) {
            x = Mathf.Clamp(p1, 0, 7);
            y = Mathf.Clamp(p2, 0, 7);
        }
    }
    public struct Move {
        public Point start, destination;

        public Move(string a1, string a2) {
            start = AlgebraicNotationToBoardCoordinates(a1);
            destination = AlgebraicNotationToBoardCoordinates(a2);
        }
        public Move(Point a1, Point a2) {
            start = a1;
            destination = a2;
        }
    }
    public struct AbstractPiece {
        public PieceColor color;
        public PieceType type;
        public Point boardCoordinates;

        public AbstractPiece(PieceColor color, PieceType type, Point boardCoordinates) {
            this.color = color;
            this.type = type;
            this.boardCoordinates = boardCoordinates;
        }
        public AbstractPiece(PieceColor color, PieceType type, string boardCoordinates) {
            this.color = color;
            this.type = type;
            this.boardCoordinates = AlgebraicNotationToBoardCoordinates(boardCoordinates);
        }
    }
    
    private Vector3[,] boardPositionArray = new Vector3[8, 8];
    private GameObject[,] pieceRefArray = new GameObject[8, 8];
    private PieceColor colorToMove = PieceColor.White;
    private UIController uiController;
    

    public void Start()
    {
        FillBoardPositionArray();
        InitStartPosition();
        
        uiController = GameObject.FindObjectOfType<Canvas>().GetComponent<UIController>();
    }

    public void Update() {
        if(uiController.MoveReadyToRetrieve()) {
            Move nextMove = uiController.RetrieveMove();
            GameObject pieceToMove = pieceRefArray[nextMove.start.x, nextMove.start.y];
            Piece scriptRef = pieceToMove.GetComponent<Piece>();
            
            if(ValidateTurn(ref scriptRef, nextMove)) {
                MovePieceAndUpdate(ref pieceToMove, nextMove);
                NextTurn();
            }
        }
    }

    private bool ValidateTurn(ref Piece scriptRef, Move nextMove) {
        bool clear = CheckForClearWay(nextMove);
        bool correctColorToMove = scriptRef.color == colorToMove ? true : false;
        bool isValidMove = scriptRef.IsValidMove(nextMove);

        return clear && correctColorToMove && isValidMove;
    }

    private bool CheckForClearWay(Move nextMove) {
        
        
        return (CheckForLinearClearWay(nextMove));
    }

    private bool CheckForDiagonalClearWay(Move nextMove) {
        throw new NotImplementedException();
    }

    private bool CheckForLinearClearWay(Move nextMove) {
        int xLength = Piece.lengthBetweenInts(nextMove.start.x, nextMove.destination.x);
        int yLength = Piece.lengthBetweenInts(nextMove.start.y, nextMove.destination.y);

        if (xLength == 0) {
            for (int i = 0; i < pieceRefArray.GetLength(0); i++) {
                if (IsBetweenRange(i, nextMove.start.y, nextMove.destination.y)) {
                    if (pieceRefArray[nextMove.start.x, i]) {
                        return false;
                    }
                }
            }
        }
        if (yLength == 0) {
            for (int i = 0; i < pieceRefArray.GetLength(1); i++) {
                if (IsBetweenRange(i, nextMove.start.x, nextMove.destination.x)) {
                    if (pieceRefArray[i, nextMove.start.y]) {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private void NextTurn() {
        colorToMove = colorToMove == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }

    private void MovePieceAndUpdate(ref GameObject pieceToMove, Move nextMove) {
        MovePiece(ref pieceToMove, nextMove);
        UpdatePieceRefArray(ref pieceToMove, nextMove);

        if(pieceToMove.GetComponent<Pawn>()) {
            pieceToMove.GetComponent<Pawn>().SetHasMovedTrue();
        }
    }

    private void MovePiece(ref GameObject pieceToMove, Move nextMove) {
        Piece piece = pieceToMove.GetComponent<Piece>();
        Vector3 pointInWorldUnits = boardPositionArray[nextMove.destination.x, nextMove.destination.y];
        piece.SetPosition(pointInWorldUnits);
    }

    private void UpdatePieceRefArray(ref GameObject pieceToMove, Move nextMove) {
        pieceRefArray[nextMove.destination.x, nextMove.destination.y] = pieceToMove;
        pieceRefArray[nextMove.start.x, nextMove.start.y] = null;
    }

    //TODO: Test function, will be deprecated when list generation from fen is implemented
    private void InitStartPosition() {
        List<AbstractPiece> pieceList = new List<AbstractPiece>() {
            new AbstractPiece(PieceColor.White, PieceType.Rook, "a1"),
            new AbstractPiece(PieceColor.White, PieceType.Knight, "b1"),
            new AbstractPiece(PieceColor.White, PieceType.Bishop, "c1"),
            new AbstractPiece(PieceColor.White, PieceType.Queen, "d1"),
            new AbstractPiece(PieceColor.White, PieceType.King, "e1"),
            new AbstractPiece(PieceColor.White, PieceType.Bishop, "f1"),
            new AbstractPiece(PieceColor.White, PieceType.Knight, "g1"),
            new AbstractPiece(PieceColor.White, PieceType.Rook, "h1"),
            new AbstractPiece(PieceColor.White, PieceType.Pawn, "a2"),
            new AbstractPiece(PieceColor.White, PieceType.Pawn, "b2"),
            new AbstractPiece(PieceColor.White, PieceType.Pawn, "c2"),
            new AbstractPiece(PieceColor.White, PieceType.Pawn, "d2"),
            new AbstractPiece(PieceColor.White, PieceType.Pawn, "e2"),
            new AbstractPiece(PieceColor.White, PieceType.Pawn, "f2"),
            new AbstractPiece(PieceColor.White, PieceType.Pawn, "g2"),
            new AbstractPiece(PieceColor.White, PieceType.Pawn, "h2"),

            new AbstractPiece(PieceColor.Black, PieceType.Rook, "a8"),
            new AbstractPiece(PieceColor.Black, PieceType.Knight, "b8"),
            new AbstractPiece(PieceColor.Black, PieceType.Bishop, "c8"),
            new AbstractPiece(PieceColor.Black, PieceType.Queen, "d8"),
            new AbstractPiece(PieceColor.Black, PieceType.King, "e8"),
            new AbstractPiece(PieceColor.Black, PieceType.Bishop, "f8"),
            new AbstractPiece(PieceColor.Black, PieceType.Knight, "g8"),
            new AbstractPiece(PieceColor.Black, PieceType.Rook, "h8"),
            new AbstractPiece(PieceColor.Black, PieceType.Pawn, "a7"),
            new AbstractPiece(PieceColor.Black, PieceType.Pawn, "b7"),
            new AbstractPiece(PieceColor.Black, PieceType.Pawn, "c7"),
            new AbstractPiece(PieceColor.Black, PieceType.Pawn, "d7"),
            new AbstractPiece(PieceColor.Black, PieceType.Pawn, "e7"),
            new AbstractPiece(PieceColor.Black, PieceType.Pawn, "f7"),
            new AbstractPiece(PieceColor.Black, PieceType.Pawn, "g7"),
            new AbstractPiece(PieceColor.Black, PieceType.Pawn, "h7"),
        };

        InstantiateAbstractPieces(pieceList);
    }

    private void InstantiateAbstractPieces(List<AbstractPiece> pieceList) {
        foreach (AbstractPiece piece in pieceList) {
            GameObject piecePrefab = piecePrefabs.Find(x => x.name.Split('_')[1] == piece.type.ToString());
            GameObject obj = Instantiate(piecePrefab) as GameObject;

            var pieceScript = obj.GetComponent<Piece>();
            pieceScript.SetMaterial(piece.color);

            obj.transform.position = boardPositionArray[piece.boardCoordinates.x, piece.boardCoordinates.y];
            pieceRefArray[piece.boardCoordinates.x, piece.boardCoordinates.y] = obj;
        }
    }

    /// <summary>
    /// Initilizes BoardPositionArray which can be used to translate Points to world position.
    /// </summary>
    private void FillBoardPositionArray()
    {
        Vector3 boardBoundsSize = gameObject.GetComponent<Renderer>().bounds.size;

        for (int i = 0; i < boardPositionArray.GetLength(0); i++)
        {
            for (int j = 0; j < boardPositionArray.GetLength(1); j++)
            {
                float yPos = boardBoundsSize.z / boardPositionArray.GetLength(0) * i;
                float xPos = boardBoundsSize.x / boardPositionArray.GetLength(1) * j;

                boardPositionArray[j, i] = new Vector3(xPos, 0, yPos);
            }
        }
    }

    public static Point AlgebraicNotationToBoardCoordinates(string enPassantSquareAN) {
        int x, y;
        Regex pattern;
        Dictionary<char, int> charValues = new Dictionary<char, int>() {
            {'a', 0},
            {'b', 1},
            {'c', 2},
            {'d', 3},
            {'e', 4},
            {'f', 5},
            {'g', 6},
            {'h', 7}
        };
        
        pattern = new Regex(@"[a-hA-H]", RegexOptions.IgnoreCase);
        string column = pattern.Match(enPassantSquareAN).Value;
        charValues.TryGetValue(column[0], out x);

        pattern = new Regex(@"[1-8]", RegexOptions.IgnoreCase);
        string row = pattern.Match(enPassantSquareAN).Value;
        Int32.TryParse(row, out y);

        return new Point(x, --y);
    }

    public static bool IsBetweenRange(int i, int a1, int a2) {
        if (a1 > a2) {
            return (i < a1 && i > a2);
        }
        return (i > a1 && i < a2);
    }
}
