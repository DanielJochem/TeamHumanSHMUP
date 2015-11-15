using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	gameManager GameManager;
	
	private float projectileSpeed = 10.0f;
	private float rotationSpeed = 10.0f;
	
	private GameObject closestEnemyUnit;
	
	private float lifeTime;
	private float lifeTimeDuration = 5.0f;
	
	private float damage = 50.0f;

	// Use this for initialization
	void Start () {
		GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
		lifeTime = Time.time + lifeTimeDuration;
	}
	
	// Update is called once per frame
	void Update () {
		//Projectile Movement
		transform.position += Time.deltaTime * projectileSpeed * transform.forward;
		
		closestEnemyUnit = FindClosestEnemyUnit();
		
		if (closestEnemyUnit != null){
			//Smooth Lock
			//Determine the target rotation. This is the rotation if the transform looks at the target point
			Quaternion targetRotation = Quaternion.LookRotation (closestEnemyUnit.transform.position - transform.position);
			
			//Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
		
		//Kill projectile after time
		if(Time.time>lifeTime) {
			Destroy (this.gameObject);
		}
	}
	
	//Algorithm controlling the detection of closest enemy target using global enemy list
	//Return the closest enemy in enemyList
	GameObject FindClosestEnemyUnit() {
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		
		foreach(GameObject enemyUnit in GameManager.enemyUnitList) {
			Vector3 diff = enemyUnit.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			
			if(curDistance < distance) {
				closestEnemyUnit = enemyUnit;
				distance = curDistance;
			}
		}
		return closestEnemyUnit;
	}
	
	void OnTriggerEnter(Collider collider) {
		if(collider.tag == "Enemy") {
            collider.GetComponent<Enemies>().takeDamage(damage);
            Destroy(this.gameObject);

            if (collider.GetComponent<Enemies>().health <= 0) {
                if (this.gameObject.tag == "P1Fired") {
                    GameManager.enemiesKilledP1++;
                } else if (this.gameObject.tag == "P2Fired") {
                    GameManager.enemiesKilledP2++;
                }
            }
        }
	}
}
