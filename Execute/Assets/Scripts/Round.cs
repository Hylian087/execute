using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Round : MonoBehaviour {
	
	// Partie
	public ExecGame game;

	// Temps avant le début du round
	public int warmUpTime;

	// Différents états d'un round
	public enum RoundState {
		WarmUp,
		Rhythm,
		Vote
	};

	// État actuel de la manche
	public RoundState state;
	
	// Scores des joueurs dans la manche
	public int[] scores = new int[4];
	
	// Résistant potentiel de la manche
	public Player resistant;
	
	// Séquences de boutons (ici 4 Séquences)
	public Sequence[] sequences = new Sequence[4];

	/**
	 * Créer une manche
	 */
	public static Round MakeRound(ExecGame game) {
		GameObject go = new GameObject("RoundInstance");
		Round round = go.AddComponent<Round>();
		
		round.game = game;
		
	    return round;
	}
	
	/**
	 * Démarrage
	 */
	void Start() {
		
		state = RoundState.WarmUp;		
		// Initialisation des scores et séquences
		for (int i = 0; i < 4; i++) {
			scores[i] = 0;
			sequences[i] = Sequence.MakeSequence(game.players[i]);
			sequences[i].transform.parent = gameObject.transform.parent;
			sequences[i].transform.SetParent (gameObject.transform);
		}

		// Initialisation de la position des séquences (A REFACTORISER !)
		sequences [0].transform.position = new Vector3 (-1, 1.5f, 0);
		sequences [1].transform.position = new Vector3 (1, 1, 0);
		sequences [2].transform.position = new Vector3 (1, -1.5f, 0);
		sequences [3].transform.position = new Vector3 (-1, -1, 0);
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
		/*
		if (Time.deltaTime != warmUpTime) {
			state = RoundState.Rhythm;
		}

		if (state != RoundState.WarmUp && state == RoundState.Rhythm) {
			// Mise à jour des séquences
			for (int i = 0; i < 4; i++) {
				sequences[i].Update();
			}
		}
	*/
	}
}
