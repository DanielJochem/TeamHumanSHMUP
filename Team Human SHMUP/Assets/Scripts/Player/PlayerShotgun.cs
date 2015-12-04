using UnityEngine;
using System.Collections;

public class PlayerShotgun : Weapons {

    void Start() {
        projectileSpeed = 50.0f;
        lifeTime = 0;
        lifeTimeDuration = 1.0f;
        damage = 20.0f;

        lifeTime = Time.time + lifeTimeDuration;
    }

    void Update()
    {
        //Movement
        this.transform.position += Time.deltaTime * projectileSpeed * this.transform.forward;

        //Kill projectile after time
        if (Time.time > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
