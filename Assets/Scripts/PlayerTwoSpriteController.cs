using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoSpriteController : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = player.transform.position;
        transform.position = newPos;
    }
}
