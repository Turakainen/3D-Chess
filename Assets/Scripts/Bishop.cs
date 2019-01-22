using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece {

    public override void Start()
    {
        base.Start();
        base.type = GameManager.PieceType.Bishop;
    }


    public override bool IsValidMove(GameManager.Move move)
    {
        int xLength = lengthBetweenInts(move.start.x, move.destination.x);
        int yLength = lengthBetweenInts(move.start.y, move.destination.y);

        if (xLength == yLength)
        {
            return true;
        }

        return false;
    }
}