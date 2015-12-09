using UnityEngine;
using System.Collections;

public class EnemyShotgun : Enemies {

    void Start() {
        projectileSpeed = 40.0f;
        projectileLifeTime = 0.0f;
        projectileLifeTimeDuration = 1.0f;
        projectileDamage = 5;

        projectileLifeTime = Time.time + projectileLifeTimeDuration;
    }

    void Update()
    {
        //Movement
        this.transform.position += Time.deltaTime * projectileSpeed * -this.transform.forward;

        //Kill projectile after time
        if (Time.time > projectileLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Player 1")
        {
            otherObject.GetComponent<PlayerManagement>().takeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
        else if (otherObject.tag == "Player 2")
        {
            otherObject.GetComponent<PlayerManagement>().takeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
    }
}
