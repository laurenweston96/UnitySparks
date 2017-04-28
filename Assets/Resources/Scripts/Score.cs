using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Score {

	static GameObject scoreObject;
	static public int scoreValue;

	// Use this for initialization
	static Score () {
		scoreValue = 0;
		scoreObject = GameObject.Find ("ScoreNumber");
		Debug.Log (scoreObject.GetComponent<Transform> ().position);
		scoreObject.GetComponent<Transform> ().position = new Vector3 (100.0f, 500.0f, 0);
		//Debug.Log(UnityEngine.Screen.height);
		//Debug.Log(UnityEngine.Screen.width);
	}

	static public void ScorePhrase(int phrase)
	{
		//Used to initialise the score
		if (phrase == -1)
			scoreObject.GetComponent<Text> ().text = "Score: ";
		
		int specialReaction = 0;
		//If your date is really impressed with this phrase, get a lot of points
		foreach (string personality in DialogueParser.GetAttracts(phrase-1)) 
		{
			if (personality == World.yourDate.ToString())
			{
				Debug.Log("Your date loved that!");
				specialReaction = 10;
				break;
			}
		}
		//If your date really hates this phrase, lose a lot of points
		foreach (string personality in DialogueParser.GetOffends(phrase-1)) 
		{
			if (personality == World.yourDate.ToString())
			{
				Debug.Log("Your date hated that!");
				specialReaction = -10;
				break;
			}
		}

		if (specialReaction == 0 ) //just to test, can remove this
			Debug.Log("Your date didn't mind that phrase");

		//If this phrase had no special reaction on your date, get the average rating
		//Get the value shown on the screen, and add to it a special rating or the standard if
		//this phrase isn't considered special for your date

		Debug.Log(scoreObject.GetComponent<Text> ().text.Split (':') [1]);
		int.TryParse (scoreObject.GetComponent<Text> ().text.Split (':') [1], out scoreValue);
		scoreValue += (specialReaction == 0) ? DialogueParser.GetRating(phrase-1) : specialReaction;
		UpdateScore (scoreValue);
	}

	public static void UpdateScore(int score)
	{
		scoreObject.GetComponent<Text>().text = "Score: " + score;

	}

	public static void UpdateScore()
	{
		scoreObject.GetComponent<Text>().text = "Score: ";

	}
}
