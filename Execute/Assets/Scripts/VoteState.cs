using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoteState : MonoBehaviour {

	public Round round;
	public ExecGame game;
	public Player player;

	// Timer du vote
	public float voteDuration = 5.0f;

	// Temps passé sur le vote
	public float currentTime;

	// Le vote a-t-il commencé?
	public static bool voteStarted = false;
	// Les votes ont-ils été comptés? (Vote terminé?)
	public bool votesCounted;
	public bool scoreCounted;

	// Personnes qui ont voté
	public int hasVoted;

	// Compteurs de scores de PARTIE
	public Dictionary<int, GameObject> globalScoreCounter = new Dictionary<int, GameObject>();

	// Compteurs de scores de ROUND
	public Dictionary<int, GameObject> roundScoreCounter = new Dictionary<int, GameObject>();

	// Compteurs de votes (un pour chaque joueur), string = nom du joueur, GameObject = objet créé en jeu pour afficher le compteur
	public Dictionary<string,GameObject> voteCounter = new Dictionary<string,GameObject>();

	// Dictionnaire contenant les feedback visuel des votes de chaque joueur (int = ID du joueur, GameObject = crane)
	Dictionary<int, GameObject> voteSkull = new Dictionary<int, GameObject>();

	// Tableau décomptant les votes : string = nom du joueur, int = nombre de votes
	public Dictionary<string, int> votes = new Dictionary<string, int> (4);

	public GameObject continueButtons;
	// Instance du script
	static VoteState vs;



	/**
	 * Créer une voteState
	 */
	public static VoteState MakeVoteState(Round round, ExecGame game) {
		if (voteStarted == false) {
		
			GameObject go = new GameObject ("VoteState");
			vs = go.AddComponent<VoteState> ();
			vs.transform.SetParent (round.transform);
		
			vs.round = round;
			vs.game = game;

			voteStarted = true;
		}
		return vs;
	}


	// Use this for initialization
	void Start () {
		GameObject mask = Instantiate(Resources.Load ("VoteStateMask") as GameObject);
		mask.transform.SetParent (gameObject.transform);

		continueButtons = Instantiate (Resources.Load ("ContinueButtons")) as GameObject;
		continueButtons.transform.SetParent (gameObject.transform);

		foreach(Player player in game.players){
			// Création des compteurs de votes
			votes.Add (player.voteID,0);
			voteCounter.Add(player.voteID, Instantiate (Resources.Load ("VoteCounter"+player.id)) as GameObject);
			// Création des compteurs de scores GLOBAUX
			globalScoreCounter.Add (player.id, Instantiate (Resources.Load ("GlobalScoreCounter"+player.id)) as GameObject);
			// Code NECESSAIRE pour l'affichage du score de partie
			globalScoreCounter[player.id].GetComponentInChildren<MeshRenderer>().sortingLayerName = "Vote";
			globalScoreCounter[player.id].GetComponentInChildren<MeshRenderer>().sortingOrder = 7;
			// Création des compteurs de scores DE ROUND
			roundScoreCounter.Add (player.id, Instantiate (Resources.Load ("RoundScoreCounter"+player.id)) as GameObject);
			// Code NECESSAIRE pour l'affichage du score de round
			roundScoreCounter[player.id].GetComponent<MeshRenderer>().sortingLayerName = "Vote";
			roundScoreCounter[player.id].GetComponent<MeshRenderer>().sortingOrder=7;
		}

		foreach (var counter in voteCounter) {
			counter.Value.transform.SetParent (gameObject.transform);
		}

		foreach (var gSCounter in globalScoreCounter) {
			gSCounter.Value.transform.SetParent (gameObject.transform);
		}

		foreach (var rSCounter in roundScoreCounter) {
			rSCounter.Value.transform.SetParent (gameObject.transform);
		}


	}

	// Fonction d'affichage des votes dans les compteurs
	void AddVote(Player _player){
		// Pour chaque compteur dans voteCounter
			foreach(var counter in voteCounter){
			// Si un joueur a voté pour un joueur et qu'il n'avait pas voté avant...
				if(_player.hasVotedFor == counter.Key && _player.hasAlreadyVoted == false){
				// Création d'un crane et placement dans le compteur
				voteSkull.Add (_player.id,Instantiate(Resources.Load ("skullVote")) as GameObject);
				voteSkull[_player.id].transform.SetParent (counter.Value.transform);
				voteSkull[_player.id].transform.position = counter.Value.transform.position + new Vector3((_player.id+1)*10,0,0);
				// Le joueur a donc voté
				_player.hasAlreadyVoted = true;
				}
			// Si le joueur change son vote (s'il a déjà voté mais qu'il vote encore une fois)
				else if(_player.hasVotedFor == counter.Key && _player.hasAlreadyVoted == true){
				// Changement de la place du crane vers le joueur pour lequel il change son vote
				voteSkull[_player.id].transform.SetParent (counter.Value.transform);
				voteSkull[_player.id].transform.position = counter.Value.transform.position + new Vector3((_player.id+1)*10,0,0);
				}
		} 
		

	}

	// Comptage des votes à la fin du vote
	void countVotes(){
		votesCounted = false;

			// Comptage des votes pour chaque joueur
			foreach (Player player in game.players) {
				if (player.hasVotedFor == "A" && votes.ContainsKey ("A")) {
					votes ["A"] += 1;
				} else if (player.hasVotedFor == "B" && votes.ContainsKey ("B")) {
					votes ["B"] += 1;
				} else if (player.hasVotedFor == "X" && votes.ContainsKey ("X")) {
					votes ["X"] += 1;
				} else if (player.hasVotedFor == "Y" && votes.ContainsKey ("Y")) {
					votes ["Y"] += 1;
				}
			}

			// Attribution des votes et impact sur le score des joueurs
			// Pour chaque vote dans la liste votes
			foreach (var vote in votes) {
				// Pour chaque joueur dans la partie
				foreach (Player player in game.players) {
					// Si l'ID du joueur = le vote
					if (player.voteID == vote.Key) {
						// Nombre de vote contre le joueur +=1
						player.hasVotes += vote.Value;
						Debug.Log (" -Joueur # " + player.id + " a reçu " + player.hasVotes + " votes contre lui.");

						// Comptage des scores, différentes conditions de victoire et défaite
						// Si le joueur a >3 votes et n'était pas résistant, score /2
						if (player.hasVotes > 3 && !player.isResistant) {
							player.score = player.score / 2;
							Debug.Log ("Joueur " + player.id + " n'était pas résistant et voit son score divisé par 2. Score actuel :" + player.score);
							// Si le joueur a >3 votes et était résistant, score /4
						} else if (player.hasVotes > 3 && player.isResistant) {
							player.score = player.score / 4;
							Debug.Log ("Joueur " + player.id + " était un résistant et voit son score divisé par 4. Score actuel :" + player.score);
							// Si le joueur a < 3 votes et n'était pas résistant, pas de pénalité
						} else if (player.hasVotes < 3 && !player.isResistant) {
							Debug.Log ("Joueur " + player.id + " n'a pas reçu suffisamment de vote pour etre pénalisé. Score actuel :" + player.score);
							// Si le joueur a < 3 votes et était résistant, score *4
						} else if (player.hasVotes < 3 && player.isResistant) {
							player.score = player.score * 4;
							Debug.Log ("Joueur " + player.id + " était résistant et voit son score multiplié par 4. Score actuel :" + player.score);
						}
					}

				}
			}
			// Les votes ont été comptés !
			votesCounted = true;
	}


	// Update is called once per frame
	void Update () {
		// Temps passé sur la manche de vote
		currentTime += Time.deltaTime;
		// Animation des boutons start
		continueButtons.GetComponent<Animator>().SetInteger("hasVoted", hasVoted);

		// Quand un joueur appuie sur un bouton
		foreach(Player player in game.players){

			roundScoreCounter[player.id].GetComponentInChildren<TextMesh>().text = "+"+round.scores[player.id].ToString();
			globalScoreCounter[player.id].GetComponentInChildren<TextMesh>().text = player.score.ToString ();

			foreach (string buttonName in Joypad.AXIS_BUTTONS) {

				if (player.joypad.IsDown (buttonName)) {

					// Si le bouton != lui-meme & que ce n'est pas les touches fléchées
					if(buttonName != player.voteID && new List<string>(){"A","B","X","Y"}.Contains(buttonName)){
					player.hasVotedFor = buttonName;
						// Création / déplacement des skulls dans les compteurs
					AddVote (player);
					Debug.Log ("Joueur "+player.id+" a voté pour "+player.hasVotedFor);

						// Si le joueur vote pour lui-meme, NOPE TODO : FEEDBACK
					}else if(buttonName == player.voteID){
						Debug.Log("Vous ne pouvez pas voter contre vous-meme !");

						// Si le joueur appuie sur une touche fléchée, NOPE TODO : FEEDBACK
					}else if(new List<string>(){"Up", "Down", "Left", "Right"}.Contains(buttonName)){
						Debug.Log ("Vote invalide !");
					}
					break;
				}
			}
		}
					
		// A la fin du vote...
		foreach(Player player in game.players){
			if(Input.GetButtonDown("Joy"+(player.id+1)+"Start") && !player.hasPushedStart){
				hasVoted+=1;
				//player.hasPushedStart=true;
				Debug.Log (hasVoted);
			}else if(Input.GetButtonDown("Joy"+(player.id+1)+"Start") && player.hasPushedStart){
				hasVoted-=1;
				//player.hasPushedStart=false;
				Debug.Log (hasVoted);
			}
		}

		if (hasVoted >= 3 && !votesCounted) {
			countVotes ();
		} else if (hasVoted >= 3 && votesCounted){
			foreach(Player player in game.players){
				if(!scoreCounted){
					player.score+=round.scores[player.id];
					round.scores[player.id]-=round.scores[player.id];
					scoreCounted = true;
					voteStarted = false;

					// Reload une manche!
					game.hasEnded = true;
				}
			}
		}

	}
}
