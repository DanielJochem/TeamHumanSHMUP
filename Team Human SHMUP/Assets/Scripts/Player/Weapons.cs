using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

	protected float projectileSpeed;
	protected float lifeTime;
	protected float lifeTimeDuration;
	protected float damage;
    public int pointsForBoss = 696969;
	
	protected void OnTriggerEnter(Collider otherObject){
		if(otherObject.tag == "Enemy"){
			otherObject.GetComponent<Enemies>().takeDamage (damage);
		}
	}
}















