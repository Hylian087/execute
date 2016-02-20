using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Round : MonoBehaviour {
	
	// GameManager
	protected GameManager gm;
	
	// Scores des joueurs dans la manche
	protected int[] scores = new int[4];
	
	// Résistant potentiel de la manche
	protected Player resistant;
	
	// Longueur des séquences
	protected int sequencesLength = 3;
	
	// Séquences de boutons
	protected Sequence[] sequences = new Sequence[4];
	
	
	/**
	 * Constructeur
	 */
	public void Start() {
		this.gm = GameManager.GetInstance();
		
		// Initialisation des scores et séquences
		for (int i = 0; i < 4; i++) {
			scores[i] = 0;
			sequences[i] = new Sequence();
		}
	}
	
	
	/**
	 * Mise à jour
	 */
	public void Update() {
		// Mise à jour des séquences
		// for (int i = 0; i < 4; i++) {
		for (int i = 0; i < 1; i++) {
			sequences[i].Update();
		}
	}
}
