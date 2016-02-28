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
	public float buttonAdditionalDuration = 1.0f;

	// Choix des boutons
	int randomBtnId;

	// Temps actuel de la séquence
	float currentTime;

	//public List<GameObject> buttons;

	// Séquences de boutons (<float> key : instant de début du bouton)


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
		// Création de la séquence
		for (float j = 0; j < buttonCount; j++) {

			// Sélection aléatoire des boutons
			randomBtnId = Random.Range (0,8);
			// Création des boutons
			GameObject showedButton = Instantiate (ButtonsList.buttonsList [randomBtnId]);

			// Assignation de la position des boutons
			showedButton.transform.SetParent(gameObject.transform);
			showedButton.transform.position = gameObject.transform.position;

			// Ajout d'un temps additionnel (pour l'équilibrage)
			float addDuration = j+buttonAdditionalDuration;
			showedButton.GetComponent<Button>().buttonDuration += addDuration;
			// Détermination de la durée de chaque bouton
			Debug.Log (showedButton.GetComponent<Button>().buttonDuration += addDuration);
		}

		float targetDuration = sequenceDuration;
		float totalDuration = 0.0f;

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
		//Debug.Log (GameManager.joypads [player.Id]);


		// Si le joueur appuie sur une touche de son pad...
		foreach (string button in Joypad.AXIS_BUTTONS) {
			if(GameManager.joypads[player.Id].IsDown(button)){
				//Debug.Log (GameManager.joypads[player.Id].getID() + " a appuyé sur " + button);
			}
		}
	}
}
