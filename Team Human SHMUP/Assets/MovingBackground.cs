using UnityEngine;
using System.Collections;

public class MovingBackground : MonoBehaviour {

    public Texture[] frames;
    public int framesPerSecond = 10;
	
	void Update () {
        if (frames.Length != 0)
        {
            int index = ((int)Time.time * framesPerSecond) % frames.Length;
            GetComponent<Renderer>().material.mainTexture = frames[index];
        }
    }
}