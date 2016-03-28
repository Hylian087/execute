using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public int currentSlide;
	GameObject slide1;
	GameObject slide2;
	GameObject slide3;
	Text slideTitle;
	Text slideInstructions;


	// Use this for initialization
	void Start () {
		currentSlide = 1;
		slide1 = GameObject.Find ("Slide1");
		slide2 = GameObject.Find ("Slide2");
		slide3 = GameObject.Find ("Slide3");

		slideTitle = GameObject.Find ("Title").GetComponent<Text> ();
		slideInstructions = GameObject.Find ("Instructions1").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		switch (currentSlide) {
		case 1:
			slide1.SetActive(true);
			slide2.SetActive (false);
			slide3.SetActive (false);
			slideTitle.text = "AU DEBUT D'UNE MANCHE";
			slideInstructions.text = "Si votre manette ne vibre pas, vous avez le choix entre la collaboration et la résistance. Si vous prenez le risque d’être résistant, vous en serez récompensé.";
			break;
		case 2 :
			slide1.SetActive(false);
			slide2.SetActive (true);
			slide3.SetActive (false);
			slideTitle.text ="PENDANT UNE MANCHE";
			slideInstructions.text="Exécuteurs : appuyez sur les bons boutons aux bons moments pour être le plus efficace possible. --- Résistant : appuyez sur les boutons inverses pour vous investir dans la résistance.";
			break;
		case 3 :
			slide1.SetActive(false);
			slide2.SetActive (false);
			slide3.SetActive (true);
			slideTitle.text ="A LA FIN D'UNE MANCHE";
			slideInstructions.text="Exécuteurs, débusquez le résistant pour l’empêcher de remporter la manche. Mais prenez garde à ne pas vous tromper !";
			break;
		}

		if (Input.GetButtonDown ("Joy1A") == true && currentSlide < 3) {
			currentSlide += 1;
		} else if (Input.GetButtonDown ("Joy1B") == true && currentSlide > 1) {
			currentSlide -= 1;
		} else if (Input.GetButtonDown ("Joy1Start") == true) {
			GameObject.Find ("MenuManager").GetComponent<menuScript>().menuMask.SetActive(true);
			gameObject.SetActive (false);
		}

	}
}
