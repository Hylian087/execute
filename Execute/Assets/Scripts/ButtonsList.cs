using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonsList : MonoBehaviour{

	public static List<GameObject> buttonsList = new List<GameObject>();
	void Start(){

		buttonsList.Add (Resources.Load (("ButtonA"), typeof(GameObject)) as GameObject);
		buttonsList.Add (Resources.Load (("ButtonB"), typeof(GameObject)) as GameObject);
		buttonsList.Add (Resources.Load (("ButtonX"), typeof(GameObject)) as GameObject);
		buttonsList.Add (Resources.Load (("ButtonY"), typeof(GameObject)) as GameObject);
		buttonsList.Add (Resources.Load (("ButtonUp"), typeof(GameObject)) as GameObject);
		buttonsList.Add (Resources.Load (("ButtonDown"), typeof(GameObject)) as GameObject);
		buttonsList.Add (Resources.Load (("ButtonLeft"), typeof(GameObject)) as GameObject);
		buttonsList.Add (Resources.Load (("ButtonRight"), typeof(GameObject)) as GameObject);


		Debug.Log (buttonsList);
	}

}
