using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoteState : MonoBehaviour {

	// Références aux Instances
	public Round round;
	public ExecGame game;
	public Player player;

	// Timer du vote
	public float voteDuration = 60.0f;

	bool voteDisplayed;
	GameObject timerText;

	// Temps passé sur le vote
	public float currentTime;

	// Les votes ont-ils été comptés? (Vote terminé?)
	public bool votesCounted;
	public bool scoreCounted;
	// Score animation
	public float animDuration = 1.5f;

	// Resistant montré
	public bool resistantShown = false;
	// Tous les calculs sont-ils faits?
	public bool done;
	// Next round launched?
	public bool nextRound;

	// Personnes qui ont voté
	public int hasVoted;

	// Compteurs de scores de PARTIE
	public Dictionary<int, GameObject> globalScoreCounter;

	// Compteurs de scores de ROUND
	public Dictionary<int, GameObject> roundScoreCounter;

	// Compteurs de votes (un pour chaque joueur), string = nom du joueur, GameObject = objet créé en jeu pour afficher le compteur
	public Dictionary<string,GameObject> voteCounter;

	// Dictionnaire contenant les feedback visuel des votes de chaque joueur (int = ID du joueur, GameObject = crane)
	Dictionary<int, GameObject> voteSkull;

	// Tableau décomptant les votes : string = nom du joueur, int = nombre de votes
	public Dictionary<string, int> votes;

	public GameObject continueButtons;
	// Instance du script
	static VoteState vs;



	/**
	 * Créer une voteState
	 */
	public static VoteState MakeVoteState(Round round, ExecGame game) {
		bool voteStarted = false;
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

	IEnumerator voteDisplay(){
		if (!voteDisplayed) {

		// création du mask
		GameObject mask = Instantiate(Resources.Load ("VoteStateMask") as GameObject);
		mask.transform.SetParent (gameObject.transform);

		yield return new WaitForSeconds (0.25f);
			mask.GetComponentInChildren<MeshRenderer>().sortingLayerName = "Vote";
			mask.GetComponentInChildren<MeshRenderer>().sortingOrder = 7;
			mask.GetComponentInChildren<TextMesh>().text = "JOUR "+game.roundNumber;

		//Compteur de scores de partie pour chaque joueur
		foreach (Player player in game.players) {
			// Création des compteurs de scores GLOBAUX
			globalScoreCounter.Add (player.id, Instantiate (Resources.Load ("GlobalScoreCounter"+player.id)) as GameObject);
			// Code NECESSAIRE pour l'affichage du score de partie
			globalScoreCounter[player.id].GetComponentInChildren<MeshRenderer>().sortingLayerName = "Vote";
			globalScoreCounter[player.id].GetComponentInChildren<MeshRenderer>().sortingOrder = 7;
			// Affichage du score global
			globalScoreCounter[player.id].GetComponentInChildren<TextMesh>().text = player.score.ToString ();
		}

		yield return new WaitForSeconds(0.75f);
		// Compteur de scores Round pour chaque joueur
		foreach(Player player in game.players){

			// Création des compteurs de scores DE ROUND
			roundScoreCounter.Add (player.id, Instantiate (Resources.Load ("RoundScoreCounter"+player.id)) as GameObject);
			// Code NECESSAIRE pour l'affichage du score de round
			roundScoreCounter[player.id].GetComponent<MeshRenderer>().sortingLayerName = "Vote";
			roundScoreCounter[player.id].GetComponent<MeshRenderer>().sortingOrder=7;
			// Affichage du score de round
			roundScoreCounter[player.id].GetComponentInChildren<TextMesh>().text = "+"+round.scores[player.id].ToString();						
		}

		yield return new WaitForSeconds(0.75f);	
		// Compteur de votes pour chaque joueur
		foreach (Player player in game.players) {
			// Création des compteurs de votes
			votes.Add (player.voteID,0);
			voteCounter.Add(player.voteID, Instantiate (Resources.Load ("VoteCounter"+player.id)) as GameObject);
		}

		// Rangement des compteurs dans leur parent (le GO VoteState)
		foreach (var counter in voteCounter) {
			counter.Value.transform.SetParent (gameObject.transform);
		}
		
		foreach (var gSCounter in globalScoreCounter) {
			gSCounter.Value.transform.SetParent (gameObject.transform);
		}
		
		foreach (var rSCounter in roundScoreCounter) {
			rSCounter.Value.transform.SetParent (gameObject.transform);
		}
		
			yield return new WaitForSeconds(0.75f);
			timerText = Instantiate (Resources.Load ("VoteTimer") as GameObject);
			timerText.transform.SetParent (gameObject.transform);
			timerText.GetComponent<MeshRenderer>().sortingLayerName = "Vote";
			timerText.GetComponent<MeshRenderer>().sortingOrder = 7;
			timerText.GetComponent<TextMesh>().text = voteDuration.ToString();

		// création du start
		continueButtons = Instantiate (Resources.Load ("ContinueButtons")) as GameObject;
		continueButtons.transform.SetParent (gameObject.transform);

			yield return new WaitForSeconds(0.75f);

		voteDisplayed = true;
		}

	}


	IEnumerator countVotes(){
		if (!done) {
		
		if (!votesCounted) {
			// Comptage des votes pour chaque joueur
			foreach (Player player in game.players) {			
				if (player.hasVotedFor == "A" && votes.ContainsKey ("A")) {
					votes ["A"] += 1;
					//Debug.Log (votes ["A"]);
				} else if (player.hasVotedFor == "B" && votes.ContainsKey ("B")) {
					votes ["B"] += 1;
					//Debug.Log (votes ["B"]);
				} else if (player.hasVotedFor == "X" && votes.ContainsKey ("X")) {
					votes ["X"] += 1;
					//Debug.Log (votes ["X"]);
				} else if (player.hasVotedFor == "Y" && votes.ContainsKey ("Y")) {
					votes ["Y"] += 1;
					//Debug.Log (votes ["Y"]);
				}
			}
			votesCounted = true;
		}
		
			yield return new WaitForSeconds (1.0f);

			foreach(Player player in game.players){
				if(player.isResistant && !resistantShown){
					GameObject resistantStamp = Instantiate(Resources.Load ("ResistantStamp")) as GameObject; 
					resistantStamp.transform.SetParent(gameObject.transform);
					resistantStamp.transform.position = game.objects["Executer"+player.id].transform.position + new Vector3(0,-25,0);
					resistantShown = true;
				}
			}

			yield return new WaitForSeconds(1.0f);
		
		// Attribution des votes et impact sur le score des joueurs
		// Pour chaque vote dans la liste votes
		foreach (var vote in votes) {
			// Pour chaque joueur dans la partie
			foreach (Player player in game.players) {
				// Si l'ID du joueur = le vote
				if (player.voteID == vote.Key && votesCounted == true) {
					// Nombre de vote contre le joueur +=1, addition des scores au score global
					player.hasVotes += vote.Value;
					//Debug.Log (" -Joueur # " + player.id + " a reçu " + player.hasVotes + " votes contre lui.");		
					StartCoroutine(countScores(player));
					}
				}
				
			}				

			yield return new WaitForSeconds(animDuration);

			foreach(Player player in game.players){
				//if(!scoreCounted){
				// Conditions de victoire et défaite
				// Si le joueur a >3 votes et n'était pas résistant, score /2
				if (player.hasVotes > 3 && !player.isResistant && !player.scoreCounted) {
					player.score = player.score / 2;
					player.scoreCounted = true;
					//Debug.Log ("Joueur " + player.id + " n'était pas résistant et voit son score divisé par 2. Score GLOBAL actuel :" + player.score);
					// Si le joueur a >3 votes et était résistant, score /4
				} else if (player.hasVotes > 3 && player.isResistant && !player.scoreCounted) {
					player.score = player.score / 4;
					player.scoreCounted = true;
					//Debug.Log ("Joueur " + player.id + " était un résistant et voit son score divisé par 4. Score GLOBAL actuel :" + player.score);
					// Si le joueur a < 3 votes et n'était pas résistant, pas de pénalité
				} else if (player.hasVotes < 3 && !player.isResistant && !player.scoreCounted) {
					player.scoreCounted = true;
					//Debug.Log ("Joueur " + player.id + " n'a pas reçu suffisamment de vote pour etre pénalisé. Score GLOBAL actuel :" + player.score);
					// Si le joueur a < 3 votes et était résistant, score *4
				} else if (player.hasVotes < 3 && player.isResistant && !player.scoreCounted) {
					player.score = player.score * 4;
					player.scoreCounted = true;
					//Debug.Log ("Joueur " + player.id + " était résistant et voit son score multiplié par 4. Score GLOBAL actuel :" + player.score);
				}
				//scoreCounted = true;
				//}
			}	
				
		yield return new WaitForSeconds(1.0f);
			done = true;
		}
	}


	IEnumerator countScores(Player _player){
		
		
		int gScorestart = (int)_player.score;
		int gScoretarget = (int)_player.score + round.scores [_player.id];

		int rScorestart = (int)round.scores [_player.id];
		int rScoretarget = (int)round.scores [_player.id] - round.scores [_player.id];
		
		for (float timer = 0; timer < animDuration; timer+=Time.deltaTime) {
			float progress = timer/animDuration;
			_player.score = (int)Mathf.Lerp(gScorestart,gScoretarget,progress);
			round.scores[_player.id] = (int)Mathf.Lerp (rScorestart,rScoretarget,progress);
			yield return null;
		}

		_player.score = gScoretarget;	
		round.scores [_player.id] = rScoretarget;		
	}


	// Fonction d'affichage des votes dans les compteurs
	void AddSkull(Player _player){
		// Pour chaque compteur dans voteCounter
		foreach(var counter in voteCounter){
			// Si un joueur a voté pour un joueur et qu'il n'avait pas voté avant...
			if(_player.hasVotedFor == counter.Key && _player.hasAlreadyVoted == false){
				// Création d'un crane et placement dans le compteur
				voteSkull.Add (_player.id,Instantiate(Resources.Load ("skullVote")) as GameObject);
				voteSkull[_player.id].transform.SetParent (counter.Value.transform);
				voteSkull[_player.id].transform.position = counter.Value.transform.position + new Vector3((_player.id+1)*10,-25,0);
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

	// Use this for initialization
	void Start () {
		// refacto
		if(globalScoreCounter!=null && roundScoreCounter !=null && voteCounter !=null && voteSkull != null && votes !=null){
			globalScoreCounter.Clear();
			roundScoreCounter.Clear();
			voteCounter.Clear();
			voteSkull.Clear();
			votes.Clear();
		} else if(globalScoreCounter==null && roundScoreCounter ==null && voteCounter ==null && voteSkull == null && votes ==null){
				globalScoreCounter = new Dictionary<int, GameObject> ();
				roundScoreCounter = new Dictionary<int, GameObject>();
				voteCounter = new Dictionary<string,GameObject>();
				voteSkull = new Dictionary<int, GameObject>();
				votes = new Dictionary<string, int> ();
		}

		foreach(Player player in game.players){

			player.resistantRatio = (player.resistantCount / round.sequences[player.id].buttonCount) * 100.0f;
			if(player.resistantRatio >=0 && player.resistantRatio < 75){
				//Debug.Log ("Joueur "+player.id+" est exécutant");
			} else if(player.resistantRatio >= 75){
				player.isResistant = true;
				//Debug.Log ("Joueur "+player.id+" est résistant avec un ratio de "+player.resistantRatio+" %");
			}
		}

		StartCoroutine (voteDisplay ());

	}

	// Update is called once per frame
	void Update () {

		if (voteDisplayed) {
			foreach(Player player in game.players){
				globalScoreCounter[player.id].GetComponentInChildren<TextMesh>().text = player.score.ToString ();
				roundScoreCounter[player.id].GetComponentInChildren<TextMesh>().text = "+"+round.scores[player.id].ToString();
			}

		}

		if (voteDisplayed && !votesCounted) {

			// Temps passé sur la manche de vote
			currentTime += Time.deltaTime;
			// Animation des boutons start
			continueButtons.GetComponent<Animator>().SetInteger("hasVoted", hasVoted);

			if(currentTime < voteDuration){
				timerText.GetComponent<TextMesh>().text = (Mathf.RoundToInt(voteDuration-currentTime)).ToString();
			}else{
				timerText.GetComponent<TextMesh>().text = "TEMPS ECOULE";
			}

			// Quand un joueur appuie sur un bouton
			foreach(Player player in game.players){		
				foreach (string buttonName in Joypad.AXIS_BUTTONS) {
					if (player.joypad.IsDown (buttonName)) {
						
						// Si le bouton != lui-meme & que ce n'est pas les touches fléchées
						if(buttonName != player.voteID && new List<string>(){"A","B","X","Y"}.Contains(buttonName)){
							player.hasVotedFor = buttonName;
							// Création / déplacement des skulls dans les compteurs
							AddSkull (player);
							//Debug.Log ("Joueur "+player.id+" a voté pour "+player.hasVotedFor);
							
							// Si le joueur vote pour lui-meme, NOPE TODO : FEEDBACK
						}else if(buttonName == player.voteID){
							//Debug.Log("Vous ne pouvez pas voter contre vous-meme !");
							
							// Si le joueur appuie sur une touche fléchée, NOPE TODO : FEEDBACK
						}else if(new List<string>(){"Up", "Down", "Left", "Right"}.Contains(buttonName)){
							//Debug.Log ("Vote invalide !");
						}
						break;
					}
				}
				
				// Les joueurs appuient sur Start
				if(Input.GetButtonDown("Joy"+(player.id+1)+"Start") && !player.hasPushedStart){
					hasVoted+=1;
					player.hasPushedStart=true;
					//Debug.Log (hasVoted);
				}else if(Input.GetButtonDown("Joy"+(player.id+1)+"Start") && player.hasPushedStart){
					hasVoted-=1;
					player.hasPushedStart=false;
					//Debug.Log (hasVoted);
				}
				
			}
		}			


		if (hasVoted >= 3 && !votesCounted  || currentTime >= voteDuration && !votesCounted) {
			StartCoroutine(countVotes ());
		} else if (hasVoted >= 3 && votesCounted  || currentTime > voteDuration && votesCounted){
			if(done && !nextRound){
				// Reinit
				hasVoted = 0;
				foreach(Player player in game.players){
					player.execCount = 0;
					player.resistantCount = 0;
					player.resistantRatio = 0.0f;
					player.isResistant = false;
					player.hasVotedFor = "";
					player.hasVotes = 0;
					player.hasAlreadyVoted = false;
					player.hasPushedStart = false;
					player.scoreCounted = false;
				}
				GameManager.GetInstance().game.NextRound(); 
				nextRound=true;
			}
				
		}

	}
}
