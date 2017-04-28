using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class World {

	public static int lineNum;

	public static Personality yourDate; //Enum for your date's personality
	public static string yourPhrase; //built up as you add selections

	public enum Personality {	CHAV,
								COWBOY,
								GOTH,
								LAIDBACK,
								ROMANTIC,
								SHY};


	static World () {
		FindBlindDate();		
	}

	static void FindBlindDate()
	{
		yourDate = (Personality)UnityEngine.Random.Range(0,5);
		Debug.Log("Your date is " + yourDate.ToString()); 
		//Load dates images and name/type
	}

	public static void SavePhrase(int choice)
	{
		yourPhrase += DialogueParser.GetPhrase(choice);
	}
}
