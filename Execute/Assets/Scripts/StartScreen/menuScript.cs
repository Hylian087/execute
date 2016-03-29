using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class menuScript : MonoBehaviour {

	// Joypads
	public Joypad[] joypads = new Joypad[4];
	
	public GameObject tutorialMask;
	public GameObject menuMask;

	GameObject introText1;
	GameObject introText2;
	GameObject title;
	GameObject subtitle;
	GameObject buttonsGroup;
	Vector3 titleStartPosition;
	Vector3 titleEndPosition;
	float currentTime;

	private GameObject currentButton;
	private GameObject buttonCommencer;
	private GameObject buttonTutoriel;
	private GameObject buttonQuitter;

	void Start() {

		tutorialMask = GameObject.Find("Tutorial");
		tutorialMask.SetActive(false);
		
		menuMask = GameObject.Find("MenuMask");
		menuMask.SetActive(true);

		buttonsGroup = GameObject.Find ("Boutons");
		buttonsGroup.SetActive (false);

		subtitle = GameObject.Find ("Subtitle");
		subtitle.SetActive (false);

		introText1 = GameObject.Find ("Texte1");
		introText2 = GameObject.Find ("Texte2");
		title = GameObject.Find ("MainTitle");

		titleStartPosition = title.transform.position;
		titleEndPosition = new Vector3 (0, 45, 0);

		introText1.SetActive (false);
		introText2.SetActive (false);

		if (introText1.activeSelf == false) {
			StartCoroutine(introCine ());
		}
		
		for (int i = 0; i < 4; i++) {
			joypads[i] = new Joypad(i);
		}
		
		

		
		
		// Tableau des prefabs
		prefabs = new Dictionary<string, GameObject>();
		
		// Chargement des prefabs des nuages
		clouds = GameObject.Find("Clouds");
		for (int i = 0; i < 3; i++) {
			prefabs.Add("Cloud" + i, Resources.Load("Clouds/Cloud" + i, typeof(GameObject)) as GameObject);
		}
		
		// Boutons du menu
		var buttonsTransf = GameObject.Find("MenuMask").transform.Find("Boutons").gameObject.transform;
		buttonCommencer = buttonsTransf.Find("ButtonCommencer/Hover").gameObject;
		buttonTutoriel = buttonsTransf.Find("ButtonTutoriel/Hover").gameObject;
		buttonQuitter = buttonsTransf.Find("ButtonQuitter/Hover").gameObject;
	}

	IEnumerator introCine(){
		yield return new WaitForSeconds (2.0f);
		GameObject.Find("MenuManager").GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds (0.5f);
		introText1.SetActive (true);
		yield return new WaitForSeconds (3.0f);
		introText1.SetActive (false);
		introText2.SetActive (true);
		yield return new WaitForSeconds (3.0f);
		introText2.SetActive (false);
		title.GetComponent<Animator> ().SetBool ("animate", true);
		yield return new WaitForSeconds (5.0f);
		subtitle.SetActive (true);
		yield return new WaitForSeconds (1.0f);
		buttonsGroup.SetActive (true);
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
		currentTime += Time.deltaTime;
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
