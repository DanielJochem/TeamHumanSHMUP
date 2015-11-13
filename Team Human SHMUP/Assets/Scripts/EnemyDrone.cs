using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDrone : Enemies {

    void Start()
    {
        name = "Drone";
        health = 100.0f;
        moveSpeed = 5.0f;
        addPlayers();
    }
}
