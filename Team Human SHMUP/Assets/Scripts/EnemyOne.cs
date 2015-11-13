using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyOne : Enemies {

    gameManager GameManager;

    float rotateSpeed;

    public GameObject closestPlayer;
    public List<GameObject> players = new List<GameObject>();

    //Rotation Vars
    private float rotationSpeed = 2.0f;
    private float adjRotSpeed;
    private Quaternion targetRotation;

    //For later use
    //public GameObject deathExplosion;

    // Use this for initialization
    void Start() {
        players.Add(GameObject.FindGameObjectWithTag("Player 1"));
        players.Add(GameObject.FindGameObjectWithTag("Player 2"));

        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update() {
        FollowPlayer();
        transform.position += Time.deltaTime * moveSpeed * transform.forward;

        //Kill Check
        if (health <= 0) {
            Destroy(this.gameObject);
            //For later use
            //Instantiate(deathExplosion, transform.position, transform.rotation);
        }
    }

    void FindClosestPlayer() {
        for (int index = 0; index < players.Count; index++) {
            if (players[index] != null)  {
                if (index == 0) {
                    closestPlayer = players[0];
                }

                if (Vector3.Distance(transform.position, players[index].transform.position) <= Vector3.Distance(transform.position, closestPlayer.transform.position)) {
                    closestPlayer = players[index];
                }
            }
        }
    }

    void FollowPlayer() {
        FindClosestPlayer();
        if (!(closestPlayer != null) || closestPlayer.transform.position.z >= transform.position.z) {
            return;
        }

        targetRotation = Quaternion.LookRotation(closestPlayer.transform.position - transform.position);
        rotateSpeed = Mathf.Min(rotationSpeed * Time.deltaTime, 1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed);
    }
}
