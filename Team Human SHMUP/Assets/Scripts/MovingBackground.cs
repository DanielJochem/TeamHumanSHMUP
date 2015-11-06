using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class MovingBackground : MonoBehaviour {

    public Texture[] frames;
    //public FileInfo[] framesBefore;
    //public List<FileInfo> frameArray;
    public int framesPerSecond = 30;

    //Was trying to get it automatically add the frames to the script, but I can't wuite get it to function, maybe you can try?
    void Awake() {
        //frames = (Texture[])Resources.LoadAll("Assets\\Images\\Background Frames\\*.gif");

        /*
        //DirectoryInfo dir = new DirectoryInfo("Assets\\Images\\Background Frames");
        //framesToLoad = dir.GetFiles("*.gif");

        for (int i = 0; i < framesToLoad.Length; i++) {
             frameArray.Add(framesToLoad[i]);
             frameArray.Sort();
             frames[i] = (Texture)frameArray[i];
             //frames[i] = framesToLoad[i];
             Debug.Log((Texture)frameArray[i]);
         }*/

        //Debug.Log(frames.ToString());
    }

    void FixedUpdate() {
        int index = (int)((Time.time / 1.1) * framesPerSecond) % frames.Length;
        GetComponent<Renderer>().material.mainTexture = frames[index];
    }





}