using UnityEngine;
using System.Collections;

public class Player {
	
	// Identifiant du player (0-3)
	private int _id;
	public int Id {
		get { return _id; }
		set {}
	}
	
	// Contrôleur associé au joueur
	protected Joypad joypad;
	
	// Score
	protected int score = 0;
	
	
	/**
	 * Constructeur
	 * @param <int> id
	 * @param <Joypad> joypad
	 */
	public Player(int id, Joypad joypad) {
		this._id = id;
		this.joypad = joypad;
	}
	
	
	
}
