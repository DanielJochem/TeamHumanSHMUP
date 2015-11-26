using UnityEngine;
using System.Collections;

public class PlayerMachineGun : MonoBehaviour {

    gameManager GameManager;

    private float projectileSpeed = 80.0f;

    private float lifeTime;
    private float lifeTimeDuration = 0.5f;

    private float damage = 5.0f;

    // Use this for initialization
    void Start() {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
        lifeTime = Time.time + lifeTimeDuration;
    }

    // Update is called once per frame
    void Update() {
        //Projectile Movement
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
