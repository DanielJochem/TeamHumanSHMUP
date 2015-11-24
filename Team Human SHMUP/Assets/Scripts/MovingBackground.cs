using UnityEngine;
using System.Collections;

public class MovingBackground : MonoBehaviour {

    public float scrollSpeed = 5.0f;
    private Vector2 scroll;

    void FixedUpdate()  {
        //Increase the speed of the screen scrolling.
        if ((Mathf.Abs(GetComponent<Renderer>().material.mainTextureOffset.y) % 1.0f) >= 0.004f) {
            //Will reach scrollSpeed 15 at 3min 20sec
            scrollSpeed += 0.001f;
        }

        scroll.y = -(Time.time * scrollSpeed) / 100.0f;
        GetComponent<Renderer>().material.mainTextureOffset = scroll;
    }

    /*void FixedUpdate() {
        //Increase the speed of the screen scrolling.

        if ((Mathf.Abs(GetComponent<Renderer>().material.mainTextureOffset.y) % 1.0f) >= 0.004f) {
            if (!reversed) {
                scrollSpeed += 0.002f;
            } else {
                scrollSpeed -= 0.002f;
            }
        }

        if (scrollSpeed < 15.0f) {
            if (!reversed) {
                scroll.y = (-(Time.time) * scrollSpeed) / 100.0f;
            } else {
                scroll.y = (((-(Time.time) * scrollSpeed) / 100.0f) + -(80 * (Time.time / 1000.0f) + (Time.time / 30.0f)) / 10) * 5;
            }
        } else {
            scrollSpeed = 15.0f;
        }

        if (!secondTimeRound) {
            if (scrollSpeed >= 10.0f) {
                reversed = true;
            } else if (scrollSpeed <= 8.0f && reversed) {
                reversed = false;
                secondTimeRound = true;
            }
        }

        GetComponent<Renderer>().material.mainTextureOffset = scroll;
        //Debug.Log(-(Time.time * scrollSpeed) / 100.0f);
    }*/
}