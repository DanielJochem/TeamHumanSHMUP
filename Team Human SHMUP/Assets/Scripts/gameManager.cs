using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int innerClip = 30;

    public GameObject[] enemyUnitList;

    //For adding to score later on
    public float timeSurvivedP1;
    public float timeSurvivedP2;

    //Enemies Killed UI
    public Text p1KilledEnemiesText;
    public Text p2KilledEnemiesText;
    public int enemiesKilledP1;
    public int enemiesKilledP2;

    //Player Lives UI
    public Text p1Lives;
    public Text p2Lives;
    public int p1LivesRemaining;
    public int p2LivesRemaining;

    void Awake()
    {
        p1LivesRemaining = 3;
        p2LivesRemaining = 3;
    }

    // Update is called once per frame
    void Update() {
        //Update enemy unit list
        enemyUnitList = GameObject.FindGameObjectsWithTag("Enemy");

        //Enemies Killed
        p1KilledEnemiesText.text = "P1 Enemies Killed: " + enemiesKilledP1;
        p2KilledEnemiesText.text = "P2 Enemies Killed: " + enemiesKilledP2;

        //Player Lives
        p1Lives.text = "P1 Lives Remaining: " + p1LivesRemaining;
        p2Lives.text = "P2 Lives Remaining: " + p2LivesRemaining;
    }
}

