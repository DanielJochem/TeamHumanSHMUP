using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class gameManager : SingletonBehaviour<gameManager>
{
    public UIGameOver gameOver;
    public EnemyBoss enemyBoss;

    //public GameObject[] enemyUnitList;
    public GameObject[] enemyUnitList;
    public List<GameObject> players = new List<GameObject>();

    public int enemiesAlive;

    public float timer = 25.0f;
    public bool timeStarted = false;

    //For adding to score later on
    public float timeSurvivedP1;
    public float timeSurvivedP2;
    public int prevTimeP1;
    public int prevTimeP2;

    //Enemies Killed UI
    public Text p1ScoreText;
    public Text p2ScoreText;
    public int p1Score = 0;
    public int p2Score = 0;

    //Player Lives UI
    public Text p1Lives;
    public Text p2Lives;
    public int p1LivesRemaining;
    public int p2LivesRemaining;

    //Player Health UI
    public Text p1Health;
    public Text p2Health;
    public int p1HealthRemaining;
    public int p2HealthRemaining;
    public bool playerOneDead = false;
    public bool playerTwoDead = false;

    //Only display final score on player that died until both players are dead
    public Text p1FinalScore;
    public Text p2FinalScore;

    //Current phase activated
    public Text phase;
    public int phaseLevel = 0;
    public float radioactivityLevel = 0.0f;
    public string phaseBossLevel;

    public GameObject boss;

    //Rocket Launcher Explosion
    public GameObject rocketExplosion;

    void Start()
    {
        p1LivesRemaining = 3;
        p2LivesRemaining = 3;

        players.Add(GameObject.FindGameObjectWithTag("Player 1"));
        players.Add(GameObject.FindGameObjectWithTag("Player 2"));

        p1HealthRemaining = 100;
        p2HealthRemaining = 100;

        enemiesAlive = 0;

        //Find and deactivate boss
        boss = GameObject.Find("Boss");
        boss.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update() {
        //Update enemy unit list
        enemyUnitList = GameObject.FindGameObjectsWithTag("Enemy");

        //Enemies Killed
        p1ScoreText.text = "Score: " + p1Score;
        p2ScoreText.text = "Score: " + p2Score;

        //Player Lives
        p1Lives.text = "Lives: " + p1LivesRemaining;
        p2Lives.text = "Lives: " + p2LivesRemaining;

        //Player Health
        p1Health.text = "Health: " + p1HealthRemaining;
        p2Health.text = "Health: " + p2HealthRemaining;

        //Player Final Score
        p1FinalScore.text = "Final Score: " + ((p1Score + (Mathf.Floor(timeSurvivedP1))) - prevTimeP1);
        p2FinalScore.text = "Final Score: " + ((p2Score + (Mathf.Floor(timeSurvivedP2))) - prevTimeP2);

        if (timeStarted) {
            timer -= Time.deltaTime;
        }

        //Phase level
        if(phaseLevel != 0) {
            phase.text = "Phase " + phaseLevel;
        } else {
            if (radioactivityLevel <= 100.0f) {
                phaseBossLevel = "DANGER! Radioactivity at: " + Mathf.Floor(radioactivityLevel) + " Percent";
                phase.text = phaseBossLevel;
                radioactivityLevel += Time.deltaTime * 2;
            } else if (radioactivityLevel > 100.0f) {
                radioactivityLevel = 100.0f;
                enemyBoss.ExplodeBoss();
                Destroy(enemyBoss.gameObject);
                gameOver.restart.SetActive(true);
                foreach(GameObject enemies in enemyUnitList)
                {
                    Destroy(enemies.gameObject);
                }
                if(GameObject.FindGameObjectWithTag("Player 1") != null)
                {
                    Destroy(GameObject.FindGameObjectWithTag("Player 1").gameObject);
                }
                if (GameObject.FindGameObjectWithTag("Player 2") != null)
                {
                    Destroy(GameObject.FindGameObjectWithTag("Player 2").gameObject);
                }
            }
        }

        if (playerOneDead == true)
        {
            p1ScoreText.gameObject.SetActive(false);
            p1Lives.gameObject.SetActive(false);
            p1Health.gameObject.SetActive(false);
            p1FinalScore.gameObject.SetActive(true);
        }

        if (playerTwoDead == true) {
            p2ScoreText.gameObject.SetActive(false);
            p2Lives.gameObject.SetActive(false);
            p2Health.gameObject.SetActive(false);
            p2FinalScore.gameObject.SetActive(true);
        }

        if (playerOneDead == true && playerTwoDead == true)
        {
            gameOver.restart.SetActive(true);
        }
    }

    public void ClickMe()
    {
        AudioManager.Instance.ClickButtonSound();
    }

    public void ExplodeMe()
    {
        AudioManager.Instance.ExplosionAudioSound();
    }
}

