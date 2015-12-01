using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRocketLauncher : MonoBehaviour {

    gameManager GameManager;

    public float projectileSpeed;
    public float projectileLifeTime;
    public float projectileLifeTimeDuration;
    public float projectileDamage;

    private GameObject closestPlayer;
    public float rotationSpeed = 10.0f;

    // Use this for initialization
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();

        projectileSpeed = 10.0f;
        projectileLifeTime = 0.0f;
        projectileLifeTimeDuration = 5.0f;
        projectileDamage = 50.0f;

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
}
