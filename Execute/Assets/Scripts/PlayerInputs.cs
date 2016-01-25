using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputs : MonoBehaviour {

	int personalScore;
	Time rythm;
	string[] p1Inputs;
	int randomNumber;
	string randomSelectedInput;
	string[] p1Selected = new string[3];
	int maxInputs = 3;

	void threeList(){
		p1Inputs = new string[]{"Joy1A", "Joy1B", "Joy1X", "Joy1Y"};

		for(int i = 0; i < maxInputs; i++){
			randomNumber = Random.Range (0,4);
			//Debug.Log (randomNumber);
			p1Selected[i] = p1Inputs[randomNumber];
			//Debug.Log (p1Selected[i]);
		}
	}

	// Use this for initialization
	void Start () {
		threeList ();
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetButtonDown("Joy1A")){
			//Debug.Log ("Joy1A");
		}

	}
}
