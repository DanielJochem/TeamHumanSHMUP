using UnityEngine;
using System.Collections;

public class EnemyInterceptor : Enemies {

    Vector3 direction = Vector3.left;
    Vector3 moveForward;

    void Start() {
        name = "Interceptor";
        health = 100.0f;
        moveSpeed = 20.0f;
        points = 45;
        addPlayers();

        moveForward = transform.position;
    }


    void FixedUpdate()
    {
        Vector3 newPosition = direction * (moveSpeed * Time.deltaTime);
        newPosition = transform.position + newPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -27.5f, 50.5f);
        transform.position = newPosition;
        if (newPosition.x >= 50.5f)
        {
            moveForward.z = transform.position.z + 5;
            direction = Vector3.left;
        }
        else if (newPosition.x <= -27.5f)
        {
            moveForward.z = transform.position.z + 5;
            direction = Vector3.right;
        }
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
