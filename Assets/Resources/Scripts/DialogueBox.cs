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
	int prevLineNum;
	int buttonClicked;

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
		buttonClicked = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonClicked >= 0) {
			//Playing the game
			if (lineNum == 0) {
				//Start of the game
				lineNum = 1;

				dialogue = "Select a choice below.";
				printPhrasesAt (lineNum);

				prevLineNum = 1;
			} else {
				//Playing the game
				dialogue = "contruction of your phrase";
				//have this work for the lineNum YOU SELECTED
				lineNum = parser.GetNextID (prevLineNum + buttonClicked);
				if (lineNum == -1) {
					//End of the game
					dialogue = "Finito!";
					choice1Text = choice2Text = choice3Text = "";
				} else {
					printPhrasesAt (lineNum);
					prevLineNum = lineNum;
				}
			}
			//Reset the button clicked
			buttonClicked = -1;
		}
	}

	void OnGUI()
	{
		float padding = Screen.width * 0.05f;
		float choiceWidth = Screen.width * 0.28f;
		dialogue = GUI.TextField (new Rect (0 + padding, //Left most position
											Screen.height/2+padding, //Top most position
											(float)Screen.width*0.9f, //width
											(float)Screen.height*0.2f //height
											), dialogue, customStyle);

		if (GUI.Button (new Rect (	padding,  //Left most position
									Screen.height - padding - 20,  //Top most position
									choiceWidth, //width
									(float)Screen.height*0.12f //height
									), choice1Text)) {
			buttonClicked = 0;
		}
		if (GUI.Button (new Rect (	-20 + (padding * 2) + choiceWidth,  //Left most position
									Screen.height - padding - 20,  //Top most position
									choiceWidth, //width
									(float)Screen.height*0.12f //height
									), choice2Text)) {
			buttonClicked = 1;
		}
		if (GUI.Button (new Rect (	Screen.width - padding - choiceWidth,  //Left most position
									Screen.height - padding - 20,  //Top most position
									choiceWidth, //width
									(float)Screen.height*0.12f //height
									), choice3Text)) {
			buttonClicked = 2;
		}
	}

	void printPhrasesAt(int lineNum) 
	{
		choice1Text = parser.GetPhrase (lineNum);
		choice2Text = parser.GetPhrase (lineNum + 1);
		choice3Text = parser.GetPhrase (lineNum + 2);
	}
}
