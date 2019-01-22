using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestGameManager {

	[Test]
	public void AlgebraicNotationToBoardCoordinates() {
        GameManager.Point p1 = new GameManager.Point(0, 0);
        GameManager.Point p2 = new GameManager.Point(2, 4);
        GameManager.Point p3 = new GameManager.Point(7, 7);

        p1 = new GameManager.Point(0, 0);

        GameManager.Point p1_an = GameManager.AlgebraicNotationToBoardCoordinates("a1");
        GameManager.Point p2_an = GameManager.AlgebraicNotationToBoardCoordinates("c5");
        GameManager.Point p3_an = GameManager.AlgebraicNotationToBoardCoordinates("h8");
        
        Assert.AreEqual(p1, p1_an);
        Assert.AreEqual(p2, p2_an);
        Assert.AreEqual(p3, p3_an);
    }

    [Test]
    public void TestIsBetweenRange() {
        Assert.IsTrue(GameManager.IsBetweenRange(1, 0, 2));
        Assert.IsTrue(GameManager.IsBetweenRange(3, 2, 4));
        Assert.IsFalse(GameManager.IsBetweenRange(1, 1, 2));
        Assert.IsFalse(GameManager.IsBetweenRange(4, 2, 4));
    }
}
