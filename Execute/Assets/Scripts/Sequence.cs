using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Sequence : MonoBehaviour {
	
	// Joueur auquel est associé la séquence
	public Player player;
	
	// Durée totale de la séquence
	public float sequenceDuration;

	public int buttonCount = 31;
	public float buttonAdditionalDuration = 0.0f;

	// Choix des boutons
	int randomBtnId;

	// Temps actuel de la séquence
	float currentTime;

	int currentButtonId;
	GameObject currentButton;

	// Tableau contenant la séquence de boutons
	SortedList<int, GameObject> buttonSequence = new SortedList<int, GameObject> ();

	/**
	 * Créer une séquence
	 * @param <Player> player : joueur associé
	 * @param <float> duration : durée totale que doit faire la séquence
	 */
	public static Sequence MakeSequence(Player player, float duration) {
		GameObject go = new GameObject("SequenceJoueur" + player.Id);
		Sequence seq = go.AddComponent<Sequence>();
		
		seq.player = player;
		seq.sequenceDuration = duration;

	    return seq;
	}
	
	/**
	 * Démarrage
	 */
	void Start() {
		buttonSequence = new SortedList<int, GameObject> (buttonCount);
		// Création de la séquence
		for (int j = 0; j < buttonCount; j++) {

			// Sélection aléatoire des boutons
			randomBtnId = Random.Range (0,8);
			// Création des boutons
			GameObject showedButton = Instantiate (ButtonsList.buttonsList [randomBtnId]);

			// Ajout le bouton créé dans le tableau contenant la séquence entière
			buttonSequence.Add(j,showedButton);
			// Range les boutons dans des calques d'affichage différents (corrige la superposition des boutons)
			foreach(var instantiatedButton in buttonSequence){
				instantiatedButton.Value.GetComponent<SpriteRenderer>().sortingOrder += 1;
			}

			// Assignation de la position des boutons
			showedButton.transform.SetParent(gameObject.transform);
			showedButton.transform.position = gameObject.transform.position;

			// Ajout d'un temps additionnel (pour l'équilibrage)
			float addDuration = j+buttonAdditionalDuration;
			showedButton.GetComponent<Button>().buttonDuration += addDuration;
			// Détermination de la durée de chaque bouton
		}

		float targetDuration = sequenceDuration;
		float totalDuration = 0.0f;

		// Initialisation du premier bouton
		currentButtonId = 0;
		currentButton = buttonSequence[currentButtonId];

		/*
		do {

			Button button = Button.MakeButton(1.0f); // TODO temps aléatoire des boutons
			buttons.Add(totalDuration, button);
			
			totalDuration += button.duration;
		}
		while (totalDuration < targetDuration);
		*/
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
		// Temps actuel de la séquence
		currentTime += Time.deltaTime;

		// Si le bouton actuel est détruit...
		if (currentButton == null) {
			//... on passe au bouton suivant
			currentButton = buttonSequence[currentButtonId++];
		}

		//Debug.Log (GameManager.joypads [player.Id]);


		// Si le joueur appuie sur une touche de son pad...
		foreach (string button in Joypad.AXIS_BUTTONS) {
			if(GameManager.joypads[player.Id].IsDown(button)){
				//... si le bouton correspond à celui qui est affiché...
				if(button == currentButton.GetComponent<Button>().btnId){
					Debug.Log ("Success!");
				} else{
					Debug.Log ("Fail !");
				}
			}
		}

	}
}
