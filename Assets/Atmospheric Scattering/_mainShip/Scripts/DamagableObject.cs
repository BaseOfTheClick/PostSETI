using UnityEngine;
using System.Collections;


public delegate void OnDeathDelegate ();

public class DamagableObject : MonoBehaviour
{

	public float health = 10;
	public OnDeathDelegate OnDeath;



	public void TakeDamage (float damage)
	{
		health -= damage;
		if (health <= 0) {
			
			
			if (OnDeath==null) {
				Destroy (gameObject);
			}else if(((System.Delegate)OnDeath).GetInvocationList ().Length < 1)
			{
				Destroy (gameObject);
				
			}
			else
			{
				OnDeath ();	
			}
			
		}
	}
	
	
}
