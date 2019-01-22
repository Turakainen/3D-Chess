using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestFEN {

    [Test]
    public void IsValidFormat() {
        FEN fenParser = new FEN();

        string fen1 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        string fen2 = "r1bqkbnr/pppp1ppp/2n5/8/2B1P3/2N5/PP3PPP/R1BQK1NR b KQkq - 2 5";
        string fen3 = "r1bqkbnr/2pp1p1p/ppn5/5Pp1/2B1P3/2N5/PP4PP/R1BQK1NR w KQkq g6 0 8";
        string fen1f = "rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        string fen2f = "r1bqkbnr/pppp1ppp/2n5/8/2B1P3/2N5/PP3PPP/R1BQK1NR KQkq - 2 5";
        string fen3f = "r1bqkbnr/2pp1p1p/ppn5/5Pp1/2B1P3/2N5/PP4PP/R1BQK1NR w KQkq 0 8";

        Assert.True(fenParser.IsValidFormat(fen1));
        Assert.True(fenParser.IsValidFormat(fen2));
        Assert.True(fenParser.IsValidFormat(fen3));
        Assert.False(fenParser.IsValidFormat(fen1f));
        Assert.False(fenParser.IsValidFormat(fen2f));
        Assert.False(fenParser.IsValidFormat(fen3f));
    }
}
