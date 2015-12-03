using UnityEngine;
using System.Collections;

public class PlayerRocketLauncher : Weapons {

    private GameObject closestEnemyUnit;
    public float rotationSpeed = 10.0f;
    
    // Use this for initialization
    void Start () {

        projectileSpeed = 10.0f;
        lifeTime = 0;
        lifeTimeDuration = 5.0f;
        damage = 50.0f;
        
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
}
