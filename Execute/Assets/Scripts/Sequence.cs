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
	
	// Temps actuel dans la séquence
	public float t = 0.0f;
	
	// Durée totale de la séquence
	public float sequenceDuration;

	
	// Choix des boutons
	int randomBtnId;

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
		for (int j = 0; j < sequenceDuration; j++) {
			randomBtnId = Random.Range (0,8);
			GameObject button = Instantiate (ButtonsList.buttonsList [randomBtnId]);

			button.GetComponent<Button>().buttonDuration += j;
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
		t += Time.deltaTime;
	}
}
