using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Sequence {
	
	// Joueur auquel est associé la séquence
	public Player player;
	
	// Temps actuel dans la séquence
	public float t = 0.0f;
	
	// Durée totale de la séquence
	public float duration;
	
	// Séquences de boutons (<float> key : instant de début du bouton)
	public SortedList<float, Button> buttons;
	
	/**
	 * Créer une séquence
	 * @param <Player> player : joueur associé
	 * @param <float> duration : durée totale que doit faire la séquence
	 */
	public static Sequence MakeSequence(Player player, float duration) {
		GameObject go = new GameObject("SequenceInstance");
		Sequence seq = go.AddComponent<Sequence>();
		
		seq.player = player;
		seq.duration = duration;
		
	    return seq;
	}
	
	/**
	 * Démarrage
	 */
	public void Start() {
		buttons = new SortedList<float, Button>();
		
		// TODO créer les boutons et les ajouter à buttons
		//Button button = Button.MakeButton();
		//buttons.Add(i, button);
	}
	
	/**
	 * Mise à jour
	 */
	public void Update() {
		t += Time.deltaTime;
	}
}
