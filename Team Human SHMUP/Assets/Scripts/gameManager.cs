using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int innerClip = 30;

    public GameObject[] enemyUnitList;

    public float timeSurvivedP1;
    public float timeSurvivedP2;

    //New UI
    public Text p1KilledEnemiesText;
    public Text p2KilledEnemiesText;
    public int enemiesKilledP1;
    public int enemiesKilledP2;

    // Update is called once per frame
    void Update() {
        //Update enemy unit list
        enemyUnitList = GameObject.FindGameObjectsWithTag("Enemy");

        //Enemies Killed
        p1KilledEnemiesText.text = "Enemies Killed P1: " + enemiesKilledP1;
        p2KilledEnemiesText.text = "Enemies Killed P2: " + enemiesKilledP2;
    }
}

