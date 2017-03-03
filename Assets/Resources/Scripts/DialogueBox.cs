using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueBox : MonoBehaviour {

	DialogueParser parser;

	public string dialogue;
	public string choice1Text;
	public string choice2Text;
	public string choice3Text;
	int lineNum;

	public GUIStyle customStyle;
	public GUIStyle choiceStyle;

	DialogueBox(int l)
	{
		lineNum = l;
	}

	// Use this for initialization
	void Start () {
		dialogue = "";
		choice1Text = "";
		choice2Text = "";
		choice3Text = "";
		parser = GameObject.Find ("DialogueParser").GetComponent<DialogueParser> ();
		lineNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (lineNum >= 0) {
				//Playing the game
				if (lineNum == 0)
					//Start of the game
					dialogue = "Select a choice below.";
				else
					//Playing the game
					dialogue = "contruction of your phrase";
				choice1Text = parser.GetPhrase (lineNum);
				choice2Text = parser.GetPhrase (lineNum + 1);
				choice3Text = parser.GetPhrase (lineNum + 2);
				//have this work for the lineNum YOU SELECTED
				lineNum = parser.GetNextID (lineNum);
			} else {
				//End of the game
				dialogue = "Finito!";
				choice1Text = "";
				choice2Text = "";
				choice3Text = "";
			}

		}
	}

	void OnGUI()
	{
		dialogue = GUI.TextField (new Rect (70, 300, 1000, 100), dialogue, customStyle);
		choice1Text = GUI.TextField (new Rect ( 70, 420, 300, 60), choice1Text, choiceStyle);
		choice2Text = GUI.TextField (new Rect (420, 420, 300, 60), choice2Text, choiceStyle);
		choice3Text = GUI.TextField (new Rect (770, 420, 300, 60), choice3Text, choiceStyle);
	}
}
