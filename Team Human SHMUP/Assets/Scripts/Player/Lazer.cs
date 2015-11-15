using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {

	private float projectileSpeed = 500.0f;
	
	private float lifeTime;
	private float lifeTimeDuration = 0.8f;
	
	private float damage = 20.0f;

	// Use this for initialization
	void Start () {
		lifeTime = Time.time + lifeTimeDuration;
	}
	
	// Update is called once per frame
	void Update () {
		//Movement
		transform.position += Time.deltaTime * projectileSpeed * transform.forward;
		
		//Kill projectile after time
		if(Time.time>lifeTime){
			Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider otherObject){
		if(otherObject.tag == "Enemy"){
			otherObject.GetComponent<Enemies>().takeDamage (damage);
			Destroy (this.gameObject);
		}
	}
}















