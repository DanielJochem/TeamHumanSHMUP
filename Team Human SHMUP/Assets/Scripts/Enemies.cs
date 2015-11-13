using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour {

    public float health = 100.0f;

    public float moveSpeed = 5.0f;

    public void takeDamage(float damage) {
        health -= damage;
        print("taking damage");
    }
}
