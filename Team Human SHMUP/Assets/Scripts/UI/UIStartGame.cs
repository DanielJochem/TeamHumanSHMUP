using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStartGame : MonoBehaviour {

    public UIDisplay score;

    public GameObject[] findEnemies;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject closePlane;
    public GameObject farPlane;

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
    }

    public void OnClick_StartGame() {
        //Activate players
        playerOne.gameObject.SetActive(true);
        playerTwo.gameObject.SetActive(true);

        //Reset background speed
        closePlane.GetComponent<MovingBackground>().scrollSpeed = 5;
        farPlane.GetComponent<MovingBackground>().scrollSpeed = 5;

        //This UI element is now inactive
        this.gameObject.SetActive(false);

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
