using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Round : MonoBehaviour {
	
	// Partie
	public ExecGame game;

	// Temps avant le début de la phase de rythme
	public float warmUpDuration = 3.0f;

	// Temps depuis le début du round
	public float currentTime = 0.0f;

	// Durée totale de la phase de rythme
	public float rhythmDuration;

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
	public bool resistantChosen;

	// Séquences de boutons
	public Sequence[] sequences = new Sequence[4];
	
	// Aiguille de l'horloge
	private GameObject clockArm;

	public AudioClip machineStart = (AudioClip)Resources.Load ("machine_start", typeof(AudioClip));
	public AudioClip machineWork = (AudioClip)Resources.Load ("machine_working", typeof(AudioClip));
	public AudioClip machineIdle = (AudioClip)Resources.Load ("machine_idle", typeof(AudioClip));
	public AudioClip machineStop = (AudioClip)Resources.Load ("machine_stop", typeof(AudioClip));

	public bool soundPlayed;


	/**
	 * Créer une manche
	 */
	public static Round MakeRound(ExecGame game) {
		GameObject go = new GameObject("RoundInstance");
		Round round = go.AddComponent<Round>();
		AudioSource machineSound = go.AddComponent<AudioSource> ();
		
		round.game = game;
		
	    return round;
	}
	
	/**
	 * Démarrage
	 */
	void Start() {
		
		state = RoundState.WarmUp;
		
        clockArm = GameObject.Find("ClockArm");
	}

	void chooseResistant(){

		if (!resistantChosen) {

			resistant = game.players[Random.Range(0, 4)];
			Debug.Log ("Joueur "+resistant.id+" a été designé résistant");
			
			foreach (Player player in game.players) {
				if (player == resistant && resistant.chosenCount < player.chosenMaxCount) {
					//Debug.Log ("Joueur "+player.id+" a été résistant "+resistant.chosenCount+" fois, on le choisit");
					player.joypad.VibrateThrice();
					player.chosenCount++;
					resistantChosen = true;
				}
				else if(player == resistant && resistant.chosenCount == 1){
					//Debug.Log ("Joueur "+player.id+" a été résistant 2 fois, choix d'un nouveau résistat");
					resistantChosen = false;
				}
				else if(player != resistant){
					//Debug.Log ("Joueur "+player.id+" est éxécutant");
					player.joypad.VibrateTwice();
				}

			}

		}

	}
	
	/**
	 * Démarrage de la phase de rythme
	 */
	void StartRhythmState() {

		state = RoundState.Rhythm;
		gameObject.GetComponent<AudioSource> ().PlayOneShot (machineStart);
		if (machineStart.length>1) {
			gameObject.GetComponent<AudioSource> ().clip = machineWork;
			gameObject.GetComponent<AudioSource> ().Play ();
		}
		
		game.StartMachineAnims();
		
		Sequence seq;
		rhythmDuration = 0.0f;
		
		// Initialisation des scores et séquences
		for (int i = 0; i < 4; i++) {
			scores[i] = 0;
			seq = Sequence.MakeSequence(this, game.players[i]);
			seq.transform.parent = gameObject.transform.parent;
			seq.transform.SetParent(gameObject.transform);
			
			rhythmDuration = Mathf.Max(rhythmDuration, seq.duration);
			
			sequences[i] = seq;
		}

		// Initialisation de la position des séquences (valeurs en dur)
		sequences [0].transform.position = new Vector3(-59, 68, 0);
		sequences [1].transform.position = new Vector3(37, 68, 0);
		sequences [2].transform.position = new Vector3(37, -29, 0);
		sequences [3].transform.position = new Vector3(-59, -29, 0);
	}
	
	/**
	 * Démarrage de la phase de rythme
	 */
	public void StartVoteState() {
		state = RoundState.Vote;
		VoteState.MakeVoteState (this, game);

		gameObject.GetComponent<AudioSource> ().clip = machineStop;
		gameObject.GetComponent<AudioSource> ().Play ();
	}

	public void DestroyRound(){
		Destroy (gameObject,3);
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
		currentTime += Time.deltaTime;

		if (game.players [0].chosenCount == game.players [0].chosenMaxCount &&
		    game.players [1].chosenCount == game.players [1].chosenMaxCount &&
		    game.players [2].chosenCount == game.players [2].chosenMaxCount &&
		    game.players [3].chosenCount == game.players [3].chosenMaxCount) {
			foreach (Player player in game.players) {
				player.chosenCount = 0;
				Debug.Log ("Tout le monde a été choisi deux fois, on réinitialise");
			}
		} else {
			chooseResistant ();
		}

		
		if (state == RoundState.WarmUp) {

			if(!soundPlayed){
				gameObject.GetComponent<AudioSource>().loop = true;
				gameObject.GetComponent<AudioSource>().PlayOneShot(machineIdle);
				soundPlayed = true;
			}


			if (currentTime > warmUpDuration) {
				StartRhythmState();
			} 
		}
		else if (state == RoundState.Rhythm) {
			bool done = true;
			
			foreach (Sequence seq in sequences) {
				done = done && seq.done;
			}
			
			if (done) {
				game.StopMachineAnims();
				clockArm.transform.localEulerAngles = new Vector3(0, 0, 0);
				
				StartVoteState();
			}
			else {
				if (rhythmDuration == 0) {
					foreach (Sequence seq in sequences) {
						rhythmDuration = Mathf.Max(rhythmDuration, seq.duration);
					}
				}
				
				clockArm.transform.localEulerAngles = new Vector3(0, 0, - (currentTime - warmUpDuration) / rhythmDuration * 360);
			}
		}

	}
}
