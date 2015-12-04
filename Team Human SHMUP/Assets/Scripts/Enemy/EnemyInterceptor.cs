using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInterceptor : Enemies {
    
    public Quaternion rotate;

    //Shotgun Weapon
    public GameObject shotgun;
    private float shotgunFireTime;
    private float shotgunFireRate = 1.0f;

    //Weapon fires from here
    public GameObject muzzle;

    void Start() {
        name = "Interceptor";
        health = 100.0f;
        moveSpeed = 3.0f;
        points = 45;
    }

    void Update()
    {
        FollowPlayer();
        fireShotgun();
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
    }


    void fireShotgun()
    {
        if (Time.time > shotgunFireTime)
        {
            Instantiate(shotgun, muzzle.transform.position, rotate);
            rotate.z += 20.0f;
            rotate.x += 0.1f;
            Instantiate(shotgun, muzzle.transform.position, rotate);
            rotate.z -= 40.0f;
            rotate.x -= 0.2f;
            Instantiate(shotgun, muzzle.transform.position, rotate);

            shotgunFireTime = Time.time + shotgunFireRate;
        }
    }

    void OnTriggerEnter(Collider collider) {
        whoHitMeLast = collider.gameObject.tag;
        if(collider.gameObject.tag == "P1Fired" || collider.gameObject.tag == "P2Fired")  {
            Destroy(collider.gameObject);
        }
    }

    public void OnTriggerExit(Collider wall) {
        if (wall.gameObject.tag == "EnemyWall") {
            Destroy(gameObject);
        }
    }
}
