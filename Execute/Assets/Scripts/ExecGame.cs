using UnityEngine;
using System.Collections;

public class ExecGame : MonoBehaviour {
	
	// GameManager
	protected GameManager gm;
	
	// Manche actuelle
	public Round round;
	
	/**
	 * Constructeur
	 */
	public ExecGame() {
	}
	
	/**
	 * Démarrage
	 */
	public void Start() {
		this.gm = GameManager.GetInstance();
		
		
	}
	
	/**
	 * Mise à jour
	 */
	public void Update() {
		if (round == null) {
			round = (Round) Instantiate(Round);
		}
		
		round.Update();
	}
	
}
