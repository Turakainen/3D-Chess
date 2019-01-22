using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {

    public bool canCaptureLeft = false;
    public bool canCaptureRight = false;

    private enum MoveDirection { Up, Down };
    private bool hasMoved = false;

    public override void Start()
    {
        base.Start();
        base.type = GameManager.PieceType.Pawn;
    }

    public override bool IsValidMove(GameManager.Move move)
    {
        int xLength = lengthBetweenInts(move.start.x, move.destination.x);
        int moveDirectionConstant = base.color == GameManager.PieceColor.White ? -1 : 1;

        if (xLength == 0) {
            if(move.start.y == move.destination.y + moveDirectionConstant) {
                return true;
            }
            else if (!hasMoved && 
              move.start.y == move.destination.y + 2 * moveDirectionConstant) {
                return true;
            }
        }

        return false;
    }

    public void SetHasMovedTrue() {
        hasMoved = true;
    }
}

