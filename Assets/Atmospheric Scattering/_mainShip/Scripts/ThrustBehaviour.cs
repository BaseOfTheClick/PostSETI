using UnityEngine;
using System.Collections;

public class ThrustBehaviour : MonoBehaviour {

 float previousInput = 0f;

 void Start () { }
 
 void FixedUpdate () {

  
  // enable thrusters when the Vertical Input Axis is greater or equals previous input
  // this ensures that the emitter stops when the key is released
    if(Input.GetAxis("Vertical") > 0 && Input.GetAxis("Vertical") >= previousInput){
     previousInput = Input.GetAxis("Vertical");
		
		particleEmitter.localVelocity = new Vector3(0,0,5);
    } else {
		
		particleEmitter.localVelocity = new Vector3(0,0,0);
    }
 }
}