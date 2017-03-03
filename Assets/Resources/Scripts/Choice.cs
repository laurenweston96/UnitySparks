using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Choice : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		//send this choice
		int number;
		Int32.TryParse(gameObject.name, out number);
		//DialogueBox.lineNum = number;
	}
}
