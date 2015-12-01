using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIGameOver : MonoBehaviour {

    gameManager GameManager;

    public GameObject restart;
    public List<Text> gameOverMessages = new List<Text>();

    void Awake() {

        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();

        restart = this.gameObject;

        //Allocate what GameOver message is going to display when the game is over.
        foreach (Text messages in gameOverMessages) {
            messages.gameObject.SetActive(false);
        }

        int messageNum = Random.Range(0, gameOverMessages.Count);
        gameOverMessages[messageNum].gameObject.SetActive(true);
        
        restart.SetActive(false);
    }

    public void OnClick_RestartGame() {
        GameManager.ClickMe();

        Application.LoadLevel(Application.loadedLevel);
        restart.SetActive(false);
    }

    public void OnClick_QuitGame()
    {
        Application.Quit();
    }
}
