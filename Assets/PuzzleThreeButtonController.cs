using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleThreeButtonController : MonoBehaviour
{
    public GameObject puzzle3platform;

    private float platformMinZ = 1.0f;
    private float platformMaxZ = 8.0f;

    public float speedIn;
    public float speedOut;
    
    // Update is called once per frame
    void Update()
    {
        if(puzzle3platform.transform.position.z < platformMaxZ)
        {
            Vector3 newPos = puzzle3platform.transform.position;
            newPos.z = newPos.z + (Time.deltaTime * speedIn);

            puzzle3platform.transform.position = newPos;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // increase Z when jumping on button
        if(puzzle3platform.transform.position.z > platformMinZ)
        {
            Vector3 newPos = puzzle3platform.transform.position;
            newPos.z = newPos.z - (Time.deltaTime * speedOut);

            puzzle3platform.transform.position = newPos;
        }
    }
}
