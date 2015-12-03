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
}