using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Victims : MonoBehaviour {

	public Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();
	public int characterAmount = 50;
	public bool done;
	Vector3 doorPos = new Vector3(-118,0,0);

	// Use this for initialization
	void Start () {
	
		for (int i = 0; i<characterAmount; i++) {
			characters.Add (i,Instantiate(Resources.Load("Char0"+Random.Range(1,8)))as GameObject);
			characters[i].transform.SetParent(gameObject.transform);
			characters[i].transform.position = new Vector3(-118-(7*i),0,0);
		}
	}
	

	IEnumerator characterMove(){
		foreach (var character in characters) {
				if(character.Value.GetComponent<victimChar>().hasWalked == false){
					yield return new WaitForSeconds(1.0f);
					character.Value.GetComponent<Animator>().SetBool ("canWalk", true);

					character.Value.GetComponent<victimChar>().moved = false;

					character.Value.GetComponent<victimChar>().hasWalked=true;	
					yield return new WaitForSeconds(1.0f);
				}else if(character.Value.GetComponent<victimChar>().hasWalked == true){				
					yield return new WaitForSeconds(1.0f);

					character.Value.GetComponent<Animator>().SetBool ("canWalk", false);

					if(character.Value.GetComponent<victimChar>().moved == false){
						character.Value.GetComponent<Transform>().transform.position+= new Vector3(7,0,0);
						character.Value.GetComponent<victimChar>().moved = true;
					}

					character.Value.GetComponent<victimChar>().hasWalked=false;
					yield return new WaitForSeconds(1.0f);
					//character.Value.GetComponent<victimChar>().moved = false;
				}
			if(character.Value.GetComponent<Transform>().transform.position.x > -118){
				character.Value.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		yield return null;
	}

	// Update is called once per frame
	void Update () {
		if (!done) {
			StartCoroutine (characterMove ());
		}else{done=true;}
			
	}
}
