using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {

    public GameObject display;
    public Text finalScoreP1;
    public Text finalScoreP2;

    void Awake () {
        //Turn off Score
        display = this.gameObject;
        display.SetActive(false);
    }
	
    //Turn on Score
    public void ScoreOn()
    {
        display.SetActive(true);
        //finalScoreP1.gameObject.SetActive(false);
       // finalScoreP2.gameObject.SetActive(false);
    }
}
