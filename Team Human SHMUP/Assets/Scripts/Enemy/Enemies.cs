using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies : MonoBehaviour {
    
    public gameManager GameManager;

    public string name = "";
    public float health;
    public float moveSpeed;
    public int points;

    public GameObject closestPlayer;
    public List<GameObject> players = new List<GameObject>();
    public string whoHitMeLast = "";

    //Rotation Vars
    private float rotationSpeed = 2.0f;
    float rotateSpeed;
    private Quaternion targetRotation;

    protected float projectileSpeed;
    protected float projectileLifeTime;
    protected float projectileLifeTimeDuration;
    protected int projectileDamage;

    //For later use
    //public GameObject deathExplosion;

    void Start() {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
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

    public void addPlayers()
    {
        players.Add(GameObject.FindGameObjectWithTag("Player 1"));
        players.Add(GameObject.FindGameObjectWithTag("Player 2"));
    }

    public void FindClosestPlayer()
    {
        for (int index = 0; index < players.Count; index++)
        {
            if (players[index] != null)
            {
                if (index == 0)
                {
                    closestPlayer = players[0];
                }

                if (Vector3.Distance(transform.position, players[index].transform.position) <= Vector3.Distance(transform.position, closestPlayer.transform.position))
                {
                    closestPlayer = players[index];
                }
            }
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
                GameManager.p1Score += points;
            } else {
                GameManager.p2Score += points;
            }
            
            Destroy(this.gameObject);
        }
    }
}
