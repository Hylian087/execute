﻿using UnityEngine;
using System.Collections;

public class AnimationDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Update is called once per frame
	public void DestroyMe () {
		Destroy(gameObject);
	}
}
