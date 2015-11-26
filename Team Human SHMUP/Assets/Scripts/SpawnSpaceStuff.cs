using UnityEngine;
using System.Collections;

public class SpawnSpaceStuff : MonoBehaviour
{
    public GameObject[] prefeb;
    public GameObject currentObject = null;
    public float delay = 0.0f;
    public float spawnTime = 500.0f;

    public Transform spawner;
    public Vector3 fall;
    public Quaternion rotation = Quaternion.identity;

    public float randFallSpeed;
    public float fallSpeedMin = 4.0f;
    public float fallSpeedMax = 20.0f;

    public int moveSpeed = 10;  //per second
    Vector3 direction = Vector3.left;

    void Awake() {
        spawner.position = this.transform.position;
    }

    void FixedUpdate() {
        delay++;

        if(delay > spawnTime) {
            if (currentObject != null) {
                Destroy(currentObject.gameObject);
            }

            delay = 0;
            Spawn();
        }

        if(currentObject != null) {
            fall.z = -(((delay/50) * randFallSpeed) - spawner.position.z);
            currentObject.transform.position = fall;
        }

        Vector3 newPosition = direction * (moveSpeed * Time.deltaTime);
        newPosition = transform.position + newPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -27.5f, 50.5f);
        transform.position = newPosition;
        if (newPosition.x >= 50.5f)
        {
            direction = Vector3.left;
        }
        else if (newPosition.x <= -27.5f)
        {
            direction = Vector3.right;
        }
    }

    public void Spawn() {
        int prefeb_num = Random.Range(0, 12);
        float randRotation = Random.Range(-360.0f, 360.0f);
        randFallSpeed = Random.Range(fallSpeedMin, fallSpeedMax);

        fall.x = spawner.position.x;
        fall.z = spawner.position.z;
        fall.y = 0.1f;

        rotation.eulerAngles = new Vector3(transform.rotation.x, randRotation, transform.rotation.z);
        currentObject = (GameObject)Instantiate(prefeb[prefeb_num], new Vector3(spawner.position.x, 0.1f, spawner.position.z), rotation);
    }
}
