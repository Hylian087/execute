using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Représente une séquence de boutons
 * Permet de calculer la précision du joueur 
 */
public class Sequence : MonoBehaviour {
	
	// Manche à laquelle est associé la séquence
	public Round round;
	
	// Joueur auquel est associé la séquence
	public Player player;
	
	// Durée totale de la séquence
	public float duration;
	
	// La séquence est terminée
	public bool done = false;
	
	// Nombre de patterns
	public int patternCount = 12;
	public int buttonCount = 0; // Calculé
	
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
	public static Sequence MakeSequence(Round round, Player player) {
		GameObject go = new GameObject("Player" + player.id + "Sequence");
		Sequence seq = go.AddComponent<Sequence>();

		seq.round = round;
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
		for (int p = 0; p < patternCount; p++) {
			
			buttonCount = (int) Mathf.Ceil(p / 2 + 3);
			
			for (int b = 0; b < buttonCount; b++) {
				
				// Création d'un bouton avec son GO
				float bDuration;
				float bInstant;
				
				// Sélection aléatoire des boutons
				string randomButtonId = Joypad.GetRandomButton();
			
				bDuration = (((float) (patternCount - p)) / ((float) patternCount) * 2.0f + 1.0f) * 0.35f;
				bInstant = bDuration / 2;
				
				if (b == 0) {
					bDuration += 2.0f;
					bInstant += 2.0f;
					
					if (p == 0) {
						
						// Rajouter un petit temps pour décaler tous les joueurs
						bDuration += 0.4f * player.id;
						bInstant += 0.4f * player.id;
						
					}
				}
				
				Button button = Button.MakeButton(this, randomButtonId, time, bDuration, bInstant);
				GameObject buttonGO = button.gameObject;
				button.gameObject.GetComponent<Renderer>().enabled = false;

				// Ajout du bouton créé dans le tableau contenant la séquence entière
				buttons.Add(button);
				
				// Assignation de la position des boutons
				buttonGO.transform.SetParent(gameObject.transform);
				buttonGO.transform.position = gameObject.transform.position;
				
				time += button.duration;
				
			}
			
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

		//Debug.Log ("currentButtonId : "+ currentButtonId);


		// Mise à jour de la position des boutons
		float scale = 30.0f;
		foreach (var button in buttons) {
			Vector3 position = button.gameObject.transform.position;
			Renderer renderer = button.gameObject.GetComponent<Renderer>();
			Color color = renderer.material.color;
			button.okSound = Resources.Load ("Sounds/Input"+(player.id+1))as AudioClip;
			
			// Position en fonction du temps
			position.x = (button.startTime + button.instant - currentTime) * scale + gameObject.transform.position.x;
			
			// Visible ou non (valeurs en dur dans le code)
			renderer.enabled = 
				position.x > gameObject.transform.position.x - 32
				&& position.x < gameObject.transform.position.x + 48;
			
			// Application des modifications
			button.gameObject.transform.position = position;
			renderer.material.color = color;
		}
		
		
		// Si le joueur appuie sur une touche de son pad
		string buttonDownName = null;
		
		foreach (string buttonName in Joypad.AXIS_BUTTONS) {
			if (player.joypad.IsDown(buttonName)) {
				buttonDownName = buttonName;
				break;
			}
		}
		
		if (buttonDownName != null && !currentButton.pressed) {
			float precision = currentButton.GetPrecisionFor(currentTime);
			
			if (precision > 0) {
				
				// Le joueur exécute et réussit
				if (currentButton.buttonName == buttonDownName) {
					
					//Debug.Log("Joueur #" + player.id + " presse " + buttonDownName + " : " + Mathf.Floor(100 * precision) + "% de précision.");
					//currentButton.SetColor(0.0f, 1.0f, 0.0f);
					
					currentButton.LaunchFeedback(precision);
					
					player.joypad.VibrateOnce(0.1f);
					round.scores[player.id] += Mathf.RoundToInt(precision * 100);

					player.execCount+=1;
					//Debug.Log ("Joueur "+player.id+" boutons :"+player.execCount);
					currentButton.buttonAudioSource.clip = currentButton.okSound;
					currentButton.buttonAudioSource.Play ();
				}
				
				// Le joueur résiste et réussit
				else if (
					player == round.resistant &&
					player.joypad.IsInverse(currentButton.buttonName, buttonDownName)
				) {
					//Debug.Log("Joueur #" + player.id + " presse " + buttonDownName + " : " + Mathf.Floor(100 * precision) + "% de précision [résistance]");
					//currentButton.SetColor(0.0f, 0.0f, 1.0f);

					currentButton.LaunchFeedback(precision);
					
					round.scores[player.id] += Mathf.RoundToInt(precision * 100);
					player.joypad.VibrateTwice(0.4f);

					player.resistantCount += 1;
					//Debug.Log ("Joueur "+player.id+" boutons :"+player.execCount);
					//Debug.Log ("Joueur "+player.id+" resiste boutons :"+player.resistantCount);


					currentButton.buttonAudioSource.clip = currentButton.okSound;
					currentButton.buttonAudioSource.Play ();
				}
				// Le joueur se trompe
				else {
					
					currentButton.LaunchFeedback(0.0f);
					
					//Debug.Log("Joueur #" + player.id + " s'est trompé de bouton (" + currentButton.buttonName + " != " + buttonDownName + ")");
					//currentButton.SetColor(1.0f, 0.0f, 0.0f);
					player.joypad.VibrateOnce(0.5f, 1.0f);
					currentButton.buttonAudioSource.clip = currentButton.errorSound;
					currentButton.buttonAudioSource.Play ();
				}
				
				currentButton.pressed = true;
			}
		}
	}
	
	
    void OnDrawGizmos() {
    	
    	Vector3 seqPosA = gameObject.transform.position;
    	seqPosA.y -= 0.5f;
		Vector3 seqPosB = new Vector3(seqPosA.x, seqPosA.y + 1.0f);
    	
		Gizmos.color = Color.magenta;
    	Gizmos.DrawLine(seqPosA, seqPosB);
        
        if (player.id == 0) {
			foreach (var button in buttons) {
				Vector3 a = button.gameObject.transform.position;
				
				a.y += 0.5f;
				
				Vector3 b = new Vector3(a.x, a.y);
				
				float precision = button.GetPrecisionFor(currentTime);
				
				b.y += 0.5f * (button.startTime + button.instant - currentTime);
				
        		Gizmos.color = Color.green;
	        	//Gizmos.DrawLine(a, b);
	        	
	        	a.x += 0.05f;
	        	b.x += 0.05f;
	        	
				b.y = a.y + 20f * precision;
				
        		Gizmos.color = Color.green;
	        	Gizmos.DrawLine(a, b);
	        	
	        	if (currentButton == button) {
	        		Gizmos.color = Color.blue;
		        	Gizmos.DrawLine(new Vector3(a.x - 5f, a.y - 5f), new Vector3(a.x + 5f, a.y - 5f));
	        	}
			}
        }
    }
}
