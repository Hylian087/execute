using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Button : MonoBehaviour {
	
	// Durée totale du bouton
	public float duration = 1.0f;
	
	// Instant exact du bouton sur sa propre timeline (entre 0 et duration)
	public float instant = 0.5f;

	public int btnId;

	/**
	 * Créer un bouton
	 * @param <float> duration : durée que doit faire le bouton
	 */
	/*public static Button MakeButton(float duration) {

		GameObject go = new GameObject("ButtonInstance");
		Button button = go.AddComponent<Button>();
		
		button.duration = duration;
		
	    return button;
	}*/
	
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
