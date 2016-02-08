using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Classe représentant une manette.
 */
public class Joypad {
	
	// Liste des joypads instanciés
	private static List<Joypad> joypads = new List<Joypad>();
	
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
		
		// Changement de Joy pour la prochaine instance
		lastId++;

		// Initialisation de l'identifiant
		id = lastId;
		
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
		float axis;
		
		
		switch (button) {
			
			case "Up":
			case "Down":
			case "Left":
			case "Right":
				// Pour détecter juste une pression
				return axesButtons[button] == 1;
			break;
			
			default:
				return Input.GetButtonDown("Joy" + id + button);
			break;
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
					axis = Input.GetAxis("Joy" + id + "UpDown");
					
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
					axis = Input.GetAxis("Joy" + id + "RightLeft");
					
					// Le bouton Droite ou Gauche est pressé
					if ((button == "Left" 	&& axis == 1) ^
						(button == "Right" 	&& axis == -1)) {
						axesButtons[button]++;
					}
					else {
						axesButtons[button] = 0;
					}
				break;
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
}
