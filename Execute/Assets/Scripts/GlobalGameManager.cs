using UnityEngine;
using System.Collections;

public class GlobalGameManager {
	
	/**
	 * Design Pattern Singleton
	 */
	
	// Unique instance de l'objet GGM
	static GlobalGameManager instance;
	
	// Impossible de construire l'objet
	private GlobalGameManager() {
		for (int j = 0; j < 4; j++) {
			joypads[j] = new Joypad();
		}
	}
	
	// Retourne l'instance de GGM
	public static GlobalGameManager GetInstance() {
		if (instance == null) {
			instance = new GlobalGameManager();
		}
		
		return instance;
	}
	
	// Joypads
	public Joypad[] joypads = new Joypad[4];
	
	// Joueurs
	public Player[] players = new Player[4];
}
