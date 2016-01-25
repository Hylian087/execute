using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {
	bool isControllerKnown = false;
	string playerNumb = "";
	GameObject askedInputString;
	public GameObject inputString;
	public GameObject fleche;
	GameObject flecheFeedback;
	int inputPressed = -1;
	int stateTurn = 0;
	System.Random rnd = new System.Random ();
	bool isHPressed = false;
	bool isVPressed = false;
	bool isNewString = false;



	// Use this for initialization
	void Start () {
		Debug.Log ("Press A");

	}
	
	// Update is called once per frame
	void Update () {

		if (!isControllerKnown) {
			if (Input.GetButtonDown ("Joy1A")) {
				playerNumb = "Joy1";
				isControllerKnown = true;
				generateInputString ();
			} else if (Input.GetButtonDown ("Joy2A")) {
				playerNumb = "Joy2";
				isControllerKnown = true;
				generateInputString ();
			} else if (Input.GetButtonDown ("Joy3A")) {
				playerNumb = "Joy3";
				isControllerKnown = true;
				generateInputString ();
			} else if (Input.GetButtonDown ("Joy4A")) {
				playerNumb = "Joy4";
				isControllerKnown = true;
				generateInputString ();
			}
		} else {
			if (isHPressed && (Input.GetAxisRaw (playerNumb + "H") >= -0.2 && Input.GetAxisRaw (playerNumb + "H") <= 0.2)){
				isHPressed = false;
			}

			if (isVPressed && (Input.GetAxisRaw (playerNumb + "V") >= -0.2 && Input.GetAxisRaw (playerNumb + "V") <= 0.2)){
				isVPressed = false;
			}

			if(isNewString){
				flecheFeedback.transform.position = new Vector2 (generateInputsAsked.InputAsked[0].transform.position.x, generateInputsAsked.InputAsked[0].transform.position.y + 0.6F);
				isNewString = false;
			}

			if (isHPressed || isVPressed)
				return;

			if (Input.GetAxis (playerNumb + "V") <= -0.8) {
				inputPressed = 0;
				isVPressed = true;
				testInput();
			} else if (Input.GetAxis (playerNumb + "V") >= 0.8) {
				inputPressed = 1;
				isVPressed = true;
				testInput();
			} else if (Input.GetAxis (playerNumb + "H") <= -0.8) {
				inputPressed = 2;
				isHPressed = true;
				testInput();
			} else if (Input.GetAxis (playerNumb + "H") >= 0.8) {
				inputPressed = 3;
				isHPressed = true;
				testInput();
			} else if (Input.GetButtonDown (playerNumb + "A")) {
				inputPressed = 4;
				testInput();
			} else if (Input.GetButtonDown (playerNumb + "B")) {
				inputPressed = 5;
				testInput();
			} else if (Input.GetButtonDown (playerNumb + "X")) {
				inputPressed = 6;
				testInput();
			} else if (Input.GetButtonDown (playerNumb + "Y")) {
				inputPressed = 7;
				testInput();
			}
		}
	}

	void generateInputString(){
		if (askedInputString != null) {
			Destroy (askedInputString);
		}
		generateInputsAsked.IdRand [0] = rnd.Next(7); 
		generateInputsAsked.IdRand [1] = rnd.Next (7);
		generateInputsAsked.IdRand [2] = rnd.Next (7);
		askedInputString = Instantiate(inputString);
		if (flecheFeedback == null) {
			flecheFeedback = Instantiate (fleche);
		}
		isNewString = true;
	}

	void testInput(){
		if (inputPressed == -1)
			return;
		Debug.Log (inputPressed);
		if (inputPressed == generateInputsAsked.IdRand [stateTurn]) {
			stateTurn++;
			if(stateTurn == 3){
				Debug.Log ("Correct !");
				generateInputString();
				stateTurn = 0;
			}
			flecheFeedback.transform.position = new Vector2 (generateInputsAsked.InputAsked[stateTurn].transform.position.x, generateInputsAsked.InputAsked[stateTurn].transform.position.y + 0.6F);
			inputPressed = -1;
		} else {
			Debug.Log ("Faux !");
			stateTurn = 0;
			generateInputString ();
		}
	}


}
