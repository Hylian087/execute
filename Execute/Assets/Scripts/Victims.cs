using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Victims : MonoBehaviour {

	public Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();
	public int characterAmount = 50;

	// Use this for initialization
	void Start () {
	
		for (int i = 0; i<characterAmount; i++) {
			characters.Add (i,Instantiate(Resources.Load("Char0"+Random.Range(1,8)))as GameObject);
			characters[i].transform.SetParent(gameObject.transform);
			characters[i].transform.position = new Vector3(-120-(7*i),0,0);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
