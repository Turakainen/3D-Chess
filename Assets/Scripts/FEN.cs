using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class FEN {

    private string fen;
    
    public GameManager.PieceColor GetActiveColor() {
        throw new NotImplementedException();
    }

    public GameManager.Point GetEnPassantSquareBoardCoordinates() {
        string enPassantSquareAN = fen.Split(' ')[3];

        return GameManager.AlgebraicNotationToBoardCoordinates(enPassantSquareAN);
    }

    public void SetFEN(string fen) {
        if(IsValidFormat(fen)) {
            this.fen = fen;
        } else {
            throw new ArgumentException("Given parameter is not a valid FEN.");
        }
    }
    
    public bool IsValidFormat(string fen) {
        //Matches on correctly formatted FEN
        // Ex. rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
        Regex pattern = new Regex(@"^([rnbqkpRNBQKP1-8]+\/){7}([rnbqkpRNBQKP1-8]+)\s[b|w]\s(([kqKQ]{1,4})|(-))\s(-|[a-h][1-8])\s\d+\s\d+$");

        return pattern.IsMatch(fen);
    }

    //TODO: Get list of pieces from FEN
    public List<GameManager.AbstractPiece> GetPieceList() {
        if(fen != null) {
            string pieceSection = fen.Split(' ')[0];
            string[] piecesInRows = pieceSection.Split('/');
        }

        return new List<GameManager.AbstractPiece>();
    }
    

    /*
     * 

Kings:

    See if there is exactly one w_king and one b_king
    Make sure kings are separated 1 square apart

Checks:

    Non-active color is not in check
    Active color is checked less than 3 times; in case of 2 that it is never pawn+(pawn, bishop, knight), bishop+bishop, knight+knight

Pawns:

    There are no more than 8 pawns from each color
    There aren't any pawns in first or last rows
    In case of en passant square; see if it was legally created (e.g it must be on the x3 or x6 row, there must be a pawn (from the correct color) in front of it, and the en passant square and the one behind it are empty)
    Prevent having more promoted pieces than missing pawns (e.g extra_pieces = Math.max(0, num_queens-1) + Math.max(0, num_rooks-2)... and then extra_pieces <= (8-num_pawns)), also you should do special calculations for bishops, If you have two (or more) bishops from the same square color, these can only be created through pawn promotion and you should include this information to the formula above somehow
    The pawn formation is possible to reach (e.g in case of multiple pawns in a single col, there must be enough enemy pieces missing to make that formation), here are some useful rules:
        it is impossible to have more than 6 pawns in a single column (because pawns can't exist in the first and last ranks)
        the minimum number of enemy missing pieces to reach a multiple pawn in a single col B to G 2=1, 3=2, 4=4, 5=6, 6=9 ___ A and H 2=1, 3=3, 4=6, 5=10, 6=15, for example, if you see 5 pawns in A or H, the other player must be missing at least 10 pieces from his 15 captureable pieces
        if there are white pawns in a2 and a3, there can't legally be one in b2, and this idea can be further expanded to cover more possibilities

Castling:

    If the king or rooks are not in their starting position; the castling ability for that side is lost (in the case of king, both are lost)

Bishops:

    Look for bishops in the first and last rows trapped by pawns that haven't moved, for example:
        a bishop (any color) trapped behind 3 pawns
        a bishop trapped behind 2 non-enemy pawns (not by enemy pawns because we can reach that position by underpromoting pawns, however if we check the number of pawns and extra_pieces we could determine if this case is possible or not)

Non-jumpers:

    If there are non-jumpers enemy pieces in between the king and rook and there are still some pawns without moving; check if these enemy pieces could have legally gotten in there. Also, ask yourself: was the king or rook needed to move to generate that position? (if yes, we need to make sure the castling abilities reflect this)
    If all 8 pawns are still in the starting position, all the non-jumpers must not have left their initial rank (also non-jumpers enemy pieces can't possibly have entered legally), there are other similar ideas, like if the white h-pawn moved once, the rooks should still be trapped inside the pawn formation, etc.

Half/Full move Clocks:

    In case of an en passant square, the half move clock must equal to 0
    HalfMoves <= ((FullMoves-1)*2)+(if BlackToMove 1 else 0), the +1 or +0 depends on the side to move
    The HalfMoves must be x >= 0 and the FullMoves x >= 1

Other:

    Make sure the FEN contains all the parts that are needed (e.g active color, castling ability, en passant square, etc)
*/
    }
