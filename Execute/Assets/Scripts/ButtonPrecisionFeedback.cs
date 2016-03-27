using UnityEngine;
using System.Collections;

public class ButtonPrecisionFeedback : MonoBehaviour {
	
	private float time = 0.0f;
	private const float duration = 1.0f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		
		if (time < duration) {
			Vector3 pos = gameObject.transform.position;
			Color color = gameObject.GetComponent<Renderer>().material.color;
			
			if (time / duration > 0.5f) {
				color.a = 1.0f - (time / duration - 0.5f) / 0.5f;
			}
			else {
				color.a = 1.0f;
			}
			
			pos.y += Time.deltaTime * 20 * (1 - time / duration);
			
			gameObject.transform.position = pos;
			gameObject.GetComponent<Renderer>().material.color = color;
		}
		else {
			Destroy(gameObject);
		}
	}
}
