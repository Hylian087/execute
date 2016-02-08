using UnityEngine;
using System.Collections;

public class JoyTest : MonoBehaviour {

	private Joypad[] joypads = new Joypad[4];

	// Use this for initialization
	void Start () {
		
		// Création de 4 joypads
		for (int j = 0; j < 4; j++) {
			joypads[j] = new Joypad();
		}
		
		
		string[] inputNames = Input.GetJoystickNames();
		
		foreach (string name in inputNames) {
			Debug.Log(name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		// Mise à jour nécessaire pour le D-Pad
		Joypad.UpdateAll();
		
		// Boucle sur les manettes
		foreach (Joypad joypad in joypads) {
			//joypad.Update();
			
			// Boucle sur les boutons qui nous intéressent (sur les axes)
			foreach (string button in Joypad.AXIS_BUTTONS) {
				
				if (joypad.IsDown(button)) {
					Debug.Log("Joueur " + joypad.getID() + " appuie sur " + button);
				}
				else if (joypad.IsInverseDown(button)) {
					Debug.Log("Joueur " + joypad.getID() + " appuie sur l'inverse de " + button);
				}
				
			}
		}

	}
}
