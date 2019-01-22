using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour {

    public bool instantiateOnMouseClick = true;

    private System.Random rnd = new System.Random();

    void Start () {
        
    }

    void Update () {
		if(instantiateOnMouseClick)
        {
            InstantiateAtMouseClick();
        }
	}

    private void InstantiateAtMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Input: Left Click");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                
            }
        }
    }
}
