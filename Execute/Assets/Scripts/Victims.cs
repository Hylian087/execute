using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Victims : MonoBehaviour {

	public Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();
	public int characterAmount = 50;
	public bool neverDone;
	public bool testWhile;
	public float delay;
	public float currentTime;
	Vector3 doorPos = new Vector3(-118,0,0);

	// Use this for initialization
	void Start () {
		neverDone = true;
		for (int i = 0; i<characterAmount; i++) {
			characters.Add (i,Instantiate(Resources.Load("Char0"+Random.Range(1,10)))as GameObject);
			characters[i].transform.SetParent(gameObject.transform);
			characters[i].transform.position = new Vector3(-118-(7*i),0,0);
		}
		//testWhile = 0;
		delay = 5.0f;


			/*
			if(currentTime == delay){
				StartCoroutine (characterMove ());
				testWhile = true;
			}else if(currentTime > delay){
				testWhile = false;
				currentTime = 0.0f;
			}
			 */


	}
	

	IEnumerator characterMove(){

			foreach (var character in characters) {
				if (character.Value.GetComponent<victimChar> ().hasWalked == false) {
					yield return new WaitForSeconds (1.0f);
					character.Value.GetComponent<Animator> ().SetBool ("canWalk", true);					
					character.Value.GetComponent<victimChar>().moved = false;					
					character.Value.GetComponent<victimChar> ().hasWalked = true;	
					//yield return new WaitForSeconds(1.0f);

					if (character.Value.GetComponent<Transform> ().transform.position.x > -118) {
						character.Value.GetComponent<SpriteRenderer> ().enabled = false;
					}

			} else if(character.Value.GetComponent<victimChar>().hasWalked == true){				
				yield return new WaitForSeconds(1.0f);

				if(character.Value.GetComponent<Animator>().GetBool("canWalk") == true){
					character.Value.GetComponent<Animator>().SetBool ("canWalk", false);				
					yield return null;
				}


				if(character.Value.GetComponent<victimChar>().moved == false){
					yield return null;
					character.Value.GetComponent<Transform>().transform.position+= new Vector3(7.0f,0,0);
					//character.Value.GetComponent<SpriteRenderer>().sprite=
					character.Value.GetComponent<victimChar>().moved = true;
				}			
				
				character.Value.GetComponent<victimChar>().hasWalked=false;
				//yield return new WaitForSeconds(1.0f);
				} 
			}


			/* 
			*/

		yield return null;
	}
		

	// Update is called once per frame
	void Update () {		

		if (currentTime <= delay) {
			currentTime += Time.deltaTime;
		}

		if(currentTime >= delay){
			StartCoroutine (characterMove ());
			currentTime = 0.0f;
			//testWhile = true;
		}
		//Debug.Log (currentTime);


	}
}
