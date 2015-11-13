using UnityEngine;
using System.Collections;

public class PlayerLazors : MonoBehaviour {

    gameManager GameManager;

    private float projectileSpeed = 500.0f;

    private float lifeTime;
    private float lifeTimeDuration = 0.8f;

    private float damage = 20.0f;

    // Use this for initialization
    void Start() {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
        lifeTime = Time.time + lifeTimeDuration;
    }

    // Update is called once per frame
    void Update() {
        //Movement
        transform.position += Time.deltaTime * projectileSpeed * transform.forward;

        //Kill projectile after time
        if (Time.time > lifeTime) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Enemy") {
            collider.GetComponent<Enemies>().takeDamage(damage);
            Destroy(this.gameObject);

            if (collider.GetComponent<Enemies>().health <= 0) {
                if (this.gameObject.tag == "P1Fired") {
                    GameManager.enemiesKilledP1++;
                } else if (this.gameObject.tag == "P2Fired") {
                    GameManager.enemiesKilledP2++;
                }
            }
        }
    }
}
