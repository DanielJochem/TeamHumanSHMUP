using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRocketLauncher : Enemies {

    gameManager GameManager;

    private GameObject closestPlayer;
    public float rotationSpeed = 10.0f;

    // Use this for initialization
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();

        projectileSpeed = 10.0f;
        projectileLifeTime = 0.0f;
        projectileLifeTimeDuration = 5.0f;
        projectileDamage = 50;

        projectileLifeTime = Time.time + projectileLifeTimeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //Projectile Movement
        transform.position += Time.deltaTime * projectileSpeed * transform.forward;

        closestPlayer = FindClosestPlayer();

        if (closestPlayer != null)
        {
            //Smooth Lock
            //Determine the target rotation. This is the rotation if the transform looks at the target point
            Quaternion targetRotation = Quaternion.LookRotation(closestPlayer.transform.position - transform.position);

            //Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Kill projectile after time
        if (Time.time > projectileLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    //Algorithm controlling the detection of closest enemy target using global enemy list
    //Return the closest enemy in enemyList
    GameObject FindClosestPlayer()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject player in GameManager.players)
        {
            Vector3 diff = player.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closestPlayer = player;
                distance = curDistance;
            }
        }
        return closestPlayer;
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Player 1")
        {
            otherObject.GetComponent<PlayerManagement>().takeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
        else if (otherObject.tag == "Player 2")
        {
            otherObject.GetComponent<PlayerManagement>().takeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
    }
}
