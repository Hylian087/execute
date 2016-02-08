using UnityEngine;
using System.Collections;

public class JoyTest : MonoBehaviour {

	private Joypad[] joypads = new Joypad[4];

	// Use this for initialization
	void Start () {
		for (int j = 0; j < 4; j++) {
			Debug.Log (j);
			joypads[j] = new Joypad();
		}
	}
	
	// Update is called once per frame
	void Update () {

		foreach (Joypad joypad in joypads) {
			if (joypad.isDown(Joypad.A)) {
				Debug.Log("Joueur " + joypad.getID() + " appuie sur A");
			}
			else if (joypad.isInverseDown(Joypad.A)) {
				Debug.Log("Joueur " + joypad.getID() + " appuie sur l'inverse de A (Y)");
			}
		}

	}
}
