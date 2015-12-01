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
        health = 100.0f;
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
        Debug.Log("Entered");
        if (collider.gameObject.tag == "P1Fired")
        {
            Debug.Log("P1");
            GameManager.p1Score += points;
            Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.tag == "P2Fired")
        {
            Debug.Log("P2");
            GameManager.p2Score += points;
            Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerExit(Collider wall) {
        if (wall.gameObject.tag == "EnemyWall") {
            Destroy(gameObject);
        }
    }
}
