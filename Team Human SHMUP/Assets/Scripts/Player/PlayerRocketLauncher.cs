﻿using UnityEngine;
using System.Collections;

public class PlayerRocketLauncher : Weapons {

    private GameObject closestEnemyUnit;
    public float rotationSpeed = 10.0f;
    
    // Use this for initialization
    void Start () {

        projectileSpeed = 22.0f;
        lifeTime = 0;
        lifeTimeDuration = 3.0f;
        damage = 50.0f;
        pointsForBoss = 2000;

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
        if (Time.time > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
	
	//Algorithm controlling the detection of closest enemy target using global enemy list
	//Return the closest enemy in enemyList
	GameObject FindClosestEnemyUnit() {
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		
		foreach(GameObject enemyUnit in gameManager.Instance.enemyUnitList) {
			Vector3 diff = enemyUnit.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			
			if(curDistance < distance) {
				closestEnemyUnit = enemyUnit;
				distance = curDistance;
			}
		}
		return closestEnemyUnit;
	}

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.gameObject.tag == "Enemy" || otherObject.gameObject.tag == "Boss")
        {
            otherObject.GetComponent<Enemies>().takeDamage(damage);
            Instantiate(gameManager.Instance.rocketExplosion, transform.position, transform.rotation);
        }
    }
}
