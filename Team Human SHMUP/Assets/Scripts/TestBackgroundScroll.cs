using UnityEngine;
using System.Collections;

public class TestBackgroundScroll : MonoBehaviour {

    public float scrollSpeed = 5.0f;
    private Vector2 scroll;

    void FixedUpdate()  {
        //Increase the speed of the screen scrolling.
        if ((Mathf.Abs(GetComponent<Renderer>().material.mainTextureOffset.y) % 1.0f) >= 0.004f) {
            scrollSpeed += 0.002f;
        }

        scroll.y = -(Time.time * scrollSpeed) / 100.0f;
        GetComponent<Renderer>().material.mainTextureOffset = scroll;
        Debug.Log(GetComponent<Renderer>().material.mainTextureOffset + " + " + (Mathf.Abs(GetComponent<Renderer>().material.mainTextureOffset.y) % 1.0f));
    }
}