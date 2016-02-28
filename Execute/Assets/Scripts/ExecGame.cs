using UnityEngine;
using System.Collections;

public class ExecGame : MonoBehaviour {
	
	// Manche actuelle
	//Round round;

	// Tableau qui va retenir les scores des joueurs
	static float[] playerScores = new float[4];
	
	/**
	 * Démarrage
	 */
	void Start() {
		//round = GetComponent<Round> ();
	}

	public void newRound(){
		Round.MakeRound();
	}

	/**
	 * Mise à jour
	 */
	void Update() {

	}
	
}
