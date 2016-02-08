using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Vote : MonoBehaviour {


	/*Get scores*/

	int highestScore, lowestScore;
	int savedPlayer1Score,savedPlayer2Score,savedPlayer3Score,savedPlayer4Score;
	int killedPlayer1Score, killedPlayer2Score, killedPlayer3Score, killedPlayer4Score;

	int totalProcessed;
	int totalSaved;
	int totalKilled;

	/*Counter*/
	Text textScore;

	/*Players and votes*/
	List<string> players = new List<string>();
	//Dictionary<string,int> playerVotes = new Dictionary<string,int>();

	// Use this for initialization
	void Start () {

		GameObject text01 = GameObject.Find("Text");
		textScore = text01.GetComponent<Text>();

		killedPlayer1Score = 1;
		killedPlayer2Score = 56;
		killedPlayer3Score = 128;
		killedPlayer4Score = 64;

		savedPlayer1Score = 128;
		savedPlayer2Score = 50;
		savedPlayer3Score = 1;
		savedPlayer4Score = 40;

		totalKilled = killedPlayer1Score + killedPlayer2Score + killedPlayer3Score + killedPlayer4Score;
		totalSaved = savedPlayer1Score + savedPlayer2Score + savedPlayer3Score + savedPlayer4Score;

		totalProcessed = totalKilled + totalSaved;

		highestScore = Mathf.Max (killedPlayer1Score, killedPlayer2Score, killedPlayer3Score, killedPlayer4Score);
		lowestScore = Mathf.Min (killedPlayer1Score, killedPlayer2Score, killedPlayer3Score, killedPlayer4Score);

		textScore.text = "Highest Score : "+highestScore+" \nLowest Score : "+lowestScore+" \nTotal Processed : "+totalProcessed+"  \nTotal Killed : "+totalKilled+" \nTotal Saved : "+totalSaved;

		players.Add ("player1");
		players.Add ("player2");
		players.Add ("player3");
		players.Add ("player4");


	}
	
	// Update is called once per frame
	void Update () {

		/*foreach (string player in players) {
			Debug.Log (player[2]);
			if(Input.GetButtonDown ("Joy2A") || Input.GetButtonDown ("Joy3A") || Input.GetButtonDown ("Joy4A")){
				playerVotes[player[0]]++;
				Debug.Log(player[1]);
			}
			if(Input.GetButtonDown ("Joy1B") || Input.GetButtonDown ("Joy3B") || Input.GetButtonDown ("Joy4B")){
				playerVotes[player[1]]++;
				Debug.Log(player[1]);
			}
			if(Input.GetButtonDown ("Joy1X") || Input.GetButtonDown ("Joy2X") || Input.GetButtonDown ("Joy4X")){
				playerVotes[player[2]]++;
				Debug.Log(player[2]);
			}
			if(Input.GetButtonDown ("Joy1Y") || Input.GetButtonDown ("Joy3Y") || Input.GetButtonDown ("Joy4Y")){
				playerVotes[player[3]]++;
				Debug.Log(player[3]);
			}

		}*/


	}
}
