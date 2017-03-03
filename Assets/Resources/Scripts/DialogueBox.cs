using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour {

	DialogueParser parser;

	public string dialogue;
	public string choice1Text;
	public string choice2Text;
	public string choice3Text;
	int lineNum;

	public GUIStyle customStyle;
	public GUIStyle choiceStyle;

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
			dialogue = "Select a choice below. " + lineNum;
			if (lineNum > 0) {
				choice1Text = parser.GetPhrase (lineNum - 1);
				choice2Text = parser.GetPhrase (lineNum);
				choice3Text = parser.GetPhrase (lineNum + 1);
				lineNum = parser.GetNextID (lineNum);
			} else {
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
