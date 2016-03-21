using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	// Instance du GameManager; static afin que d'autres scripts puisse y accéder
	public static GameManager instance = null;

	// Gestionnaire d'une partie
	public ExecGame game;

	// Joypads
	public Joypad[] joypads = new Joypad[4];
	
	// Prefabs
	public Dictionary<string, GameObject> prefabs;
	
	// Nuages
	private GameObject clouds;
	private int lastCloudId;
	private float lastCloudTime = 0.0f;
	private int lastCloudLayerOrder = 20;
	private float cloudsTime = 0.0f; 
	
	/**
	 * Retourne l'unique instance de GameManager
	 * @return <GameManager>
	 */
	public static GameManager GetInstance() {
		return instance;
	}
	
	/**
	 * Initialisation au démarrage du jeu
	 */
	public void Awake() {
		
		// si instance de GameManager n'existe pas
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) { // si l'instance existe déjà
			Destroy(gameObject); // destruction du GameManager	
		}
		
		// Tableau des prefabs
		prefabs = new Dictionary<string, GameObject>();
		
		// Chargement des prefabs des boutons
		foreach (string button in Joypad.AXIS_BUTTONS) {
			string name = "Button" + button;
			prefabs.Add(name, Resources.Load(name, typeof(GameObject)) as GameObject);
		}
		
		// Prefabs des feedbacks
		prefabs.Add("ButtonFeedbackOK", Resources.Load("ButtonFeedbackOK", typeof(GameObject)) as GameObject);
		
		// Chargement des prefabs des nuages
		clouds = GameObject.Find("Clouds");
		for (int i = 0; i < 3; i++) {
			prefabs.Add("Cloud" + i, Resources.Load("Clouds/Cloud" + i, typeof(GameObject)) as GameObject);
		}
		
	}
	
	/**
	 * Démarrage d'une partie
	 */
	public void Start() {
		
		for (int i = 0; i < 4; i++) {
			joypads[i] = new Joypad(i);
		}
		
		
		// Lancement d'une partie
		game = ExecGame.MakeExecGame();
		
	}

	public void Update() {

		//Update nécessaire pour le fonctionnement des Joypad
		Joypad.UpdateAll();
		
		// Mise à jour de la position des nuages
		UpdateClouds(Time.deltaTime);
	}
	
	// Mise à jour des nuages
	public void UpdateClouds(float deltaTime) {
		int count = 0;
		float lastCloudPosition = 0.0f;
		
		const float spawnY = 250f;
		const float destroyY = -100f;
		
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
