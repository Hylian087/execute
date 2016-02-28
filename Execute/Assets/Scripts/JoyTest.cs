using UnityEngine;
using System.Collections;

public class JoyTest : MonoBehaviour {

	private GameManager gm;

	// Use this for initialization
	void Start () {
		
		gm = GameManager.GetInstance();
			
		string[] inputNames = Input.GetJoystickNames();
		
		foreach (string name in inputNames) {
			Debug.Log(name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		Joypad[] joypads = gm.joypads;
		
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
