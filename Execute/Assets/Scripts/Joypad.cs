using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

/**
 * Classe représentant une manette.
 */
public class Joypad {
	
	// Liste des joypads instanciés
	private static List<Joypad> joypads = new List<Joypad>();
	
	// Player associé
	public Player player = null;
	
	// Temps de vibration restant
	private int vibrationType;
	private float vibrationTime = 2.0f;
	private float vibrationDuration = 2.0f;
	private float vibrationStrength = 0.3f;
	
	// Liste des boutons des axes (const)
	public static string[] AXIS_BUTTONS = new string[] {"Up", "Down", "Left", "Right", "A", "B", "X", "Y"};
	
	// Tableau des touches inversées
	private Dictionary<string, string> inverses;
    
	// Identifiant du joueur
    public PlayerIndex playerIndex;
	
	// États des boutons
    public GamePadState state;
    public GamePadState prevState;
    
    private int updatesCounter = 0;
    
	/**
	 * Initialisation de l'objet
	 */
	public Joypad(int playerIndex) {
		
		// Tableau associatif des touches inverses
		inverses = new Dictionary<string, string>();
		
		inverses.Add("Up", 		"Down");
		inverses.Add("Right", 	"Left");
		inverses.Add("Left", 	"Right");
		inverses.Add("Down", 	"Up");
		
		inverses.Add("A", 		"Y");
		inverses.Add("B", 		"X");
		inverses.Add("X", 		"B");
		inverses.Add("Y", 		"A");
		
		// Initialisation de l'identifiant
		this.playerIndex = (PlayerIndex) playerIndex;
		
		// Ajout de ce joypad à la liste des joypads
		joypads.Add(this);
	}

	/**
	 * Tester si un bouton est appuyé
	 * 
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @return <bool>
	 */
	public bool IsDown(string buttonName) {
		bool pressed = false;
		
		if (updatesCounter < 2) {
			return false;
		}
		
		switch (buttonName) {
			
			case "Up":
				pressed = (prevState.DPad.Up == ButtonState.Pressed && state.DPad.Up == ButtonState.Released);
			break;
			
			case "Down":
				pressed = (prevState.DPad.Down == ButtonState.Pressed && state.DPad.Down == ButtonState.Released);
			break;
			
			case "Left":
				pressed = (prevState.DPad.Left == ButtonState.Pressed && state.DPad.Left == ButtonState.Released);
			break;
			
			case "Right":
				pressed = (prevState.DPad.Right == ButtonState.Pressed && state.DPad.Right == ButtonState.Released);
			break;
			
			case "A":
				pressed = (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released);
			break;
			
			case "B":
				pressed = (prevState.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released);
			break;
			
			case "X":
				pressed = (prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released);
			break;
			
			case "Y":
				pressed = (prevState.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released);
			break;
			
			case "Start":
				pressed = (prevState.Buttons.Start == ButtonState.Pressed && state.Buttons.Start == ButtonState.Released);
			break;
			
			default:
				pressed = false;
			break;
		}
		
		return pressed;
	}


	/**
	 * Tester si un bouton est appuyé
	 * 
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @return <bool>
	 */
	public bool IsCurrentlyDown(string buttonName) {
		bool pressed = false;
		
		if (updatesCounter < 2) {
			return false;
		}
		
		switch (buttonName) {
			
			case "Up":
				pressed = state.DPad.Up == ButtonState.Pressed;
			break;
			
			case "Down":
				pressed = state.DPad.Down == ButtonState.Pressed;
			break;
			
			case "Left":
				pressed = state.DPad.Left == ButtonState.Pressed;
			break;
			
			case "Right":
				pressed = state.DPad.Right == ButtonState.Pressed;
			break;
			
			case "A":
				pressed = state.Buttons.A == ButtonState.Pressed;
			break;
			
			case "B":
				pressed = state.Buttons.B == ButtonState.Pressed;
			break;
			
			case "X":
				pressed = state.Buttons.X == ButtonState.Pressed;
			break;
			
			case "Y":
				pressed = state.Buttons.Y == ButtonState.Pressed;
			break;
			
			case "Start":
				pressed = state.Buttons.Start == ButtonState.Pressed;
			break;
			
			default:
				pressed = false;
			break;
		}
		
		return pressed;
	}


	/**
	 * Tester si la manette est connectée
	 * @return <bool>
	 */
	public bool IsConnected() {
		return state.IsConnected;
	}


	/**
	 * Tester si l'inverse d'un bouton est appuyé
	 * (ne fonctionne que pour les touches directionnelles et ABXY)
	 * 
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @return <bool>
	 */
	public bool IsInverseDown(string button) {
		return IsDown(inverses[button]);
	}

	/**
	 * Tester si un bouton est l'inverse d'un autre
	 * (ne fonctionne que pour les touches directionnelles et ABXY)
	 * 
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @param <string> le nom de la touche ("A", "B", "Up", ...)
	 * @return <bool>
	 */
	public bool IsInverse(string button1, string button2) {
		return button2 == inverses[button1];
	}

	/**
	 * Retourne l'ID du joypad
	 * 
	 * @return <int>
	 */
	public int getID() {
		return (int) playerIndex;
	}
	
	
	/**
	 * Mise à jour
	 */
	public void Update() {
		
        prevState = state;
        state = GamePad.GetState(playerIndex);
        
        updatesCounter++;
        
		if (vibrationTime < vibrationDuration) {
			
			float activate = 1.0f;
			
			if (vibrationType != 1) {
				activate = 1.0f - Mathf.Floor((vibrationTime / vibrationDuration) / (1.0f / (vibrationType * 2.0f - 1.0f))) % 2;
			}
			
			GamePad.SetVibration(playerIndex, activate * vibrationStrength, activate * vibrationStrength);
			
			vibrationTime += Time.deltaTime;
			
			if (vibrationTime >= vibrationDuration) {
				GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
			}
		}
	}
	
	
	/**
	 * Mise à jour des toutes les instances de Joypad
	 */
	public static void UpdateAll() {
		// Met à jour le tableau des axes utilisées
		foreach (Joypad joypad in joypads) {
			joypad.Update();
		}
	}
	
	/**
	 * Retourne un bouton au hasard
	 * @return <string>
	 */
	public static string GetRandomButton() {
		return AXIS_BUTTONS[Random.Range(0, AXIS_BUTTONS.Length)];
	}
	
	/**
	 * Faire vibrer la manette deux fois
	 * @param <float> 
	 */
	public void VibrateOnce(float vibrationDuration = 2.0f, float vibrationStrength = 0.5f) {
		this.vibrationType = 1;
		this.vibrationTime = 0.0f;
		this.vibrationDuration = vibrationDuration;
		this.vibrationStrength = vibrationStrength;
	}
	
	/**
	 * Faire vibrer la manette deux fois
	 * @param <float> 
	 */
	public void VibrateTwice(float vibrationDuration = 2.0f, float vibrationStrength = 0.5f) {
		this.vibrationType = 2;
		this.vibrationTime = 0.0f;
		this.vibrationDuration = vibrationDuration;
		this.vibrationStrength = vibrationStrength;
	}
	
	/**
	 * Faire vibrer la manette trois fois
	 * @param <float> 
	 */
	public void VibrateThrice(float vibrationDuration = 2.0f, float vibrationStrength = 0.5f) {
		this.vibrationType = 3;
		this.vibrationTime = 0.0f;
		this.vibrationDuration = vibrationDuration;
		this.vibrationStrength = vibrationStrength;
	}
}
