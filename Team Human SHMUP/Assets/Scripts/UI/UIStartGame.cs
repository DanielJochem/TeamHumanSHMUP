using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStartGame : MonoBehaviour {

    gameManager GameManager;

    public UIDisplay score;

    public GameObject[] findEnemies;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject closePlane;
    public GameObject farPlane;
    public GameObject spaceSpawners;

    void Awake() {

        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();

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
    }

    public void OnClick_StartGame() {

        GameManager.ClickMe();

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

        //Turn score on
        score.ScoreOn();

        //Activate enemies once again, will change to activate spawners instead later
        foreach (GameObject enemies in findEnemies) {
            enemies.gameObject.SetActive(true);
        }
    }

    public void OnClick_QuitGame() {
        Application.Quit();
    }
}
