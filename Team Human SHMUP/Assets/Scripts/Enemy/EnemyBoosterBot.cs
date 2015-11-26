using UnityEngine;
using System.Collections;

public class EnemyBoosterBot : Enemies {

    void Start() {
        name = "Booster Bot";
        health = 100.0f;
        moveSpeed = 3.0f;
        points = 100;
        addPlayers();
    }

    void Update() {
        FollowPlayer();
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "P1Fired") {
            GameManager.p1Score += points;
        } else if (collider.gameObject.tag == "P2Fired") {
            GameManager.p2Score += points;
        }
        Destroy(this.gameObject);
    }

    public void OnTriggerExit(Collider wall) {
        if (wall.gameObject.tag == "EnemyWall") {
            Destroy(gameObject);
        }
    }
}
