using UnityEngine;
using System.Collections;

public class Player {
	
	// Identifiant du player (0-3)
	public int id;
	
	// Contrôleur associé au joueur
	public Joypad joypad;
	
	// Score
	public float score = 0;

	// Le joueur est-il résistant?
	public bool isResistant;
	// Vote ID
	public string voteID;
	// Celui pour qui le joueur a voté
	public string hasVotedFor;
	// Nombre de votes que le joueur a reçu contre lui
	public int hasVotes;
	
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
