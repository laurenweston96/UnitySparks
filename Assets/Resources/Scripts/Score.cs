using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Score {

	static Text scoreObject;
	static public int scoreValue;

	// Use this for initialization
	static Score () {
		scoreValue = 0;
		scoreObject = GameObject.Find ("Score").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	static void Update () {
		scoreObject.text = "Score: " + scoreValue;
	}

	static public void ScorePhrase(int phrase)
	{
		int specialReaction = 0;
		//If your date is really impressed with this phrase, get a lot of points
		foreach (string personality in World.parser.GetAttracts(phrase-1)) 
		{
			if (personality == World.yourDate.ToString())
			{
				Debug.Log("Your date loved that!");
				specialReaction = 10;
				break;
			}
		}
		//If your date really hates this phrase, lose a lot of points
		foreach (string personality in World.parser.GetOffends(phrase-1)) 
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
		scoreValue += (specialReaction == 0) ? World.parser.GetRating(phrase-1) : specialReaction;
	}
}
