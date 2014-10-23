using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	public float zoomOutRate;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (transform.position.x,
		                                  transform.position.y,
		                                  transform.position.z + zoomOutRate * Time.deltaTime);

	}

}
