using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Button : MonoBehaviour {
	
	// Durée totale du bouton
	public float buttonDuration;
	public float buttonTime;
	// Instant exact du bouton sur sa propre timeline (entre 0 et duration)
	public float instant = 0.5f;

	public int btnId;


	/**
	 * Démarrage
	 */
	void Start() {
		Debug.Log (buttonDuration);
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
		buttonTime += Time.deltaTime;
		if(buttonTime > buttonDuration){
			Debug.Log("aaaa");
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
