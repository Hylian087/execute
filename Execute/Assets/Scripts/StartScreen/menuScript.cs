using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {

	public GameObject tutorialMask;
	public GameObject menuMask;

	void Start(){
		tutorialMask = GameObject.Find ("Tutorial");
		tutorialMask.SetActive (false);

		menuMask = GameObject.Find ("MenuCanvas");
		menuMask.SetActive (true);
	}

	public void StartLevel(){
		Application.LoadLevel (2);
	}

	public void StartTuto(){
		tutorialMask.SetActive (true);
		menuMask.SetActive (false);
	}

	public void CloseGame(){
		Application.Quit ();
	}

}
