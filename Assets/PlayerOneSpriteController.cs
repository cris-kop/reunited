using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneSpriteController : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Player.transform.position;
        transform.position = newPos;
        
    }
}
