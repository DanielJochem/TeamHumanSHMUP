using UnityEngine;
using System.Collections;

public class EnemyDrone : Enemies {

    void Start() {
        name = "Drone";
        health = 100.0f;
        moveSpeed = 3.0f;
        addPlayers();
    }

    public void OnTriggerExit(Collider wall) {
        if (wall.gameObject.tag == "EnemyWall") {
            Destroy(gameObject);
        }
    }
}
