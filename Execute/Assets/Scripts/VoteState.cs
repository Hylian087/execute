using UnityEngine;
using System.Collections;

public class VoteState : MonoBehaviour {

	public Round round;

	/**
	 * Créer une voteState
	 */
	public static VoteState MakeVoteState(Round round) {
		GameObject go = new GameObject("VoteState");
		VoteState vs = go.AddComponent<VoteState>();
		
		vs.round = round;
		
		return vs;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
