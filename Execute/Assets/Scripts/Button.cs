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
	public float instant;
	
	// Temps où le bouton est cliquable autour de son "instant".
	public float clickableRadius = 0.5f;

	// ID du bouton
	public string buttonName;
	
	// Bouton pressé ou non (un bouton ne peut être pressé qu'une fois)
	public bool pressed = false;

	/**
	 * Créer un bouton
	 * @param <string> buttonName
	 * @param <float> startTime : Position du bouton dans la timeline de la séquence
	 * @param <float> duration : durée du bouton
	 * @param <float> instant : instant où le bouton est cliquable (instant < duration !!!)
	 */
	public static Button MakeButton(Sequence sequence, string buttonName, float startTime, float duration = 1.0f, float instant = 0.5f) {
		GameManager gm = GameManager.GetInstance();
		
		GameObject go = Instantiate(gm.prefabs["Button" + buttonName]);
		Button button = go.GetComponent<Button>();
		
		button.sequence = sequence;
		button.buttonName = buttonName;
		button.startTime = startTime;
		button.duration = duration;
		button.instant = instant;

	    return button;
	}
	
	/**
	 * Démarrage
	 */
	void Start() {
		GetComponent<SpriteRenderer>().sortingLayerName = "Buttons";
		GetComponent<SpriteRenderer> ().sortingOrder = 5;
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
	}
	
	
	/**
	 * Retourne la précision pour le bouton actuel (0..1)
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
		
		// Trop tôt
		if (t > 0) {
			precision = t / Mathf.Clamp(clickableRadius, 0.0f, duration - instant);
		}
		
		// Trop tard
		else if (t < 0) {
			precision = Mathf.Abs(t) / Mathf.Clamp(clickableRadius, 0.0f, instant);
		}
		
		// Perfection !
		else {
			precision = 0.0f;
		}
		
		return 1.0f - Mathf.Clamp01(Mathf.Abs(precision));
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
	
	/**
	 * Lancer le feedback
	 */
	public void LaunchFeedback(float precision) {
		GameManager gm = GameManager.GetInstance();
		
		if (precision > 0) {
			// Feedback
			GameObject feedbackGO = Instantiate(gm.prefabs["ButtonFeedbackOK"]);
			feedbackGO.transform.position = gameObject.transform.position;
		}
	}
}

