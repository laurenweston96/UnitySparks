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

	string yourPhrase; //built up as you add selections

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
			if (lineNum == 0) {
				//Start of the game
				lineNum = 1;

				dialogue = "Select a choice below.";
				printPhrasesAt (lineNum);

				prevLineNum = 1;
			} else {
				//Playing the game

				//Save the phrase you have chosen into your speech
				yourPhrase += parser.GetPhrase(lineNum + buttonClicked);
				dialogue = yourPhrase;

				//Get the start of the next phrases that follow on from the phrase you chose.
				lineNum = parser.GetNextID (prevLineNum + buttonClicked);

				//If there is no next phrase then end the game
				if (lineNum == -1) {
					dialogue = "Game over, you said '" + yourPhrase + "'";
					choice1Text = choice2Text = choice3Text = "";
				} else {
					//Print the next phrases you can say from your last choice
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
