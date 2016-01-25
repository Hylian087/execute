using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	bool isControllerKnown = false;
	string playerNumb = "";

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
			} else if (Input.GetButtonDown ("Joy2A")) {
				playerNumb = "Joy2";
				isControllerKnown = true;
			} else if (Input.GetButtonDown ("Joy3A")) {
				playerNumb = "Joy3";
				isControllerKnown = true;
			} else if (Input.GetButtonDown ("Joy4A")) {
				playerNumb = "Joy4";
				isControllerKnown = true;
			} else {
				Debug.Log ("Press A");
			}
		}
	}
}
