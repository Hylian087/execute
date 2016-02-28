using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	// Instance du GameManager; static afin que d'autres scripts puisse y accéder
	public static GameManager instance = null;

	// Gestionnaire d'une partie
	public ExecGame game;

	// Joypads
	public Joypad[] joypads = new Joypad[4];
	
	// Prefabs
	public Dictionary<string, GameObject> prefabs;
	
	/**
	 * Retourne l'unique instance de GameManager
	 * @return <GameManager>
	 */
	public static GameManager GetInstance() {
		return instance;
	}
	
	/**
	 * Initialisation au démarrage du jeu
	 */
	public void Awake() {
		
		// si instance de GameManager n'existe pas
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) { // si l'instance existe déjà
			Destroy(gameObject); // destruction du GameManager	
		}
		
		// Assignation des joueurs et manettes
		for (int j = 0; j < 4; j++) {
			joypads[j] = new Joypad();
		}
		
		// Tableau des prefabs
		prefabs = new Dictionary<string, GameObject>();
		
		// Chargement des prefabs des boutons
		foreach (string button in Joypad.AXIS_BUTTONS) {
			string name = "Button" + button;
			prefabs.Add(name, Resources.Load(name, typeof(GameObject)) as GameObject);
		}
	}
	
	/**
	 * Démarrage d'une partie
	 */
	public void Start() {
		
		// Lancement d'une partie
		game = ExecGame.MakeExecGame();
		
	}

	public void Update() {

		//Update nécessaire pour le fonctionnement des Joypad
		Joypad.UpdateAll();
	}

}
