using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	/**
	 * Design Pattern Singleton
	 */

	// instance du GameManager; static afin que d'autres scripts puisse y accéder
	public static GameManager instance = null;

	// reference à ExecGame, qui va mettre en place tout la partie
	private ExecGame partieExecute;

	// Joypads
	public static Joypad[] joypads = new Joypad[4];
	
	// Joueurs
	public static Player[] players = new Player[4];

	// Au démarrage du jeu
	void Awake(){
		// si instance de GameManager n'existe pas
		if (instance == null) {
			// instanciation du GameManager
			instance = this;
		} else if (instance != this) { // si l'instance existe déjà
			Destroy(gameObject); // destruction du GameManager	
		}

		// Initialisation d'une partie
		partieExecute = GetComponent<ExecGame> ();
		InitGame ();
	}


	// Initialisation d'une partie de jeu
	void InitGame(){
		// Assignation des joueurs et manettes
		for (int j = 0; j < 4; j++) {
			joypads[j] = new Joypad();
			players[j] = new Player(j, joypads[j]);
		}
		// Lancement de la partie
		partieExecute.newRound ();
	}

	void Update(){

		//Update nécessaire pour le fonctionnement des Joypad
		Joypad.UpdateAll();
	}

}
