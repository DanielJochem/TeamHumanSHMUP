using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

    gameManager GameManager;

	protected float projectileSpeed;
	protected float lifeTime;
	protected float lifeTimeDuration;
	protected float damage;
	
	protected void OnTriggerEnter(Collider otherObject){
		if(otherObject.tag == "Enemy"){
            /*if(this.gameObject.tag == "Player 1")
            {
                GameManager.p1Score += points;
            }*/
			otherObject.GetComponent<Enemies>().takeDamage (damage);
		}
	}
}















