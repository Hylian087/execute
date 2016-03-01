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
	// Les votes sont-ils terminés?
	public bool votesCounted;
	public Dictionary<string,GameObject> voteCounter;
	Dictionary<int, GameObject> voteObject = new Dictionary<int, GameObject>();


	public int hasVoted;

	static VoteState vs;

	public Dictionary<string, int> votes;

	/**
	 * Créer une voteState
	 */
	public static VoteState MakeVoteState(Round round, ExecGame game) {
		if (voteStarted == false) {
		
			GameObject go = new GameObject ("VoteState");
			vs = go.AddComponent<VoteState> ();
		
			vs.round = round;
			vs.game = game;

			voteStarted = true;
		}
		return vs;
	}


	// Use this for initialization
	void Start () {
		votes = new Dictionary<string, int> (4);
		voteCounter = new Dictionary<string,GameObject>();

		foreach(Player player in game.players){
			votes.Add (player.voteID,0);
			voteCounter.Add(player.voteID, new GameObject("VoteCounter"+player.id));
		}

		voteCounter ["Y"].transform.position = new Vector3 (-59, 69, 0);
		voteCounter ["B"].transform.position = new Vector3 (59, 69, 0);
		voteCounter ["A"].transform.position = new Vector3 (59, -69, 0);
		voteCounter ["X"].transform.position = new Vector3 (-59, -69, 0);


	}

	void AddVote(Player _player){
			foreach(var vote in voteCounter){
				if(_player.hasVotedFor == vote.Key && _player.hasAlreadyVoted == false){
				voteObject.Add (_player.id,Instantiate(Resources.Load ("skullVote")) as GameObject);
				voteObject[_player.id].transform.SetParent (vote.Value.transform);
				voteObject[_player.id].transform.position = vote.Value.transform.position;
				_player.hasAlreadyVoted = true;
				}
				else if(_player.hasVotedFor == vote.Key && _player.hasAlreadyVoted == true){
				voteObject[_player.id].transform.SetParent (vote.Value.transform);
				voteObject[_player.id].transform.position = vote.Value.transform.position;
				}
		} 
		

	}

	// Comptage des votes à la fin de la manche
	void countVotes(){
		votesCounted = false;

		// Comptage des votes pour chaque joueur
		foreach(Player player in game.players){
			Debug.Log (player.hasVotedFor);
			if(player.hasVotedFor == "A" && votes.ContainsKey("A")){
				votes["A"]+=1;
			}else if(player.hasVotedFor == "B" && votes.ContainsKey("B")){
				votes["B"]+=1;
			}else if(player.hasVotedFor == "X" && votes.ContainsKey("X")){
				votes["X"]+=1;
			}else if(player.hasVotedFor == "Y" && votes.ContainsKey("Y")){
				votes["Y"]+=1;
			}
		}

		// Attribution des votes et impact sur le score des joueurs
			// Pour chaque vote dans la liste votes
		foreach(var vote in votes){
				// Pour chaque joueur dans la partie
			foreach(Player player in game.players){
				// Si l'ID du joueur = le vote
				if(player.voteID == vote.Key){
					// Nombre de vote contre le joueur +=1
					player.hasVotes+=vote.Value;
					Debug.Log (" -Joueur # "+player.id+" a reçu "+player.hasVotes+" votes contre lui.");


					// Comptage des scores
					// Si le joueur a >3 votes et n'était pas résistant, score /2
					if(player.hasVotes > 3 && !player.isResistant){
						player.score = player.score /2;
						Debug.Log ("Joueur "+player.id+" n'était pas résistant et voit son score divisé par 2. Score actuel :"+player.score);
						// Si le joueur a >3 votes et était résistant, score /4
					} else if(player.hasVotes > 3 && player.isResistant){
						player.score = player.score /4;
						Debug.Log ("Joueur "+player.id+" était un résistant et voit son score divisé par 4. Score actuel :"+player.score);
						// Si le joueur a < 3 votes et n'était pas résistant, pas de pénalité
					} else if(player.hasVotes < 3 && !player.isResistant){
						Debug.Log ("Joueur "+player.id+ " n'a pas reçu suffisamment de vote pour etre pénalisé. Score actuel :"+player.score);
						// Si le joueur a < 3 votes et était résistant, score *4
					} else if(player.hasVotes < 3 && player.isResistant){
						player.score = player.score * 4;
						Debug.Log ("Joueur "+player.id+" était résistant et voit son score multiplié par 4. Score actuel :"+player.score);
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

		// Quand un joueur appuie sur un bouton
		foreach(Player player in game.players){
			foreach (string buttonName in Joypad.AXIS_BUTTONS) {

				if (player.joypad.IsDown (buttonName)) {

					// Si le bouton != lui-meme & que ce n'est pas les touches fléchées
					if(buttonName != player.voteID && new List<string>(){"A","B","X","Y"}.Contains(buttonName)){
						// Attribution du vote TODO : FEEDBACK
					player.hasVotedFor = buttonName;
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
		if (votesCounted == false && currentTime > voteDuration || hasVoted == 4 ) {
			countVotes();
		}

	}
}
