using UnityEngine;
using System.Collections;

public class GameManager {
	
	/**
	 * Design Pattern Singleton
	 */
	
	// Unique instance de l'objet GM
	static GameManager instance;
	
	// Impossible de construire l'objet
	private GameManager() {
		for (int j = 0; j < 4; j++) {
			joypads[j] = new Joypad();
			players[j] = new Player(j, joypads[j]);
		}
	}
	
	// Retourne l'instance de GM
	public static GameManager GetInstance() {
		if (instance == null) {
			instance = new GameManager();
		}
		
		return instance;
	}
	
	// Joypads
	public Joypad[] joypads = new Joypad[4];
	
	// Joueurs
	public Player[] players = new Player[4];
}
