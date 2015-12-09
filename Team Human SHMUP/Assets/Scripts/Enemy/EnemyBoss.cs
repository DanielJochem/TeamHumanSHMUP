using UnityEngine;
using System.Collections;

public class EnemyBoss : Enemies {

    float directionZ = -1.0f;
    float directionX = -1.0f;
    public bool atPosition = false;
    public Quaternion rot;

    void Start()
    {
        name = "BOSS";
        health = 999999999.0f;
        moveSpeed = 5.0f;
    }

    void FixedUpdate()
    {
        while (!atPosition)
        {
            if (transform.position.y > 2.2f)
            {
                Vector3 newPosition = new Vector3(0, 0, directionZ * (moveSpeed * Time.deltaTime));
                newPosition = transform.position + newPosition;
            }
            else
            {
                directionZ = 1.0f;
                atPosition = true;
            }
        }

        if (atPosition)
        {
            Vector3 newPosition = new Vector3(directionX * (moveSpeed * Time.deltaTime), 0, directionZ * (moveSpeed * Time.deltaTime));
            newPosition = transform.position + newPosition;

            if (newPosition.z >= 9.5f)
            {
                directionZ = -1.0f;
            }
            else if (newPosition.z <= 0.0f)
            {
                directionZ = 1.0f;
            }

            if (newPosition.x >= 20.0f)
            {
                directionX = -1.0f;
            }
            else if (newPosition.x <= -20.0f)
            {
                directionX = 1.0f;
            }
            transform.position = newPosition;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        whoHitMeLast = collider.gameObject.tag;
        if (collider.gameObject.tag == "P1Fired" || collider.gameObject.tag == "P2Fired")
        {
            AudioManager.Instance.HitAudioSound();
            if (collider.gameObject.tag == "P1Fired")
            {
                gameManager.Instance.p1Score += collider.gameObject.GetComponent<Weapons>().pointsForBoss;
            }
            else
            {
                gameManager.Instance.p2Score += collider.gameObject.GetComponent<Weapons>().pointsForBoss;
            }
            Destroy(collider.gameObject);
        }
    }
}
