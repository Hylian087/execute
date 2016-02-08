using UnityEngine;
using System.Collections;
using System;

public class generateInputsAsked : MonoBehaviour {
	[SerializeField]
	GameObject inputAskedUp;

	[SerializeField]
	GameObject inputAskedDown;

	[SerializeField]
	GameObject inputAskedLeft;

	[SerializeField]
	GameObject inputAskedRight;

	[SerializeField]
	GameObject inputAskedA;

	[SerializeField]
	GameObject inputAskedB;

	[SerializeField]
	GameObject inputAskedX;

	[SerializeField]
	GameObject inputAskedY;

	GameObject[] InputAvailable = new GameObject[8];
	public static GameObject[] InputAsked = new GameObject[3];
	System.Random rnd = new System.Random ();
	public static int[] IdRand = new int[3];
	Vector3 posString = new Vector3();

	int[] invertedButtons = new int[8];
	public static int[] invertedAsked = new int[3];

	// Use this for initialization
	void Start () {

		InputAvailable[0] = inputAskedUp;
		InputAvailable[1] = inputAskedDown;
		InputAvailable[2] = inputAskedLeft;
		InputAvailable[3] = inputAskedRight;
		InputAvailable[4] = inputAskedA;
		InputAvailable[5] = inputAskedB;
		InputAvailable[6] = inputAskedX;
		InputAvailable[7] = inputAskedY;

		invertedButtons [0] = 1;
		invertedButtons [1] = 0;
		invertedButtons [2] = 3;
		invertedButtons [3] = 2;
		invertedButtons [4] = 7;
		invertedButtons [5] = 6;
		invertedButtons [6] = 5;
		invertedButtons [7] = 4;

		posString = this.transform.position;

		for (int i=0; i<=2; i++) {

			invertedAsked[i] = invertedButtons[IdRand[i]];
			Debug.Log (invertedAsked[i]);

			InputAsked[i] = Instantiate(InputAvailable[IdRand[i]]);
			InputAsked[i].transform.parent = this.transform;
			InputAsked[i].transform.position = new Vector3(posString.x + (float)i*0.65F, posString.y);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}


}
