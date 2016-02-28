using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Round : MonoBehaviour {
	
	public enum RoundState {
		WarmUp,
		Rhythm,
		Vote
	};
	
	// GameManager
	public GameManager gm;
	
	// État actuel de la manche
	public RoundState state;
	
	// Scores des joueurs dans la manche
	public int[] scores = new int[4];
	
	// Résistant potentiel de la manche
	public Player resistant;
	
	// Séquences de boutons
	public Sequence[] sequences = new Sequence[4];
	
	/**
	 * Démarrage
	 */
	public void Start() {
		gm = GameManager.GetInstance();
		state = RoundState.WarmUp;
		
		// Initialisation des scores et séquences
		for (int i = 0; i < 4; i++) {
			scores[i] = 0;
			sequences[i] = Sequence.MakeSequence(gm.players[i], 30.0f);
		}
	}
	
	/**
	 * Mise à jour
	 */
	public void Update() {
		if (state == RoundState.Rhythm) {
			// Mise à jour des séquences
			for (int i = 0; i < 4; i++) {
				sequences[i].Update();
			}
		}
	}
}
