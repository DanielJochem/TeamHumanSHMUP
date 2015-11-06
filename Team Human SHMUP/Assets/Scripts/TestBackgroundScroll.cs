using UnityEngine;
using System.Collections;

public class TestBackgroundScroll : MonoBehaviour {

    public float scrollSpeed = 0.1f;
    public float scrollSpeed2 = 0.0f;
    public Vector2 scroll;

    void FixedUpdate()
    {
        scroll.x = Time.time * scrollSpeed2;
        scroll.y = -(Time.time * scrollSpeed);
        GetComponent<Renderer>().material.mainTextureOffset = scroll;
    }
}