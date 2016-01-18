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
	GameObject[] InputAsked = new GameObject[3];
	System.Random rnd = new System.Random ();
	int[] IdRand = new int[3];
	Vector3 posString = new Vector3();


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
		IdRand [0] = rnd.Next(7); 
		IdRand [1] = rnd.Next (7);
		IdRand [2] = rnd.Next (7);

		posString = this.transform.position;
		for (int i=0; i<=2; i++) {
			Debug.Log(i);
			InputAsked[i] = Instantiate(InputAvailable[IdRand[i]]);
			InputAsked[i].transform.position = new Vector3(posString.x + i, posString.y);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
