using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBoosterBot : Enemies {

    Vector3 direction = Vector3.left;
    Vector3 prevDirection = Vector3.left;
    
    public bool down = false;
    public int goneDown;

    //Rocket Launcher Weapon
    public GameObject rocketLauncher;
    private float rocketLauncherFireTime;
    private float rocketLauncherFireRate = 5.0f;

    //Weapon fires from here
    public GameObject muzzle;

    void Start() {
        name = "Booster Bot";
        health = 50.0f;
        moveSpeed = 15.0f;
        points = 1000;
    }

    void FixedUpdate() {
        if (!down)
        {
            Vector3 newPosition = direction * (moveSpeed * Time.deltaTime);
            newPosition = transform.position + newPosition;
            newPosition.x = Mathf.Clamp(newPosition.x, -27.5f, 50.5f);

            if (newPosition.x >= 50.5f)
            {
                down = true;
                prevDirection = Vector3.left;
            }
            else if (newPosition.x <= -27.5f)
            {
                down = true;
                prevDirection = Vector3.right;
            }
            transform.position = newPosition;
        }
        else
        {
            direction = Vector3.back;
            Vector3 newPosition = direction * (moveSpeed * Time.deltaTime);
            newPosition = transform.position + newPosition;
            transform.position = newPosition;
            goneDown += 1;

            if (goneDown == 10)
            {
                down = false;
                direction = prevDirection;
                goneDown = 0;
            }
        }
        fireRocketLauncher();
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
            gameManager.Instance.enemiesAlive--;
            Destroy(gameObject);
        }
    }
}
