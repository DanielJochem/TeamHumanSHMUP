using UnityEngine;
using System.Collections;

public class UIInstructions : MonoBehaviour
{
    public UIStartGame startGame;

    // Use this for initialization
    void Start() {
        this.gameObject.SetActive(false);
    }

    public void OnClick_GoBack() {
        gameManager.Instance.ClickMe();
        startGame.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
