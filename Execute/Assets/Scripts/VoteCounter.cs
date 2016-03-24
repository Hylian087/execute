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

		switch (voteCount) {
		case 0 :
			skulls [1].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			skulls [2].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			skulls [3].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			break;
		case 1:
			skulls [1].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			skulls [2].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			skulls [3].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			break;
		case 2 :
			skulls [1].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			skulls [2].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			skulls [3].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			break;
		case 3 :
			skulls [1].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			skulls [2].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			skulls [3].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			break;
		}
			

			


	}
}
