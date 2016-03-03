using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class ExecGame : MonoBehaviour {
	
	// Différents états d'une partie
	public enum GameState {
		JoyInit,
		Rounds
	};
	
	// État actuel
	GameState state;

	// Joueurs
	public Player[] players = new Player[4];
	
	// Objets
	public Dictionary<string, GameObject> objects;
	
	// Manche actuelle
	Round round;

	/**
	 * Créer une partie
	 */
	public static ExecGame MakeExecGame() {
		GameObject go = new GameObject("ExecGameInstance");
		ExecGame execGame = go.AddComponent<ExecGame>();
		
	    return execGame;
	}
	
	/**
	 * Initialisation
	 */
	void Start() {
		
		objects = new Dictionary<string, GameObject>();
		
		state = GameState.JoyInit;
		
		// Objets
		objects.Add("MachineBody", GameObject.Find("MachineBody"));
		objects.Add("MachineMask", GameObject.Find("MachineMask"));
        
		for (int i = 0; i < 4; i++) {
			objects.Add("Executer" + i, GameObject.Find("Executer" + i));
			objects.Add("TVScreen" + i, GameObject.Find("TVScreen" + i));
		}
		
        // Démarrer
        StartJoyInitState();
	}
	
	/**
	 * Démarrer la JoyInitState
	 */
	public void StartJoyInitState() {
		objects["MachineMask"].GetComponent<Renderer>().enabled = true;
		
		for (int i = 0; i < 4; i++) {
			objects["Executer" + i].GetComponent<Renderer>().enabled = false;
			objects["TVScreen" + i].GetComponent<Renderer>().enabled = false;
		}
	}

	/**
	 * Mise à jour
	 */
	void Update() {
		GameManager gm = GameManager.GetInstance();
		
		// Boucle sur les manettes
		foreach (Joypad joypad in gm.joypads) {
				
			if (state == GameState.JoyInit) {
				
				bool alreadyRegistred = false;
				Player associatedPlayer = null;
				foreach (Player player in players) {
					if (player != null && player.joypad == joypad) {
						alreadyRegistred = true;
						associatedPlayer = player;
						break;
					}
				}
				
				// Un joueur est enregistré, mais la manette est débranchée
				if (alreadyRegistred && !joypad.IsConnected()) {
					// Destruction du joueur si manette débranchée
					objects["Executer" + associatedPlayer.id].GetComponent<Renderer>().enabled = false;
					players[associatedPlayer.id] = null;
					associatedPlayer = null;
					Debug.Log("Manette #" + joypad.playerIndex + " déconnectée : Joueur #" + associatedPlayer.id + " supprimé.");
				}
				
				else if (!alreadyRegistred && joypad.IsConnected()) {
					int firstFreeId = -1;
					for (int i = 0; i < 4; i++) {
						if (players[i] == null) {
							firstFreeId = i;
							break;
						}
					}
					
					// Création d'un joueur pour la manette
					players[firstFreeId] = new Player(firstFreeId, joypad);
					Debug.Log("Manette #" + joypad.playerIndex + " connectée : Joueur #" + firstFreeId + " créé.");
					objects["Executer" + firstFreeId].GetComponent<Renderer>().enabled = true;
				}
				
				// Identifier son emplacement
				bool aButtonIsDown = false;
				foreach (string button in Joypad.AXIS_BUTTONS) {
					if (associatedPlayer != null && joypad.IsCurrentlyDown(button)) {
						aButtonIsDown = true;

						AudioSource audioSource = objects["TVScreen"+associatedPlayer.id].GetComponent<AudioSource>();
						AudioClip audioClip = (AudioClip)Resources.Load ("static", typeof(AudioClip));
						audioSource.clip = audioClip;
						audioSource.Play ();
						Renderer renderer = objects["TVScreen" + associatedPlayer.id].GetComponent<Renderer>();
						renderer.enabled = true;
						Color color = renderer.material.color;
						
						float rand = Random.Range(0, 4);
						
						if (rand > 3) {
							color = Color.red;
						}
						else if (rand > 2) {
							color = Color.blue;
						}
						else if (rand > 1) {
							color = Color.green;
						}
						else {
							color = Color.yellow;
						}
						
						renderer.material.color = color;
					}
				}
				
				if (!aButtonIsDown && associatedPlayer != null) {
					objects["TVScreen" + associatedPlayer.id].GetComponent<Renderer>().enabled = false;
					objects["TVScreen"+associatedPlayer.id].GetComponent<AudioSource>().Pause();
				}
				
				
				
				// Passage aux manches
				
				if (joypad.IsDown("Start")) {
					// On vérifie si toutes les manettes sont associées
					bool joypadAllAssociated = true;
					for (int i = 0; i < 4; i++) {
						joypadAllAssociated = joypadAllAssociated && (players[i] != null);
					}
					
					//if (joypadAllAssociated) {
						
						// TODO : Supprimer ÇA (remplissage des players avec de l'air)
						foreach (Joypad joypad2 in gm.joypads) {
							bool associated = false;
							for (int i = 0; i < 4; i++) {
								if (players[i] != null && players[i].joypad == joypad2) {
									associated = true;
									break;
								}
							}
							
							if (!associated) {
								for (int i = 0; i < 4; i++) {
									if (players[i] == null) {
										players[i] = new Player(i, joypad2);
										Debug.Log("Création d'un joueur #" + i + " pour les tests, associé à la manette #" + joypad2.playerIndex + " (déconnectée)");
										break;
									}
								}
							}
						}
						
						// Démarrage du jeu !
						StartRoundsState();
					//}
				}
			}
		}
	}

	/**
	 * Démarrer les manches.
	 */
	public void StartRoundsState() {
		state = GameState.Rounds;
		
		for (int i = 0; i < 4; i++) {
			objects["TVScreen" + i].GetComponent<Renderer>().enabled = false;
		}
		
		objects["MachineMask"].GetComponent<Renderer>().enabled = false;
		
		players[0].voteID = "Y";
		players[1].voteID = "B";
		players[2].voteID = "A";
		players[3].voteID = "X";
		
		NextRound();
	}

	/**
	 * Démarrer une nouvelle manche.
	 */
	public void NextRound() {
		if (round) {
			round.DestroyRound();
		}
		
		round = Round.MakeRound(this);
	}

	/**
	 * Allumer les engrenages et animation sur la machine
	 */
	public void StartMachineAnims() {
		foreach (Transform child in objects["MachineBody"].transform) {
			child.GetComponent<Animator>().SetBool ("hasStarted", true);
		}
	}

	/**
	 * Arrêter les engrenages et animation sur la machine
	 */
	public void StopMachineAnims() {
		foreach (Transform child in objects["MachineBody"].transform) {
			child.GetComponent<Animator>().SetBool ("hasStarted", false);
		}
	}
}
