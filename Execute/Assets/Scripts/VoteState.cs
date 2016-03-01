using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoteState : MonoBehaviour {

	public Round round;
	public ExecGame game;
	public Player player;
	public float voteDuration = 5.0f;
	public float currentTime;
	public static bool voteStarted = false;
	public bool votesCounted;

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
		GameManager gm = GameManager.GetInstance();
		votes = new Dictionary<string, int> (4);

		foreach(Player player in game.players){
			votes.Add (player.voteID,0);
		}

	}

	void countVotes(){
		votesCounted = false;
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
		Debug.Log ("-Votes pour le joueur 0 : "+votes["Y"]
		           +" -Votes pour le joueur 1 : "+votes["B"]
		           +" -Votes pour le joueur 2 : "+votes["A"]
		           +" -Votes pour le joueur 3 : "+votes["X"]);
		votesCounted = true;
	}


	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		foreach(Player player in game.players){
			foreach (string buttonName in Joypad.AXIS_BUTTONS) {
				
				if (player.joypad.IsDown (buttonName)) {
					if(buttonName != player.voteID && new List<string>(){"A","B","X","Y"}.Contains(buttonName)){
					player.hasVotedFor = buttonName;
					Debug.Log ("Joueur "+player.id+" a voté pour "+player.hasVotedFor);
					}else if(buttonName == player.voteID){
						Debug.Log("Vous ne pouvez pas voter contre vous-meme !");
					}else if(new List<string>(){"Up", "Down", "Left", "Right"}.Contains(buttonName)){
						Debug.Log ("Vote invalide !");
					}
					break;
				}
			}
		}
					
		// A la fin du vote...
		if (currentTime > voteDuration && votesCounted == false) {
			countVotes();
		}

	}
}
