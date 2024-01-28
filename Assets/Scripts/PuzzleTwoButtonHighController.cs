using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTwoButtonHighController : MonoBehaviour
{
    public GamestateController gameState;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player ON button high!");
        gameState.puzzle2player2onbutton = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player OFF button high!");
        gameState.puzzle2player2onbutton = false;
    }
}
