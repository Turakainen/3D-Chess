using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestUIController : UIController {
    
	[Test]
	public void TestIsValidSquare() {
        Assert.IsTrue(IsValidSquare("a1"));
        Assert.IsTrue(IsValidSquare("a8"));
        Assert.IsTrue(IsValidSquare("h1"));
        Assert.IsTrue(IsValidSquare("h8"));

        Assert.IsFalse(IsValidSquare("a0"));
        Assert.IsFalse(IsValidSquare("b9"));
        Assert.IsFalse(IsValidSquare("j1"));
        Assert.IsFalse(IsValidSquare("e"));
        Assert.IsFalse(IsValidSquare(""));
    }
}
