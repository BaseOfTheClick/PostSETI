using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	// Zoom Vars
	public Transform target;
	
	public float distance = 20.0f;
	public float distanceMin = 5.0f;
	public float distanceMax = 25.0f;
	public float zoomSensitivity = 5f;
	public float zoomSmooth = 0.05f;
	
	private float smoothDistance;
	
	// Orbit Vars
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	
	public float yMinLimit = -50.0f;
	public float yMaxLimit = 80.0f;
	
	public float smoothTime = 0.05f;
	
	private float x;
	private float y;
	private Quaternion rotation;
	private Vector3 position;
	
	private float xSmooth = 0.0f;
	private float ySmooth = 0.0f;
	private float xVelocity = 0.0f;
	private float yVelocity = 0.0f;
	
	private Vector3 posSmooth = Vector3.zero;
	
	void Awake() {
		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;		
		
		/*if ( GetComponent<Rigidbody>() != null ) {
			rigidbody.freezeRotation = true;
		}*/
		
		rotation = Quaternion.Euler (y, x, 0);
		position = rotation * ( new Vector3(0.0f, 0.0f, -distance) + target.position );
		
		transform.rotation = rotation;
		transform.position = position;
		
		smoothDistance = -transform.localPosition.z;

	}
	
	void Update(){
		
		//Zooming
		if( Input.GetAxis ("Mouse ScrollWheel") != 0 ){
			DoZooming ();
		}
		if ( smoothDistance != distance ){
			DoZooming ();
		}
	}
	
	void LateUpdate(){
		if ( xSmooth != x | ySmooth != y ){
			
			DoCamOrbit();
			
		}
		
	} // END LateUpdate
	
	public void DoCamOrbit( float mouseX, float mouseY ){

		Debug.Log ("Orbiting");

		x += (float)( mouseX * xSpeed * 0.02f );
		y -= (float)( mouseY * ySpeed * 0.02f );
		
		xSmooth = Mathf.SmoothDamp (xSmooth, x, ref xVelocity, smoothTime);
		ySmooth = Mathf.SmoothDamp (ySmooth, y, ref yVelocity, smoothTime);
		
		ySmooth = ClampAngle(ySmooth, yMinLimit, yMaxLimit);
		
		posSmooth = target.position;
		
		rotation = Quaternion.Euler(ySmooth, xSmooth, 0);
		position = rotation * ( new Vector3(0.0f, 0.0f, -smoothDistance) + posSmooth );
		
		transform.rotation = rotation;
		transform.position = position;
	}
	
	public void DoCamOrbit () {
		
		xSmooth = Mathf.SmoothDamp (xSmooth, x, ref xVelocity, smoothTime);
		ySmooth = Mathf.SmoothDamp (ySmooth, y, ref yVelocity, smoothTime);
		
		ySmooth = ClampAngle(ySmooth, yMinLimit, yMaxLimit);
		
		posSmooth = target.position;
		
		rotation = Quaternion.Euler(ySmooth, xSmooth, 0);
		position = rotation * ( new Vector3(0.0f, 0.0f, -smoothDistance) + posSmooth );
		
		transform.rotation = rotation;
		transform.position = position;
	}
	
	public float ClampAngle( float angle, float min, float max ) {
		if( angle < -360 ) {
			angle += 360;
		}
		
		if ( angle > 360 ) {
			angle -= 360;
		}
		
		return Mathf.Clamp ( angle, min, max );
	} // END ClampAngle
	
	public void DoZooming (){
		distance += Input.GetAxis ("Mouse ScrollWheel") * -zoomSensitivity;
		distance = Mathf.Clamp (distance, distanceMin, distanceMax);
		
		smoothDistance = Mathf.Lerp ( smoothDistance, distance, Time.deltaTime * zoomSmooth );
		
		posSmooth = target.position;
		
		rotation = Quaternion.Euler(ySmooth, xSmooth, 0);
		position = rotation * ( new Vector3(0.0f, 0.0f, -smoothDistance) + posSmooth );
		
		transform.rotation = rotation;
		transform.position = position;
		
	}
	
}