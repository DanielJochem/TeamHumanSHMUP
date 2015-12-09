using UnityEngine;
using System.Collections;

public class PlayerMachineGun : Weapons {

    void Start() {
        projectileSpeed = 80.0f;
        lifeTime = 0;
        lifeTimeDuration = 0.5f;
        damage = 5.0f;
        pointsForBoss = 5;

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
