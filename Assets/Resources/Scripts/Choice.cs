using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Choice : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}

	void OnMouseDown()
	{
		//When this button is clicked, send the name of this object (i.e. a number 0,1 or 2)
		int number;
		Int32.TryParse(gameObject.name, out number);
	}
}
