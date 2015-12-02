using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDrone : Enemies {

    //Lazor Weapon
    public GameObject machineGun;
    private float machineGunFireTime;
    private float machineGunFireRate = 0.2f;

    //Weapon fires from here
    public GameObject muzzle;

    void Start() {
        name = "Drone";
        health = 20.0f;
        moveSpeed = 3.0f;
        points = 10;
        addPlayers();
    }

    void Update() {
        FollowPlayer();
        fireMachineGun();
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
    }

    void fireMachineGun()
    {
        if (Time.time > machineGunFireTime) {
            Instantiate(machineGun, muzzle.transform.position, muzzle.transform.rotation);
            machineGunFireTime = Time.time + machineGunFireRate;
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
