using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControllerScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if(Time.time > startTime + videoPlayer.clip.length)
        {
            Debug.Log("Done!");
            Application.Quit();
        }

    }


}
