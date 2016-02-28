using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Round : MonoBehaviour {
	
	// Partie
	public ExecGame game;

	// Temps avant le début de la phase de rythme
	public float warmUpDuration = 5.0f;

	// Temps depuis le début du round
	public float currentTime = 0.0f;

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
	
	// Séquences de boutons
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
		
		resistant = game.players[Random.Range(0, 4)];
		Debug.Log("Résistant : " + resistant.id);
		
		foreach (Player player in game.players) {
			if (player == resistant) {
				player.joypad.VibrateThrice();
			}
			else {
				player.joypad.VibrateTwice();
			}
		}
	}
	
	/**
	 * Démarrage de la phase de rythme
	 */
	void StartRhythmState() {
		
		state = RoundState.Rhythm;
		
		// Initialisation des scores et séquences
		for (int i = 0; i < 4; i++) {
			scores[i] = 0;
			sequences[i] = Sequence.MakeSequence(this, game.players[i]);
			sequences[i].transform.parent = gameObject.transform.parent;
			sequences[i].transform.SetParent(gameObject.transform);
		}

		// Initialisation de la position des séquences (A REFACTORISER !)
		sequences [0].transform.position = new Vector3 (-1, 1.5f, 0);
		sequences [1].transform.position = new Vector3 (1, 1, 0);
		sequences [2].transform.position = new Vector3 (1, -1.5f, 0);
		sequences [3].transform.position = new Vector3 (-1, -1, 0);
	}
	
	/**
	 * Démarrage de la phase de rythme
	 */
	void StartVoteState() {
		
		state = RoundState.Vote;
		
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
		currentTime += Time.deltaTime;
		
		if (state == RoundState.WarmUp) {
			
			if (currentTime > warmUpDuration) {
				StartRhythmState();
			}
		}
		
		
	}
}
