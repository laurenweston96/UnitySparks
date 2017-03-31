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
		lineNum = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (lineNum > 0) {
				//Playing the game
				if (lineNum == 1)
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
				print("Going to " + lineNum);
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
			choice1Text = "poobert";
		}
		if (GUI.Button (new Rect (	-20 + (padding * 2) + choiceWidth,  //Left most position
									Screen.height - padding - 20,  //Top most position
									choiceWidth, //width
									(float)Screen.height*0.12f //height
									), choice2Text)) {
			choice1Text = "poobert";
		}
		if (GUI.Button (new Rect (	Screen.width - padding - choiceWidth,  //Left most position
									Screen.height - padding - 20,  //Top most position
									choiceWidth, //width
									(float)Screen.height*0.12f //height
									), choice3Text)) {
			choice1Text = "poobert";
		}
	}
}
