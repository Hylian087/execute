using UnityEngine;
using System.Collections;

public class Joypad {

	// Constantes des boutons
	public const int UP = 0;
	public const int DOWN = 1;
	public const int LEFT = 2;
	public const int RIGHT = 3;

	public const int A = 4;
	public const int B = 5;
	public const int X = 6;
	public const int Y = 7;

	public const int START = 8;

	private string[] buttonsNames = new string[] { "Up", "Down", "Left", "Right", "A", "B", "X", "Y", "Start" };

	private int[] buttonsInverses = new int[] { UP, DOWN, RIGHT, LEFT, B, A, Y, X };


	// Identifiant de la dernière instance
	private static int _lastId = 0;

	// Identifiant de l'instance actuelle
	private int _id;

	// 
	private bool _isPluggedIn = false;

	// Si le 

	/**
	 * Initialisation de l'objet
	 */
	public Joypad() {

		// Changement de Joy pour la prochaine instance
		_lastId++;

		// Initialisation de l'identifiant
		_id = _lastId;
	}
	
	/**
	 * Tester si un bouton est appuyé
	 * 
	 */
	public bool isDown(int button) {
		return Input.GetButtonDown ("Joy" + _id + buttonsNames[button]);
	}
	
	
	/**
	 * Tester si un bouton est appuyé
	 * 
	 */
	public bool isInverseDown(int button) {
		return Input.GetButtonDown ("Joy" + _id + buttonsNames[buttonsInverses[button]]);
	}


	
	/**
	 * Tester si la manette est branchée
	 * (si un bouton a été pressé au moins une fois)
	 */
	public bool isPluggedIn() {
		return _isPluggedIn;
	}


	/**
	 * Retourne l'ID
	 */
	public int getID() {
		return this._id;
	}
}
