using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Button {
	
	// Durée totale du bouton
	public float duration = 1.0f;
	
	// Instant exact du bouton sur sa propre timeline (entre 0 et duration)
	public float instant = 0.5f;
	
	
	/**
	 * Constructeur
	 */
	public Button() {
	}
	
	/**
	 * Démarrage
	 */
	public void Start() {
	}
	
	/**
	 * Mise à jour
	 */
	public void Update() {
	}
	
	
	/**
	 * Retourne la précision pour le bouton actuel (-1..1)
	 * (si le joueur appuie sur une touche à ce moment, par exemple)
	 * @return <float>
	 */
	public float GetPrecisionFor(float t) {
		if (t < instant) {
			return - (1 - t / instant);
		}
		else if (t > instant) {
			return (t - instant) / (duration - instant);
		}
		else {
			return 0.0f;
		}
	}
}
