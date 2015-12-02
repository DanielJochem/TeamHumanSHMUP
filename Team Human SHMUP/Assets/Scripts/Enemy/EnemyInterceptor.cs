﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInterceptor : Enemies {

    Vector3 direction = Vector3.left;
    Vector3 moveForward;
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
        moveSpeed = 20.0f;
        points = 45;
        addPlayers();

        moveForward = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = direction * (moveSpeed * Time.deltaTime);
        newPosition = transform.position + newPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -27.5f, 50.5f);
        transform.position = newPosition;
        if (newPosition.x >= 50.5f)
        {
            moveForward.z = transform.position.z + 5;
            direction = Vector3.left;
        }
        else if (newPosition.x <= -27.5f)
        {
            moveForward.z = transform.position.z + 5;
            direction = Vector3.right;
        }

        fireShotgun();
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
