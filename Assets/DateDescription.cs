using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateDescription : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (this.GetComponent<Transform> ().position);
		this.GetComponent<Transform> ().position = new Vector3 (300.0f, 500.0f, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
