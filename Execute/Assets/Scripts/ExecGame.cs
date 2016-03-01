using UnityEngine;
using System.Collections;

public class ExecGame : MonoBehaviour {
	
	// Joueurs
	public Player[] players = new Player[4];

	// Manche actuelle
	Round round;

	/**
	 * Créer une partie
	 */
	public static ExecGame MakeExecGame() {
		GameObject go = new GameObject("ExecGameInstance");
		ExecGame execGame = go.AddComponent<ExecGame>();
		
	    return execGame;
	}
	
	/**
	 * Initialisation
	 */
	void Start() {
		
		GameManager gm = GameManager.GetInstance();
		
		// Assignation des joueurs et manettes
		for (int i = 0; i < 4; i++) {
			players[i] = new Player(i, gm.joypads[i]);
		}

		
		players[0].voteID = "Y";
		players[1].voteID = "B";
		players[2].voteID = "A";
		players[3].voteID = "X";

		// Lancement d'un round
		NewRound();
		
	}
	
	/**
	 * Démarrer une nouvelle manche.
	 */
	public void NewRound() {
		round = Round.MakeRound(this);
	}

	/**
	 * Mise à jour
	 */
	void Update() {
	}
}
