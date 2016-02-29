using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

/**
 * Classe représentant une manette.
 */
public class Joypad {
	
	// Liste des joypads instanciés
	private static List<Joypad> joypads = new List<Joypad>();
	
	// Temps de vibration restant
	private int vibrationType;
	private float vibrationTime = 2.0f;
	private float vibrationDuration = 2.0f;
	private float vibrationStrength = 0.3f;
	
	// Liste des boutons des axes (const)
	public static string[] AXIS_BUTTONS = new string[] {"Up", "Down", "Left", "Right", "A", "B", "X", "Y"};
	
	// Tableau des touches inversées
	private Dictionary<string, string> inverses;
    
	// Identifiant de la dernière instance
	private static int lastId = 0;
	
	// Identifiant de l'instance actuelle
	private int id;
	
	// Axes à traiter comme des boutons (D-Pad)
	private Dictionary<string, int> axesButtons;

	/**
	 * Initialisation de l'objet
	 */
	public Joypad() {
		
		// Tableau associatif des touches inverses
		inverses = new Dictionary<string, string>();
		
		inverses.Add("Up", 		"Down");
		inverses.Add("Right", 	"Left");
		inverses.Add("Left", 	"Right");
		inverses.Add("Down", 	"Up");
		
		inverses.Add("A", 		"Y");
		inverses.Add("B", 		"X");
		inverses.Add("X", 		"B");
		inverses.Add("Y", 		"A");
		
		// Initialisation des axes à traiter comme des boutons (D-Pad)
		axesButtons = new Dictionary<string, int>();
		
		axesButtons.Add("Up", 		0);
		axesButtons.Add("Down", 	0);
		axesButtons.Add("Left", 	0);
		axesButtons.Add("Right", 	0);
		
		// Initialisation de l'identifiant
		id = lastId;
		
		Debug.Log ("manette "+ lastId);

		// Changement de Joy pour la prochaine instance
		lastId++;

		// Ajout de ce joypad à la liste des joypads
		joypads.Add(this);
	}

	/**
	 * Tester si un bouton est appuyé
	 * 
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @return <bool>
	 */
	public bool IsDown(string button) {
		switch (button) {
			
			case "Up":
			case "Down":
			case "Left":
			case "Right":
				// Pour détecter juste une pression
				return axesButtons[button] == 1;
			
			default:
				return Input.GetButtonDown("Joy" + (id+1) + button);
		}
	}


	/**
	 * Tester si l'inverse d'un bouton est appuyé
	 * (ne fonctionne que pour les touches directionnelles et ABXY)
	 * 
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @return <bool>
	 */
	public bool IsInverseDown(string button) {
		return IsDown(inverses[button]);
	}

	/**
	 * Tester si un bouton est l'inverse d'un autre
	 * (ne fonctionne que pour les touches directionnelles et ABXY)
	 * 
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @return <bool>
	 */
	public bool IsInverse(string button1, string button2) {
		return button2 == inverses[button1];
	}

	/**
	 * Retourne l'ID du joypad
	 * 
	 * @return <int>
	 */
	public int getID() {
		return id;
	}
	
	
	/**
	 * Mise à jour
	 */
	public void Update() {
		// Met à jour le tableau des axes utilisées
		List<string> buttons = new List<string> (axesButtons.Keys);
		
		foreach (string button in buttons) {
			float axis;
			
			switch (button) {
				case "Up":
				case "Down":
					axis = Input.GetAxis("Joy" + (id+1) + "UpDown");
					
					// Le bouton Haut ou Bas est pressé
					if ((button == "Up" 	&& axis == 1) ^
						(button == "Down" 	&& axis == -1)) {
						axesButtons[button]++;
					}
					else {
						axesButtons[button] = 0;
					}
					
				break;
					
				case "Left":
				case "Right":
					axis = Input.GetAxis("Joy" + (id+1) + "RightLeft");
					
					// Le bouton Droite ou Gauche est pressé
					if ((button == "Left" 	&& axis == -1) ^
						(button == "Right" 	&& axis == 1)) {
						axesButtons[button]++;
					}
					else {
						axesButtons[button] = 0;
					}
				break;
			}
		}
		
		if (vibrationTime < vibrationDuration) {
			
			PlayerIndex pid = (PlayerIndex) id;
			
			float activate = 1.0f - Mathf.Floor((vibrationTime / vibrationDuration) / (1.0f / (vibrationType * 2.0f - 1.0f))) % 2;
			
			GamePad.SetVibration(pid, activate * vibrationStrength, activate * vibrationStrength);
			
			vibrationTime += Time.deltaTime;
			
			if (vibrationTime >= vibrationDuration) {
				GamePad.SetVibration(pid, 0.0f, 0.0f);
			}
		}
	}
	
	
	/**
	 * Mise à jour des toutes les instances de Joypad
	 */
	public static void UpdateAll() {
		// Met à jour le tableau des axes utilisées
		foreach (Joypad joypad in joypads) {
			joypad.Update();
		}
	}
	
	/**
	 * Retourne un bouton au hasard
	 * @return <string>
	 */
	public static string GetRandomButton() {
		return AXIS_BUTTONS[Random.Range(0, AXIS_BUTTONS.Length)];
	}
	
	/**
	 * Faire vibrer la manette deux fois
	 * @param <float> 
	 */
	public void VibrateTwice(float vibrationValue = 0.5f) {
		vibrationType = 2;
		vibrationTime = 0.0f;
		vibrationDuration = 2.0f;
	}
	
	/**
	 * Faire vibrer la manette trois fois
	 * @param <float> 
	 */
	public void VibrateThrice(float vibrationValue = 0.5f) {
		vibrationType = 3;
		vibrationTime = 0.0f;
		vibrationDuration = 2.0f;
	}
}
