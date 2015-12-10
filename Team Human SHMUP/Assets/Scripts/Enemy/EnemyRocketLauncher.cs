using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRocketLauncher : Enemies {

    private GameObject closest;
    public List<GameObject> players = new List<GameObject>();
    public float rotation = 10.0f;

    // Use this for initialization
    void Start()
    {
        players.Add(GameObject.FindGameObjectWithTag("Player 1"));
        players.Add(GameObject.FindGameObjectWithTag("Player 2"));

        projectileSpeed = 8.0f;
        projectileLifeTime = 0.0f;
        projectileLifeTimeDuration = 5.0f;
        projectileDamage = 5;

        projectileLifeTime = Time.time + projectileLifeTimeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //Projectile Movement
        transform.position += Time.deltaTime * projectileSpeed * transform.forward;

        closest = FindClosest();

        if (closest != null)
        {
            //Smooth Lock
            //Determine the target rotation. This is the rotation if the transform looks at the target point
            Quaternion targetRotation = Quaternion.LookRotation(closest.transform.position - transform.position);

            //Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotation * Time.deltaTime);
        }

        //Kill projectile after time
        if (Time.time > projectileLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    //Algorithm controlling the detection of closest enemy target using global enemy list
    //Return the closest enemy in enemyList
    GameObject FindClosest()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject player in players)
        {
            Vector3 diff = player.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = player;
                distance = curDistance;
            }
        }
        return closest;
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Player 1")
        {
            Instantiate(gameManager.Instance.rocketExplosion, transform.position, transform.rotation);
            otherObject.GetComponent<PlayerManagement>().takeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
        else if (otherObject.tag == "Player 2")
        {
            Instantiate(gameManager.Instance.rocketExplosion, transform.position, transform.rotation);
            otherObject.GetComponent<PlayerManagement>().takeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
    }
}
