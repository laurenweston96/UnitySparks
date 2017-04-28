using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class DialogueParser {

	static List<DialogueLine> lines;

	struct DialogueLine {
		public int ID;
		public int nextID;
		public string phrase;
		public int rating;
		public List<string> attracts;
		public List<string> offends;

		public DialogueLine(int i, int ni, string p, int r, List<string> a, List<string> o)
		{
			ID = i;
			nextID = ni;
			phrase = p;
			rating = r;
			attracts = a;
			offends = o;
		}

	}

	// Use this for initialization
	static DialogueParser() {
		string file = "phrases.txt";

		lines = new List<DialogueLine>();
		LoadDialogue(file);
	}

	static void LoadDialogue(string filename){
		string path = "Assets/Resources/" + filename;
		string line;
		StreamReader r = new StreamReader(path);
		using(r)
		{
			do
			{
				line = r.ReadLine();
				if(line != null)
				{
					//Make a new line to insert to the list of lines
					string[] lineValues = line.Split(',');

					//Get numbers from the line for ID, nextID, rating
					int ID, nextID, rating, toDiscard;
					ID = nextID = rating = -1;

					if( !Int32.TryParse(lineValues[0], out toDiscard) |
						!Int32.TryParse(lineValues[1], out toDiscard) |
						!Int32.TryParse(lineValues[3], out toDiscard))
						Console.WriteLine("One of the following is not a number\nID:" + lineValues[0] +
							"\nNextID:" + lineValues[1] + "\nRating:" + lineValues[3]);
					else {
						ID = Int32.Parse(lineValues[0]);
						nextID = Int32.Parse(lineValues[1]);
						rating = Int32.Parse(lineValues[3]);
					}


					//Create the Attracts and offends arrays from a . separated section of the line
					List<string> lstAttracts = new List<string>();
					List<string> lstOffends = new List<string>();

					if(lineValues[4] != "")
						foreach(string attracts in lineValues[4].Split('.'))
							lstAttracts.Add(attracts);

					if(lineValues[5] != "")
						foreach(string offends in lineValues[5].Split('.'))
							lstOffends.Add(offends);


					//Create a new line object for this line of the text file. Add to the list
					DialogueLine lineEntry = new DialogueLine(ID, nextID, lineValues[2],
																rating, lstAttracts, lstOffends);
					lines.Add(lineEntry);
				}
			}
			while(line != null);

			r.Close();
		}
	}

	public static int GetID(int lineNumber)
	{
		if (lineNumber >= 0 & lineNumber < lines.Count)
			return lines [lineNumber].ID;

		return -1;
	}

	public static int GetNextID(int lineNumber)
	{
		if (lineNumber >= 0 & lineNumber < lines.Count)
			return lines [lineNumber-1].nextID;

		return -1;
	}

	public static string GetPhrase(int lineNumber)
	{
		if (lineNumber != -1 & lineNumber <= lines.Count)
			return lines [lineNumber-1].phrase;

		return "";
	}

	public static int GetRating(int lineNumber)
	{
		if (lineNumber >= 0 & lineNumber < lines.Count)
			return lines [lineNumber].rating;

		return -1;
	}

	public static List<string> GetAttracts(int lineNumber)
	{
		if (lineNumber < lines.Count)
			return lines [lineNumber].attracts;

		return null;
	}

	public static List<string> GetOffends(int lineNumber)
	{
		if (lineNumber < lines.Count)
			return lines [lineNumber].offends;

		return null;
	}
}
