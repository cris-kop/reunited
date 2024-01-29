using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTwoFenceLowController : MonoBehaviour
{
    public bool isReleasing;
    public float releaseSpeed;

    public bool isReleased;
    private float maxZbeforeDestroy = 10.5f;

    // Start is called before the first frame update
    void Start()
    {
        isReleasing = false;
        isReleased = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReleased && isReleasing)
        {
            Debug.Log("Function to release fence called!");
            if(transform.position.z < maxZbeforeDestroy)
            {
                Vector3 newPos = transform.position;
                newPos.z += releaseSpeed * Time.deltaTime;
                transform.position = newPos;
            }
            else
            {
                isReleased = true;
            }
        }
    }
}
