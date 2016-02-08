using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {
	bool isControllersInit = false;
	bool isController1Known = false;
	bool isController2Known = false;
	bool isController3Known = false;
	bool isController4Known = false;

	bool isJoy1Init = false;
	bool isJoy2Init = false;
	bool isJoy3Init = false;
	bool isJoy4Init = false;

	string player1Numb = "";
	string player2Numb = "";
	string player3Numb = "";
	string player4Numb = "";
	string currentPlayerNumb = "";
	string[] playerNumb = new string[4]; 

	int playerTurn = 1;

	Text textPlayer1;
	Text textPlayer2;
	Text textPlayer3;
	Text textPlayer4;
	Text textStart;

	int playerMax;


	Vector2[] positionInputs = new Vector2[4];

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
		GameObject TextPlayer1 = GameObject.Find("player1");
		textPlayer1 = TextPlayer1.GetComponent<Text>();

		GameObject TextPlayer2 = GameObject.Find("player2");
		textPlayer2 = TextPlayer2.GetComponent<Text>();

		GameObject TextPlayer3 = GameObject.Find("player3");
		textPlayer3 = TextPlayer3.GetComponent<Text>();

		GameObject TextPlayer4 = GameObject.Find("player4");
		textPlayer4 = TextPlayer4.GetComponent<Text>();

		GameObject TextStart = GameObject.Find("pressStart");
		textStart = TextStart.GetComponent<Text>();

		textPlayer1.text = "Press A";
		textPlayer2.text = "Press A";
		textPlayer3.text = "Press A";
		textPlayer4.text = "Press A";
		textStart.text = "";

		positionInputs [0] = new Vector2 (-9, 3);
		positionInputs [1] = new Vector2 (9, 3);
		positionInputs [2] = new Vector2 (9, -3);
		positionInputs [3] = new Vector2 (-9, -3);

	}
	
	// Update is called once per frame
	void Update () {
		if (!isControllersInit) {
			if (!isController1Known) {
				if (Input.GetButtonDown ("Joy1A")) {
					playerNumb[0] = "Joy1";
					isJoy1Init = true;
					isController1Known = true;
					textPlayer1.text = "Ready";
					textStart.text = "Press Start";
					Debug.Log("Joy1");
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy2A")) {
					playerNumb[0] = "Joy2";
					isJoy2Init = true;
					isController1Known = true;
					textPlayer1.text = "Ready";
					textStart.text = "Press Start";
					Debug.Log("Joy2");
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy3A")) {
					playerNumb[0] = "Joy3";
					isJoy3Init = true;
					isController1Known = true;
					textPlayer1.text = "Ready";
					textStart.text = "Press Start";
					Debug.Log("Joy3");
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy4A")) {
					playerNumb[0] = "Joy4";
					isJoy4Init = true;
					isController1Known = true;
					textPlayer1.text = "Ready";
					textStart.text = "Press Start";
					Debug.Log("Joy4");
					//generateInputString ();
				} 
			} else if (!isController2Known) {
				if (Input.GetButtonDown ("Joy1A") && !isJoy1Init) {
					playerNumb[1] = "Joy1";
					isJoy1Init = true;
					isController2Known = true;
					textPlayer2.text = "Ready";
					Debug.Log("Joy1");
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy2A") && !isJoy2Init) {
					playerNumb[1] = "Joy2";
					isJoy2Init = true;
					isController2Known = true;
					textPlayer2.text = "Ready";
					Debug.Log("Joy2");
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy3A") && !isJoy3Init) {
					playerNumb[1] = "Joy3";
					isJoy3Init = true;
					isController2Known = true;
					textPlayer2.text = "Ready";
					Debug.Log("Joy3");
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy4A") && !isJoy4Init) {
					playerNumb[1] = "Joy4";
					isJoy4Init = true;
					isController2Known = true;
					textPlayer2.text = "Ready";
					Debug.Log("Joy4");
					//generateInputString ();
				} 
			} else if (!isController3Known) {
				if (Input.GetButtonDown ("Joy1A") && !isJoy1Init) {
					playerNumb[2] = "Joy1";
					isJoy1Init = true;
					isController3Known = true;
					textPlayer3.text = "Ready";
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy2A") && !isJoy2Init) {
					playerNumb[2] = "Joy2";
					isJoy2Init = true;
					isController3Known = true;
					textPlayer3.text = "Ready";
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy3A") && !isJoy3Init) {
					playerNumb[2] = "Joy3";
					isJoy3Init = true;
					isController3Known = true;
					textPlayer3.text = "Ready";
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy4A") && !isJoy4Init) {
					playerNumb[2] = "Joy4";
					isJoy4Init = true;
					isController3Known = true;
					textPlayer3.text = "Ready";
					//generateInputString ();
				} 
			} else if (!isController4Known) {
				if (Input.GetButtonDown ("Joy1A") && !isJoy1Init) {
					playerNumb[3] = "Joy1";
					isJoy1Init = true;
					isController4Known = true;
					textPlayer4.text = "Ready";
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy2A") && !isJoy2Init) {
					playerNumb[3] = "Joy2";
					isJoy2Init = true;
					isController4Known = true;
					textPlayer4.text = "Ready";
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy3A") && !isJoy3Init) {
					playerNumb[3] = "Joy3";
					isJoy3Init = true;
					isController4Known = true;
					textPlayer4.text = "Ready";
					//generateInputString ();
				} else if (Input.GetButtonDown ("Joy4A") && !isJoy4Init) {
					playerNumb[3] = "Joy4";
					isJoy4Init = true;
					isController4Known = true;
					textPlayer4.text = "Ready";
					//generateInputString ();
				} 
			}

			if (Input.GetButtonDown ("Joy1Start") || Input.GetButtonDown ("Joy2Start") || Input.GetButtonDown ("Joy3Start") || Input.GetButtonDown ("Joy4Start")) {
				isControllersInit = true;
				textPlayer1.text = "";
				textPlayer2.text = "";
				textPlayer3.text = "";
				textPlayer4.text = "";
				textStart.text = "";
				playerTurn = 1;
				currentPlayerNumb = playerNumb[playerTurn - 1];
				if (isController4Known)
					playerMax = 4;
				else if (isController3Known)
					playerMax = 3;
				else if (isController2Known)
					playerMax = 2;
				else 
					playerMax = 1;
				generateInputString ();
			}
		} else {

			Debug.Log ("playerTurn = " + playerTurn + ", currentPlayerNumb = " + currentPlayerNumb + ", playerMax = " +playerMax);

			if (isHPressed && (Input.GetAxisRaw (currentPlayerNumb + "H") >= -0.2 && Input.GetAxisRaw (currentPlayerNumb + "H") <= 0.2)){
				isHPressed = false;
			}

			if (isVPressed && (Input.GetAxisRaw (currentPlayerNumb + "V") >= -0.2 && Input.GetAxisRaw (currentPlayerNumb + "V") <= 0.2)){
				isVPressed = false;
			}

			if(isNewString){
				flecheFeedback.transform.position = new Vector2 (generateInputsAsked.InputAsked[0].transform.position.x, generateInputsAsked.InputAsked[0].transform.position.y + 0.6F);
				isNewString = false;
			}

			if (isHPressed || isVPressed)
				return;

			if (Input.GetAxis (currentPlayerNumb + "V") <= -0.8) {
				inputPressed = 0;
				isVPressed = true;
				testInput();
			} else if (Input.GetAxis (currentPlayerNumb + "V") >= 0.8) {
				inputPressed = 1;
				isVPressed = true;
				testInput();
			} else if (Input.GetAxis (currentPlayerNumb + "H") <= -0.8) {
				inputPressed = 2;
				isHPressed = true;
				testInput();
			} else if (Input.GetAxis (currentPlayerNumb + "H") >= 0.8) {
				inputPressed = 3;
				isHPressed = true;
				testInput();
			} else if (Input.GetButtonDown (currentPlayerNumb + "A")) {
				inputPressed = 4;
				testInput();
			} else if (Input.GetButtonDown (currentPlayerNumb + "B")) {
				inputPressed = 5;
				testInput();
			} else if (Input.GetButtonDown (currentPlayerNumb + "X")) {
				inputPressed = 6;
				testInput();
			} else if (Input.GetButtonDown (currentPlayerNumb + "Y")) {
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
		askedInputString.transform.position = positionInputs [playerTurn - 1];
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
				stateTurn = 0;
				if(playerTurn < playerMax)
					playerTurn++;
				else
					playerTurn = 1;
				currentPlayerNumb = playerNumb[playerTurn - 1];
				generateInputString();
			}
			flecheFeedback.transform.position = new Vector2 (generateInputsAsked.InputAsked[stateTurn].transform.position.x, generateInputsAsked.InputAsked[stateTurn].transform.position.y + 0.6F);
			inputPressed = -1;
		} else {
			Debug.Log ("Faux !");
			stateTurn = 0;
			if(playerTurn < playerMax)
				playerTurn++;
			else
				playerTurn = 1;
			currentPlayerNumb = playerNumb[playerTurn - 1];
			generateInputString ();
		}
	}


}
