using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Piece : MonoBehaviour {

    public GameManager.PieceColor color;
    public GameManager.PieceType type;
    public Material blackMat;
    public Material whiteMat;

    public virtual void Start() {

    }

    public virtual bool IsValidMove(GameManager.Move move) {
        return false;
    }
    
    public void SetMaterial(GameManager.PieceColor color)
    {
        this.color = color;
        Renderer rend = GetComponent<Renderer>();
        rend.material = (color == GameManager.PieceColor.White) ? whiteMat : blackMat;
    }

    public void SetPosition(Vector3 vector) {
        transform.position = vector;
    }

    public GameManager.PieceType GetPieceType() {
        return type;
    }

    public static int lengthBetweenInts(int a1, int a2)
    {
        return Math.Abs(a1 - a2);
    }
}
