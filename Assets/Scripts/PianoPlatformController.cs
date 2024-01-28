using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPlatformController : MonoBehaviour
{
    public AudioSource landSound;

    void OnCollisionEnter(Collision collision)
    {
        if (!landSound.isPlaying)
        {
            landSound.Play();
        }
    }
}
