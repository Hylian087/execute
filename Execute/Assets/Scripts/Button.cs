using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Button : MonoBehaviour {
	
	// Durée totale du bouton (déterminée dans la séquence)
	public float duration;

	// Position du bouton dans la timeline de la séquence
	public float time;

	// Instant exact du bouton sur sa propre timeline (entre 0 et duration)
	public float instant = 0.5f;

	// ID du bouton
	public string buttonName;
	
	// Bouton pressé ou non (un bouton ne peut être pressé qu'une fois)
	public bool pressed = false;

	/**
	 * Créer un bouton
	 * @param <string> buttonName
	 * @param <float> time : Position du bouton dans la timeline de la séquence
	 * @param <float> duration : durée du bouton
	 */
	public static Button MakeButton(string buttonName, float time, float duration) {
		GameManager gm = GameManager.GetInstance();
		
		GameObject go = Instantiate(gm.prefabs["Button" + buttonName]);
		Button button = go.GetComponent<Button>();
		
		button.buttonName = buttonName;
		button.time = time;
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
	 * @return <float>
	 */
	public float GetPrecisionFor(float t) {
		float precision;
		
		if (t < instant) {
			precision = - (1 - t / instant);
		}
		else if (t > instant) {
			precision = (t - instant) / (duration - instant);
		}
		else {
			precision = 0.0f;
		}
		
		return 1.0f - Mathf.Abs(precision);
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
