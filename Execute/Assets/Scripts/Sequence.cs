using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Sequence : MonoBehaviour {
	
	// Joueur auquel est associé la séquence
	public Player player;
	
	// Durée totale de la séquence
	public float duration;
	
	// La séquence est terminée
	public bool done = false;
	
	// Nombre de boutons
	public int buttonCount = 30;
	
	// Temps actuel de la séquence
	private float currentTime;
	
	// Bouton actuel
	private int currentButtonId = 0;
	private Button currentButton;

	// Tableau contenant la séquence de boutons
	public List<Button> buttons;

	/**
	 * Créer une séquence
	 * @param <Player> player : joueur associé
	 */
	public static Sequence MakeSequence(Player player) {
		GameObject go = new GameObject("Player" + player.id + "Sequence");
		Sequence seq = go.AddComponent<Sequence>();
		
		seq.player = player;

	    return seq;
	}
	
	/**
	 * Démarrage
	 */
	void Start() {
		
		GameManager gm = GameManager.GetInstance();
		
		buttons = new List<Button>();
		
		float time = 0.0f;
		
		// Création de la séquence
		for (int j = 0; j < buttonCount; j++) {

			// Sélection aléatoire des boutons
			string randomButtonId = Joypad.GetRandomButton();
			
			// Création d'un bouton avec son GO
			Button button = Button.MakeButton(this, randomButtonId, time, 1.0f);
			GameObject buttonGO = button.gameObject;

			// Ajout du bouton créé dans le tableau contenant la séquence entière
			buttons.Add(button);
			
			// Assignation de la position des boutons
			buttonGO.transform.SetParent(gameObject.transform);
			buttonGO.transform.position = gameObject.transform.position;
			
			time += button.duration;
		}
		
		// Durée totale de la séquence = durée de tous les boutons
		duration = time;
		
		// Premier bouton
		currentButtonId = 0;
		currentButton = buttons[currentButtonId];

		// Range les boutons dans des calques d'affichage différents (corrige la superposition des boutons)
		foreach (var button in buttons) {
			button.gameObject.GetComponent<SpriteRenderer>().sortingOrder += 1;
		}
	}
	
	/**
	 * Mise à jour
	 */
	void Update() {
		// Temps actuel de la séquence
		currentTime += Time.deltaTime;
		
		// Si la séquence est terminée
		if (currentTime > duration) {
			done = true;
		}
		else {
			// Si on a dépassé le bouton actuel
			if (currentTime > currentButton.startTime + currentButton.duration) {
				currentButtonId++;
				currentButton = buttons[currentButtonId];
			}
		}
		
		// Mise à jour de la position des boutons
		float scale = 1.0f;
		foreach (var button in buttons) {
			Vector3 position = button.gameObject.transform.position;
			Renderer renderer = button.gameObject.GetComponent<Renderer>();
			Color color = renderer.material.color;
			
			// Position en fonction du temps
			position.x = button.startTime * scale - currentTime + gameObject.transform.position.x;
			
			// Opacité en fonction du temps (+-1s)
			color.a = 1 - Mathf.Abs(Mathf.Clamp(button.startTime - currentTime, -1.0f, 1.0f));
			
			// Application des modifications
			button.gameObject.transform.position = position;
			renderer.material.color = color;
		}
		
		
		// Si le joueur appuie sur une touche de son pad
		foreach (string buttonName in Joypad.AXIS_BUTTONS) {
			if (player.joypad.IsDown(buttonName)) {
				if (!currentButton.pressed) {
					if (currentButton.buttonName == buttonName) {
						float precision = currentButton.GetPrecisionFor(currentTime);
						
						Debug.Log("Joueur #" + player.id + " presse " + buttonName + " : " + Mathf.Floor(100 * precision) + "% de précision.");
						currentButton.SetColor(0.0f, 1.0f, 0.0f);
					}
					else {
						Debug.Log("Joueur #" + player.id + " s'est trompé de bouton (" + currentButton.buttonName + " != " + buttonName + ")");
						currentButton.SetColor(1.0f, 0.0f, 0.0f);
					}
					
					currentButton.pressed = true;
				}
			}
		}
	}
	
	
    void OnDrawGizmos() {
    	
    	Vector3 seqPosA = gameObject.transform.position;
    	seqPosA.y -= 0.5f;
		Vector3 seqPosB = new Vector3(seqPosA.x, seqPosA.y + 1.0f);
    	
		Gizmos.color = Color.black;
    	Gizmos.DrawLine(seqPosA, seqPosB);
        
        if (player.id == 0) {
			foreach (var button in buttons) {
				Vector3 a = button.gameObject.transform.position;
				
				a.y += 0.5f;
				
				Vector3 b = new Vector3(a.x, a.y);
				
				float precision = button.GetPrecisionFor(currentTime);
				
				b.y += 0.5f * (button.startTime - currentTime);
				
        		Gizmos.color = Color.green;
	        	Gizmos.DrawLine(a, b);
	        	
	        	a.x += 0.05f;
	        	b.x += 0.05f;
	        	
				b.y += 0.5f * precision;
				
        		Gizmos.color = Color.red;
	        	Gizmos.DrawLine(a, b);
	        	
	        	if (currentButton == button) {
	        		Gizmos.color = Color.blue;
		        	Gizmos.DrawLine(new Vector3(a.x - 0.2f, a.y - 1f), new Vector3(a.x + 0.2f, a.y - 1f));
	        	}
			}
        }
    }
}
