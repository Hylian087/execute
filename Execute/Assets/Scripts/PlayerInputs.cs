using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputs : MonoBehaviour {

	int personalScore;
	Time rythm;
	//List<T> inputsTable; 

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Input.GetAxisRaw("Up"));
	}
}
