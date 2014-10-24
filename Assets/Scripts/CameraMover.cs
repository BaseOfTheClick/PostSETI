using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	public int zoomOutRate = 3;

	// Use this for initialization
	void Start () {
		// Make sure the camera moves backwards.
		// if the camera is on the negative side of the z-axis
		// set zoomOutRate to be negative.
		if ( Mathf.Sign(transform.position.z) == -1 ) {
			zoomOutRate = zoomOutRate * -1;
		}

	}
	
	// Update is called once per frame
	void Update () {

		transform.position = transform.position + new Vector3( 0.0f, 0.0f, zoomOutRate * Time.deltaTime );

	}

}
