using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTwoButtonLowController : MonoBehaviour
{
    public GamestateController gameState;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player ON button low!");
        gameState.puzzle2player1onbutton = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player OFF button low!");
        gameState.puzzle2player1onbutton = false;
    }
}
