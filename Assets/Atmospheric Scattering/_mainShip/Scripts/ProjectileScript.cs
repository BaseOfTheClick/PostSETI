using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
	public float damage = 10.0f;
	public float speed = 0.5f;
	private Vector3 velocity;

	void Start ()
	{
		velocity = transform.forward.normalized * speed;
		Destroy (gameObject, 3);
	}


	void FixedUpdate ()
	{
		
		transform.position += velocity * Time.deltaTime;
	}
	void OnCollisionEnter (Collision collision)
	{
		
		DamagableObject damObj = collision.gameObject.GetComponent<DamagableObject>();
		if (damObj) {
			damObj.TakeDamage(damage);
		}
		Destroy (gameObject);
		
		
		
	}
}
