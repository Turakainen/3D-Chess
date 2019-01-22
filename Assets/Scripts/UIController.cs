using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UI = UnityEngine.UI;

public class UIController : MonoBehaviour {

    public UI.InputField startSquare;
    public UI.InputField endSquare;

    public string start, end;
    
    public void OnSubmit() {
        if(IsValidSquare(startSquare.text) && IsValidSquare(endSquare.text)) {
            start = startSquare.text;
            end = endSquare.text;
            startSquare.text = "";
            endSquare.text = "";
        }
    }

    public GameManager.Move RetrieveMove() {
        string a1 = start;
        string a2 = end;
        start = null;
        end = null;

        return new GameManager.Move(a1, a2);
    }

    public bool MoveReadyToRetrieve() {
        if (IsValidSquare(start) && IsValidSquare(end))
            return true;
        return false;
    }

    protected bool IsValidSquare(string input) {
        if (input == null) return false;

        Regex pattern = new Regex(@"^[A-Ha-h][1-8]$");
        return pattern.IsMatch(input);
    }
}
