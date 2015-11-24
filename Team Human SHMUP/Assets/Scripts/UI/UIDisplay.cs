using UnityEngine;
using System.Collections;

public class UIDisplay : MonoBehaviour {

    public GameObject display;

    void Awake () {
        //Turn off Score
        display = this.gameObject;
        display.SetActive(false);
    }
	
    //Turn on Score
    public void ScoreOn()
    {
        display.SetActive(true);
    }
}
