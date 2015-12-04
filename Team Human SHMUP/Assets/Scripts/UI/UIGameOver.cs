using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIGameOver : MonoBehaviour {
    public GameObject restart;
    public PlayerManagement playerManagement;
    public List<Text> gameOverMessages = new List<Text>();

    void Awake() {

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
        gameManager.Instance.ClickMe();

        Application.LoadLevel(Application.loadedLevel);
        restart.SetActive(false);
    }

    public void OnClick_QuitGame()
    {
        gameManager.Instance.ClickMe();
        Application.Quit();
    }
}
