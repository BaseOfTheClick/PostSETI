using UnityEngine;
using System.Collections;

public class FPSMouseFlight : MonoBehaviour
{

	public float moveSpeed = 6.0f;
	public float rotationSpeedX = 2;
	public float rotationSpeedY = 2;
	public float maxSpeed = 60.0f;
	public float maxAcceleratedSpeed = 600.0f;
	public Transform shipModel;
	public float actualSpeed = 0.0f;
	public Scale[] engineScale;
	
	public bool boost = false;
	
	public Vector3 acceleratedCameraPos = Vector3.zero;
	public ParticleEmitter particleDust;
	public float maxSpeedParticle = -50.0f;
	public float maxAcceleratedSpeedParticle = -100.0f;
	
	public Material skyboxMat;
	public float skyboxDistanceStart;
	public float skyboxDistanceEnd;
	public Transform planet;
	
	private Vector3 baseCameraPos;

	private Vector3 moveDirection = Vector3.zero;
	private float baseRotationX = 0.0f;
	private float baseRotationY = 0.0f;
	private float oldSpeed = 0.0f;
	
	void Start()
	{
		for (int i = 0; i < engineScale.Length; i++) {
				engineScale[i].scale = 0.15f;
				engineScale[i].UpdateScale();
			}
		if (acceleratedCameraPos == Vector3.zero) {
			acceleratedCameraPos = Camera.main.transform.localPosition;	
		}
		baseCameraPos = Camera.main.transform.localPosition;
	}
	
	void OnDestroy()
	{
		for (int i = 0; i < engineScale.Length; i++) {
				engineScale[i].scale = 1f;
				
		}
	}

	void FixedUpdate ()
	{
		Boost();
		if (boost) 
		{
			actualSpeed = Mathf.Clamp (actualSpeed + Input.GetAxis ("Vertical")*5, 0, maxAcceleratedSpeed);
			
		
		}
		else
		{
			if (actualSpeed > maxSpeed*2) {
				actualSpeed -= actualSpeed*0.1f;
			}else
				actualSpeed = Mathf.Clamp (actualSpeed + Input.GetAxis ("Vertical"), 0, maxSpeed);
			
			
		}
		
		float distPlanet = Vector3.Distance(transform.position, planet.position);
		
		float blend = Mathf.Lerp(0,1,
		                         1-((Mathf.Clamp (distPlanet,skyboxDistanceStart,skyboxDistanceEnd)-skyboxDistanceStart)
		                         /(float)(skyboxDistanceEnd-skyboxDistanceStart)));
		
		skyboxMat.SetFloat("_Blend", blend);
		
		if (actualSpeed<=maxSpeed) {
			float zParticleVel = Mathf.Lerp(0,maxSpeedParticle,(actualSpeed)/(float)maxSpeed);
			particleDust.localVelocity = new Vector3(0,0,zParticleVel);
		}else
		{
			float zParticleVel = Mathf.Lerp(maxSpeedParticle,maxAcceleratedSpeedParticle,(actualSpeed)/(float)maxAcceleratedSpeed);
			particleDust.localVelocity = new Vector3(0,0,zParticleVel);
		}
		
		
		Camera.main.fieldOfView = Mathf.Lerp(60,70,(actualSpeed-maxSpeed)/(float)maxAcceleratedSpeed);
		Camera.main.transform.localPosition = Vector3.Lerp(baseCameraPos,acceleratedCameraPos,(actualSpeed-maxSpeed)/(float)maxAcceleratedSpeed);
		
		if (actualSpeed != oldSpeed) {
			for (int i = 0; i < engineScale.Length; i++) {
				engineScale[i].scale = Mathf.Clamp (actualSpeed,0,maxSpeed*2) / maxSpeed * (0.35f) + 0.15f;
				engineScale[i].UpdateScale();
			}
			oldSpeed = actualSpeed;
			audio.volume = actualSpeed/maxSpeed* (0.9f) + 0.1f;
		}
		moveDirection = new Vector3 (0, 0, actualSpeed);
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= moveSpeed;
		
		baseRotationX = (Input.mousePosition.x - Screen.width * 0.5f) * 0.001f * rotationSpeedX;
		baseRotationY = (Input.mousePosition.y - Screen.height * 0.5f) * 0.001f * rotationSpeedY;

		transform.Rotate (Vector3.up, baseRotationX);
		transform.Rotate (Vector3.left, baseRotationY);
		
		float angleZ = baseRotationX * 30;
		
		
		float angleX = baseRotationY * 30;
		
		shipModel.localEulerAngles = new Vector3 (angleX, 180, angleZ);
		
		
		transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z-Input.GetAxis ("Horizontal"));
		// Move the controller
		//CharacterController controller  = (CharacterController)GetComponent("CharacterController");
		//controller.Move(moveDirection * Time.deltaTime);
		
		this.transform.position += moveDirection * Time.deltaTime;
		
	}
	
	void Boost()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			boost = true;
		}else if (Input.GetKeyUp(KeyCode.LeftShift)) {
			boost = false;
		}	
	}
	
	
}
