using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIStartGame : MonoBehaviour {

    public UIDisplay score;
    public UIInstructions instructions;

    public GameObject[] findEnemies;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject closePlane;
    public GameObject farPlane;
    public GameObject spaceSpawners;
    public GameObject enemySpawner;

    public Text p1NameText;
    public Text p2NameText;
    public string p1Name = "";
    public string p2Name = "";
    public Text p1NameDisplay;
    public Text p2NameDisplay;

    void Awake() {
        //Find all enemies on screen and deactivate them
        findEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemies in findEnemies) {
            enemies.gameObject.SetActive(false);
        }
       
        //Find and deactivate players
        playerOne = GameObject.FindGameObjectWithTag("Player 1");
        playerTwo = GameObject.FindGameObjectWithTag("Player 2");
        playerOne.gameObject.SetActive(false);
        playerTwo.gameObject.SetActive(false);

        spaceSpawners = GameObject.FindGameObjectWithTag("SpaceSpawners");
        spaceSpawners.SetActive(false);

        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawner.SetActive(false);
    }

    void Update()
    {
        p1Name = p1NameText.text;
        p2Name = p2NameText.text;
    }

    public void OnClick_StartGame() {
        if(p1Name != "" && p2Name != "")
        {
            gameManager.Instance.ClickMe();

            p1NameDisplay.text = p1Name + ":";
            p2NameDisplay.text = p2Name + ":";

            //Activate players
            playerOne.gameObject.SetActive(true);
            playerTwo.gameObject.SetActive(true);

            //Reset background speed
            closePlane.GetComponent<MovingBackground>().scrollSpeed = 5;
            farPlane.GetComponent<MovingBackground>().scrollSpeed = 5;

            //This UI element is now inactive
            this.gameObject.SetActive(false);

            //Turn falling space object spawners on
            spaceSpawners.SetActive(true);
            enemySpawner.SetActive(true);

            //Turn score on
            score.ScoreOn();

            gameManager.Instance.timeStarted = true;
            gameManager.Instance.radioactivityLevel = 0.0f;

            //Activate enemies once again, will change to activate spawners instead later
            foreach (GameObject enemies in findEnemies)
            {
                enemies.gameObject.SetActive(true);
            }

            gameManager.Instance.prevTimeP1 = (int)Time.time;
            gameManager.Instance.prevTimeP2 = (int)Time.time;
        }
    }

    public void OnClick_Instructions() {
        gameManager.Instance.ClickMe();
        this.gameObject.SetActive(false);
        instructions.gameObject.SetActive(true);
    }

    public void OnClick_QuitGame() {
        gameManager.Instance.ClickMe();
        Application.Quit();
    }
}
