using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	private LensFlare flare;

	// Use this for initialization
	void Start () {
		flare = gameObject.GetComponent<LensFlare>();

	}
	
	// Update is called once per frame
	void Update () {
	
		flare.brightness -= 1.0f * Time.deltaTime;

		if ( flare.brightness <= 0.0f ) {

			Destroy(gameObject);

		}

	}
}
