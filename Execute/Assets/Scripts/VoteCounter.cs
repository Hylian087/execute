using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoteCounter : MonoBehaviour {

	public Dictionary<int, Transform> skulls;
	int skullCount;
	public int voteCount;
	public string voteID;

	// Use this for initialization
	void Start () {

		skulls = new Dictionary<int, Transform> ();

		// Récupérer les cranes
		for (skullCount = 0; skullCount < 3; skullCount++) {
			skulls.Add(skullCount+1, gameObject.transform.GetChild(skullCount));
		}

		// Cacher les cranes
		foreach (var skull in skulls) {
			skull.Value.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
		}

	}

	// Update is called once per frame
	void Update () {
		Debug.Log ("Vote Counter :" + voteID + " Count :" + voteCount);
		if(voteCount > 0){
			skulls [voteCount].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}else{
			foreach(var skull in skulls){
				skull.Value.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
			}
		}


	}
}
