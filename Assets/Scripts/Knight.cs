using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece {

	public override void Start()
    {
        base.Start();
    }

    
    public override bool IsValidMove(GameManager.Move move)
    {
        int xLength = lengthBetweenInts(move.start.x, move.destination.x);
        int yLength = lengthBetweenInts(move.start.y, move.destination.y);

        if((xLength == 2 && yLength == 1) || (xLength == 1 && yLength == 2)) {
            return true;
        }

        return false;
    }
}
