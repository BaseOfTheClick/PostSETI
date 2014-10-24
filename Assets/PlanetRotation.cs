using UnityEngine;
using System.Collections;

public class PlanetRotation : MonoBehaviour {

	public float RotationRate = 10.0f;
	
	// Update is called once per frame
	void Update () {
	
		transform.RotateAround ( transform.position, transform.up, RotationRate * Time.deltaTime );

	}
}
