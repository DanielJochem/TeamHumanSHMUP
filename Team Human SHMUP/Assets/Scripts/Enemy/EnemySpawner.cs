using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
    public Enemies enemyManager;

    public GameObject[] enemies;
    public List<GameObject> currentEnemies = new List<GameObject>();
    public GameObject spawningEnemy = null;
    public GameObject spawnedEnemy = null;

    public float delay = 0.0f;
    public float spawnTime = 60.0f;

    public int bossPhaseNumber = 8;
    public GameObject boss;

    public int currentPhase = 0;

    public Transform spawner;
    public Quaternion rotation = Quaternion.identity;

    public int moveSpeed = 100;  //per second
    Vector3 direction = Vector3.left;

    void Awake() {
        spawner.position = this.transform.position;
    }

    void FixedUpdate() {
        CheckSpawnDelay();
        MoveSpawner();
        CheckPhase();
    }

    void MoveSpawner() {
        Vector3 newPosition = direction * (moveSpeed * Time.deltaTime);
        newPosition = transform.position + newPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -27.5f, 50.5f);
        transform.position = newPosition;
        if (newPosition.x >= 50.5f) {
            direction = Vector3.left;
        } else if (newPosition.x <= -27.5f)  {
            direction = Vector3.right;
        }
    }

    void CheckPhase() {
        if (gameManager.Instance.timer <= 0 && (gameManager.Instance.playerOneDead == false || gameManager.Instance.playerTwoDead == false))  {
            gameManager.Instance.timer = 27.0f;
            currentEnemies.RemoveRange(0, currentEnemies.Count);
            if (currentPhase < bossPhaseNumber) {
                currentPhase++;
                if (currentPhase == bossPhaseNumber) {
                    gameManager.Instance.phaseLevel = 0;
                    gameManager.Instance.boss.gameObject.SetActive(true);
                } else {
                    gameManager.Instance.phaseLevel++;
                    NewPhaseAddEnemies();
                    Debug.Log("Phase: " + currentPhase + " Activated!");
                }
            }
        }
    }

    void CheckSpawnDelay() {
        delay++;
        if (delay > spawnTime) {
            if (gameManager.Instance.enemiesAlive < 10 && gameManager.Instance.phase.text != gameManager.Instance.phaseBossLevel) {
                int spawnRandom = Random.Range(1, (currentEnemies.Count + 1));
                if (spawnRandom == 1) {
                    spawningEnemy = currentEnemies[0];
                    spawnedEnemy = Instantiate(spawningEnemy, new Vector3(spawner.position.x, spawningEnemy.transform.position.y, spawner.position.z), rotation) as GameObject;
                } else if (spawnRandom == 2) {
                    spawningEnemy = currentEnemies[1];
                    spawnedEnemy = Instantiate(spawningEnemy, new Vector3(spawner.position.x, spawningEnemy.transform.position.y, spawner.position.z), rotation) as GameObject;
                } else if (spawnRandom == 3)  {
                    spawningEnemy = currentEnemies[2];
                    spawnedEnemy = Instantiate(spawningEnemy, new Vector3(spawner.position.x, spawningEnemy.transform.position.y, spawner.position.z), rotation) as GameObject;
                }
                gameManager.Instance.enemiesAlive++;
            }
            delay = 0;
        }
    }

    void NewPhaseAddEnemies()
    {
        if (currentPhase == 1)
        {
            currentEnemies.Add(enemies[0]);
        }
        else if (currentPhase == 2)
        {
            currentEnemies.Add(enemies[1]);
        }
        else if (currentPhase == 3)
        {
            currentEnemies.Add(enemies[0]);
            currentEnemies.Add(enemies[1]);
        }
        else if (currentPhase == 4)
        {
            currentEnemies.Add(enemies[2]);
        }
        else if (currentPhase == 5)
        {
            currentEnemies.Add(enemies[0]);
            currentEnemies.Add(enemies[2]);
        }
        else if (currentPhase == 6)
        {
            currentEnemies.Add(enemies[1]);
            currentEnemies.Add(enemies[2]);
        }
        else if (currentPhase == 7)
        {
            currentEnemies.Add(enemies[0]);
            currentEnemies.Add(enemies[1]);
            currentEnemies.Add(enemies[2]);
        }
    }
}
