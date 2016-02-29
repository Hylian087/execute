using UnityEngine;
using System.Collections;

public class Player {
	
	// Identifiant du player (0-3)
	public int id;
	
	// Contrôleur associé au joueur
	public Joypad joypad;
	
	// Score
	public float score = 0;
	
	
	/**
	 * Constructeur
	 * @param <int> id
	 * @param <Joypad> joypad
	 */
	public Player(int id, Joypad joypad) {
		this.id = id;
		this.joypad = joypad;
	}
}
