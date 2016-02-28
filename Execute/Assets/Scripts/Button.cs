using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Button : MonoBehaviour {
	
	// Durée totale du bouton (déterminée dans la séquence)
	public float buttonDuration;

	// Position actuelle du bouton dans la timeline de la séquence
	public float buttonTime;

	// Instant exact du bouton sur sa propre timeline (entre 0 et duration)
	public float instant = 0.5f;

	// Id du bouton (pour les prefabs : Up Down Left Right...)
	public string btnId;
	public string invertBtnId;

	/**
	 * Démarrage
	 */
	void Start() {
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
		// Position actuelle du bouton dans la timeline de la séquence dans laquelle il se trouve
		buttonTime += Time.deltaTime;
		// Calcul du moment exact où il faut appuyer sur le bouton
		instant = buttonDuration - instant;
		// Si la position actuelle du bouton dans la timeline de la séquence est supérieure à sa durée de vie, destruction du bouton
		if(buttonTime > buttonDuration){
			Destroy(gameObject);
		}
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
			return (t - instant) / (buttonDuration - instant);
		}
		else {
			return 0.0f;
		}
	}
}
