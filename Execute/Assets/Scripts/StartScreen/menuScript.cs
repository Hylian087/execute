﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {

	void Start(){
	
	}

	public void StartLevel(){
		Application.LoadLevel (1);
	}

	public void CloseGame(){
		Application.Quit ();
	}

}