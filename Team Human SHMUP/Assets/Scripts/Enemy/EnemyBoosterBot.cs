using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBoosterBot : Enemies {

    //Missile Weapon
    public GameObject rocketLauncher;
    private float rocketLauncherFireTime;
    private float rocketLauncherFireRate = 5.0f;

    //Weapon fires from here
    public GameObject muzzle;

    void Start() {
        name = "Booster Bot";
        health = 100.0f;
        moveSpeed = 3.0f;
        points = 100;
        addPlayers();
    }

    void Update() {
        FollowPlayer();
        fireRocketLauncher();
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
    }

    void fireRocketLauncher()
     {
         if (Time.time > rocketLauncherFireTime)
         {
             Instantiate(rocketLauncher, muzzle.transform.position, muzzle.transform.rotation);
             rocketLauncherFireTime = Time.time + rocketLauncherFireRate;
         }
     }

    void OnTriggerEnter(Collider collider)
    {
        whoHitMeLast = collider.gameObject.tag;
        if (collider.gameObject.tag == "P1Fired" || collider.gameObject.tag == "P2Fired")
        {
            Destroy(collider.gameObject);
        }
    }

    public void OnTriggerExit(Collider wall) {
        if (wall.gameObject.tag == "EnemyWall") {
            Destroy(gameObject);
        }
    }
}
