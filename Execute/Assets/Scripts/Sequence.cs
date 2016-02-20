using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Sequence {
	
	// Temps actuel dans la séquence
	private float t = 0.0f;
	
	// Durée totale de la séquence
	private float duration = 4.0f;
	
	// Longueur des séquences
	private int _length;
	public int Length {
		get { return buttons.Count; }
		set {}
	}
	
	// Séquences de boutons
	protected SortedList<int, string> buttons;
	
	
	/**
	 * Constructeur
	 */
	public Sequence(int length = 3) {
		buttons = new SortedList<int, string>();
		
		for (int i = 0; i < length; i++) {
			buttons.Add(i, Joypad.GetRandomButton());
		}
	}
	
	
	/**
	 * Mise à jour
	 */
	public void Update() {
		t += Time.deltaTime;
		Debug.Log(t);
	}
	
	/**
	 * Retourne un nombre entre 0 et 1 représentant la progression du curseur dans cette séquence.
	 * Si le nombre est supérieur à 1, alors la séquence est terminée.
	 * @return <float>
	 */
	public float GetNormalizedProgression() {
		return t / duration;
	}
	
	/**
	 * Retourne le numéro de la touche actuelle dans la séquence.
	 * @return <int>
	 */
	public int GetCurrentButtonNumber() {
		return (int) Mathf.Ceil(t / duration * Length);
	}
	
	
	/**
	 * Retourne le nom de la touche actuelle dans la séquence.
	 * @return <string>
	 */
	public string GetCurrentButtonName() {
		return buttons[GetCurrentButtonNumber()];
	}
	
	
	/**
	 * Retourne la précision pour le bouton actuel (0..1)
	 * (si le joueur appuie sur une touche à ce moment, par exemple)
	 * @return <float>
	 */
	public float GetCurrentPrecision() {
		float buttonDuration = duration / Length;
		return 1 - Mathf.Abs((float) (t % buttonDuration / buttonDuration - 0.5)) * 2;
	}
}
