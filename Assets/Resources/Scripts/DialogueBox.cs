using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueBox : MonoBehaviour {

	DialogueParser parser;

	public string dialogue;
	public string choice1Text, choice2Text, choice3Text;
	int lineNum;
	int buttonClicked;
	int choice;

	string yourPhrase; //built up as you add selections
	Personality yourDate; //Enum for your date's personality
	int score;

	public GUIStyle customStyle;
	public GUIStyle choiceStyle;

	enum Personality {CHAV,
										COWBOY,
										GOTH,
										LAIDBACK,
										ROMANTIC,
										SHY};

	DialogueBox(int l)
	{
		lineNum = l;
	}

	// Use this for initialization
	void Start () {
		InitialiseVars();
		parser = GameObject.Find ("DialogueParser").GetComponent<DialogueParser> ();
		FindBlindDate();
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonClicked >= 0) {				
			if (lineNum == 0) {
				//Start of the game
				lineNum = 1;

				dialogue = "Select a choice below.";
				PrintPhrasesAt (lineNum);
			} else {
				//Continuing the game
				choice = lineNum + buttonClicked;

				//Save the phrase you have chosen into your speech
				yourPhrase += parser.GetPhrase(choice);
				dialogue = yourPhrase;

				ScorePhrase(choice);

				//Get the start of the next phrases that follow on from the phrase you chose.
				lineNum = parser.GetNextID (choice);

				//If there is no next phrase then end the game
				if (lineNum == -1) {
					dialogue = "Game over, you said '" + yourPhrase + "'";
					choice1Text = choice2Text = choice3Text = "";
				} else {
					//Print the next phrases you can say from your last choice
					PrintPhrasesAt (lineNum);
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
		createDialogueBox(padding, choiceWidth);
		createButtons(padding, choiceWidth);
	}
	
	void InitialiseVars()
	{
		dialogue = "";
		choice1Text = choice2Text = choice3Text = "";
		buttonClicked = -1;
		score = 0;
	}
	
	void FindBlindDate()
	{
		yourDate = (Personality)Random.Range(0,5);
		print("Your date is " + yourDate.ToString()); 
		//Load dates images and name/type
	}
	
	void PrintPhrasesAt(int lineNum) 
	{
		choice1Text = parser.GetPhrase (lineNum);
		choice2Text = parser.GetPhrase (lineNum + 1);
		choice3Text = parser.GetPhrase (lineNum + 2);
	}
	
	void ScorePhrase(int phrase)
	{
		//If your date is really impressed with this phrase, get a lot of points
		foreach (string personality in parser.GetAttracts(phrase)) 
		{
			if (personality == yourDate.ToString())
			{
				print("Your date loved that!");
				specialReaction = 10;
				break;
			}
		}
		//If your date really hates this phrase, lose a lot of points
		foreach (string personality in parser.GetOffends(phrase)) 
		{
			if (personality == yourDate.ToString())
			{
				print("Your date hated that!");
				specialReaction = -10;
				break;
			}
		}
		
		if (specialReaction == 0 ) //just to test, can remove this
			print("Your date didn't think much of that phrase");
			
		//If this phrase had no special reaction on your date, get the average rating
		score += (specialReaction == 0) ? parser.GetRating(phrase)
																			: specialReaction)
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
