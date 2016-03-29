using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class menuScript : MonoBehaviour {

	// Joypads
	public Joypad[] joypads = new Joypad[4];
	
	public GameObject tutorialMask;
	public GameObject menuMask;
	
	private GameObject currentButton;
	private GameObject buttonCommencer;
	private GameObject buttonTutoriel;
	private GameObject buttonQuitter;

	void Start() {
		
		
		for (int i = 0; i < 4; i++) {
			joypads[i] = new Joypad(i);
		}
		
		
		tutorialMask = GameObject.Find("Tutorial");
		tutorialMask.SetActive(false);

		menuMask = GameObject.Find("MenuMask");
		menuMask.SetActive(true);
		
		
		// Tableau des prefabs
		prefabs = new Dictionary<string, GameObject>();
		
		// Chargement des prefabs des nuages
		clouds = GameObject.Find("Clouds");
		for (int i = 0; i < 3; i++) {
			prefabs.Add("Cloud" + i, Resources.Load("Clouds/Cloud" + i, typeof(GameObject)) as GameObject);
		}
		
		// Boutons du menu
		buttonCommencer = GameObject.Find("MenuMask/ButtonCommencer/Hover");
		buttonTutoriel = GameObject.Find("MenuMask/ButtonTutoriel/Hover");
		buttonQuitter = GameObject.Find("MenuMask/ButtonQuitter/Hover");
		
		SetActiveButton(buttonCommencer);
		
	}

	public void StartLevel(){
		Application.LoadLevel (1);
	}

	public void StartTuto(){
		tutorialMask.SetActive (true);
		menuMask.SetActive (false);
	}

	public void CloseGame(){
		Application.Quit ();
	}
	
	// Activer un bouton du menu principal (désactive les autres)
	private void SetActiveButton(GameObject button) {
		currentButton = button;
		buttonCommencer.SetActive(button != buttonCommencer);
		buttonTutoriel.SetActive(button != buttonTutoriel);
		buttonQuitter.SetActive(button != buttonQuitter);
	}
	
	public void Update() {
		
		// Update nécessaire pour le fonctionnement des Joypad
		Joypad.UpdateAll();
		
		// On est dans le menu principal
		if (menuMask.activeSelf) {
			
			foreach (Joypad joypad in joypads) {
				
				if (joypad.IsDown("Right")) {
					
					if (currentButton == buttonCommencer) {
						SetActiveButton(buttonTutoriel);
					}
					else {
						SetActiveButton(buttonQuitter);
					}
					
				}
				else if (joypad.IsDown("Left")) {
					
					if (currentButton == buttonQuitter) {
						SetActiveButton(buttonTutoriel);
					}
					else {
						SetActiveButton(buttonCommencer);
					}
					
				}
				else if (joypad.IsDown("A")) {
					
					if (currentButton == buttonCommencer) {
						StartLevel();
					}
					else if (currentButton == buttonTutoriel) {
						StartTuto();
					}
					else {
						CloseGame();
					}
					
				}
				
			}
			
		}
		
		UpdateClouds(Time.deltaTime);
		
	}
	
	// Prefabs
	public Dictionary<string, GameObject> prefabs;
	
	// Nuages
	private GameObject clouds;
	private int lastCloudId;
	private float lastCloudTime = 0.0f;
	private int lastCloudLayerOrder = 20;
	private float cloudsTime = 0.0f;
	
	// Mise à jour des nuages (spécial menu principal)
	public void UpdateClouds(float deltaTime) {
		int count = 0;
		float lastCloudPosition = 0.0f;
		
		const float spawnY = 250f;
		const float destroyY = -150f;
		
		foreach (Transform cloudTransform in clouds.transform) {
			Vector3 pos = cloudTransform.position;
			
			pos.y -= 5 * deltaTime * (((pos.y + -destroyY) / (spawnY + -destroyY)) + 0.01f);
			
			cloudTransform.position = pos;
			
			if (pos.y < destroyY) {
				Destroy(cloudTransform.gameObject);
			}
			
			lastCloudPosition = Mathf.Max(pos.y, lastCloudPosition);
			
			count++;
		}
		
		if (lastCloudPosition < spawnY - 100) {
			int randId;
			
			do {
				randId = Random.Range(0, 3);
			} while (randId == lastCloudId);
			lastCloudId = randId;
			
			GameObject cloud = Instantiate(prefabs["Cloud" + randId]);
			Vector3 pos = cloud.transform.position;
			
			pos.x = Random.Range(-320f, 320f);
			pos.y = spawnY;
			
			cloud.transform.position = pos;
			cloud.transform.SetParent(clouds.transform);
			lastCloudTime = cloudsTime;
			
			Debug.Log("Nuage " + lastCloudTime);
			cloud.GetComponent<Renderer>().sortingOrder = ++lastCloudLayerOrder;
		}
		
		cloudsTime += deltaTime;
	}

}
