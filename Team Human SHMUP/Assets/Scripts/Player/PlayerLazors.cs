using UnityEngine;
using System.Collections;

public class PlayerLazors : MonoBehaviour {

    public GameManager gameManager;

    private Transform myTransform;

    private float projectileSpeed = 150.0f;
    private float rotationSpeed = 10.0f;

    private GameObject closestEnemyUnit;

    private float lifeTime;
    private float lifeTimeDuration = 2.0f;

    private float damage = 50.0f;

    // Use this for initialization
    void Start()
    {
        myTransform = this.transform;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        lifeTime = Time.time + lifeTimeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //Projectile Movement
        myTransform.position += Time.deltaTime * projectileSpeed * transform.forward;

        closestEnemyUnit = FindClosestEnemyUnit();

        if (closestEnemyUnit != null)
        {
            //Smooth Lock
            //Determine the target rotation. This is the rotation if the transform looks at the target point
            Quaternion targetRotation = Quaternion.LookRotation(closestEnemyUnit.transform.position - myTransform.position);

            //Smoothly rotate towards the target point.
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Kill projectile after time
        if (Time.time > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    //Algorithm controlling the detection of closest enemy target using global enemy list
    //Return the closest enemy in enemyList
    private GameObject FindClosestEnemyUnit()
    {
        float distance = Mathf.Infinity;
        Vector3 position = myTransform.position;

        foreach (GameObject enemyUnit in gameManager.enemyUnitList)
        {
            Vector3 diff = enemyUnit.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {

                closestEnemyUnit = enemyUnit;
                distance = curDistance;
            }
        }
        return closestEnemyUnit;
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Enemy")
        {

            //otherObject.SendMessage ("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
            otherObject.GetComponent<Enemies>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
