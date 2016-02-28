using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Button : MonoBehaviour {
	
	// Séquence
	public Sequence sequence;

	// Durée totale du bouton (déterminée dans la séquence)
	public float duration;

	// Position du bouton dans la timeline de la séquence
	public float startTime;

	// Instant exact du bouton sur sa propre timeline (entre 0 et duration)
	public float instant = 0.5f;

	// ID du bouton
	public string buttonName;
	
	// Bouton pressé ou non (un bouton ne peut être pressé qu'une fois)
	public bool pressed = false;

	/**
	 * Créer un bouton
	 * @param <string> buttonName
	 * @param <float> startTime : Position du bouton dans la timeline de la séquence
	 * @param <float> duration : durée du bouton
	 */
	public static Button MakeButton(Sequence sequence, string buttonName, float startTime, float duration) {
		GameManager gm = GameManager.GetInstance();
		
		GameObject go = Instantiate(gm.prefabs["Button" + buttonName]);
		Button button = go.GetComponent<Button>();
		
		button.sequence = sequence;
		button.buttonName = buttonName;
		button.startTime = startTime;
		button.duration = duration;

	    return button;
	}
	
	/**
	 * Démarrage
	 */
	void Start() {
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
	}
	
	
	/**
	 * Retourne la précision pour le bouton actuel (-1..1)
	 * (si le joueur appuie sur une touche à ce moment, par exemple)
	 * @param <float> currentTime : le temps actuel global
	 * @return <float>
	 */
	public float GetPrecisionFor(float currentTime) {
		float precision;
		
		if (currentTime < startTime || currentTime > startTime + duration) {
			return 0.0f;
		}
		
		// Temps actuel relatif au bouton
		float t = startTime + instant - currentTime;
		
		if (t < 0) {
			precision = 1.0f - Mathf.Abs(t) / instant;
		}
		else if (t > 0) {
			precision = t / instant - 1.0f;
		}
		else {
			precision = 1.0f;
		}
		
		return Mathf.Abs(precision);
	}
	
	/**
	 * Fonction utilitaire pour le debug
	 * TODO : À supprimer quand les *vrais* feedbacks auront été implantés
	 */
	public void SetColor(float r, float g, float b, float a = 1.0f) {
		Renderer renderer = gameObject.GetComponent<Renderer>();
		Color color = renderer.material.color;
		
		color.r = r;
		color.g = g;
		color.b = b;
		color.a = a;
		
		renderer.material.color = color;
	}
}
