using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueBox : MonoBehaviour {

	public string dialogue;
	public string choice1Text, choice2Text, choice3Text;
	int buttonClicked;
	int choice;

	public GUIStyle customStyle;
	public GUIStyle choiceStyle;


	// Use this for initialization
	void Start () {
		InitialiseVars();
	}

	// Update is called once per frame
	void Update () {
		if (buttonClicked >= 0) {				
			if (World.lineNum == 0) {
				Debug.Log ("Setting up the game");
				//Start of the game
				World.lineNum = 1;

				dialogue = "Select a choice below.";
				ShowNextPhrases();
				Score.UpdateScore ();
			} else {
				Debug.Log ("Playing the game");
				//Continuing the game
				choice = World.lineNum + buttonClicked;

				//Save the phrase you have chosen into your speech
				World.SavePhrase(choice);

				dialogue = World.yourPhrase;

				Score.ScorePhrase(choice);

				//Get the start of the next phrases that follow on from the phrase you chose.
				World.lineNum = DialogueParser.GetNextID (choice);
				Debug.Log ("Got the next line");
				//If there is no next phrase then end the game
				if (World.lineNum == -1) {
					dialogue = "Game over, you said '" + World.yourPhrase + "'";
					choice1Text = choice2Text = choice3Text = "";
				} else {
					//Print the next phrases you can say from your last choice
					ShowNextPhrases();
				}
			}
			//Reset the button clicked
			buttonClicked = -1;
		}
	}

	void InitialiseVars()
	{
		dialogue = "";
		choice1Text = choice2Text = choice3Text = "";
		buttonClicked = -1;
	}

	void OnGUI()
	{
		float padding = Screen.width * 0.05f;
		float choiceWidth = Screen.width * 0.28f;
		createDialogueBox(padding, choiceWidth);
		createButtons(padding, choiceWidth);
	}

	void ShowNextPhrases() 
	{
		choice1Text = DialogueParser.GetPhrase (World.lineNum);
		choice2Text = DialogueParser.GetPhrase (World.lineNum + 1);
		choice3Text = DialogueParser.GetPhrase (World.lineNum + 2);
	}
	
	//Create the box to show the text
	void createDialogueBox(float padding, float choiceWidth)
	{
		dialogue = GUI.TextField (new Rect (0 + padding, //Left most position
											Screen.height/2+padding, //Top most position
											(float)Screen.width*0.9f, //width
											(float)Screen.height*0.2f //height
											), dialogue, customStyle);
	}		
	
	//The three choices of phrases you get
	void createButtons(float padding, float choiceWidth)
	{
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
}
