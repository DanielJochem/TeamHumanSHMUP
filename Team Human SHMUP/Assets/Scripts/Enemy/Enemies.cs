using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies : MonoBehaviour {

    public string name = "";
    public float health;
    public float moveSpeed;
    public int points;

    public GameObject closestPlayer;
    public string whoHitMeLast = "";

    //Rotation Vars
    private float rotationSpeed = 2.0f;
    float rotateSpeed;
    private Quaternion targetRotation;

    protected float projectileSpeed;
    protected float projectileLifeTime;
    protected float projectileLifeTimeDuration;
    protected int projectileDamage;

    //Players
    public GameObject player1; 
    public GameObject player2;

    //For later use
    //public GameObject deathExplosion;

    void Start() {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    // Update is called once per frame
    void Update() {
        //Kill Check
        if (health <= 0) {
            Destroy(this.gameObject);

            //For later use
            //Instantiate(deathExplosion, transform.position, transform.rotation);
        }
    }

    public void FindClosestPlayer() {
        if (player1 != null && player2 != null) {
            if (Vector3.Distance(transform.position, player1.transform.position) <= Vector3.Distance(transform.position, player2.transform.position)) {
                closestPlayer = player1;
            } else {
                closestPlayer = player2;
            }

        } else if (player1 != null && player2 == null) {
            closestPlayer = player1;

        } else if (player1 == null && player2 != null) {
            closestPlayer = player2;

        } else {
            closestPlayer = null;
        }
    }

    public void FollowPlayer()
    {
        FindClosestPlayer();
        if (!(closestPlayer != null) || closestPlayer.transform.position.z >= transform.position.z)
        {
            return;
        }

        targetRotation = Quaternion.LookRotation(closestPlayer.transform.position - transform.position);
        rotateSpeed = Mathf.Min(rotationSpeed * Time.deltaTime, 1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed);
    }

    public void takeDamage(float damage) {
        AudioManager.Instance.HitAudioSound();
        health -= damage;
        if(health <= 0) {
            AudioManager.Instance.ExplosionAudioSound();
            if (whoHitMeLast == "P1Fired") {
                gameManager.Instance.p1Score += points;
            } else {
                gameManager.Instance.p2Score += points;
            }

            gameManager.Instance.enemiesAlive--;
            Destroy(this.gameObject);
        }
    }
}
