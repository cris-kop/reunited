using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTwoButtonLowController : MonoBehaviour
{
    public GamestateController gameState;

    void OnTriggerEnter(Collider other)
    {
        gameState.puzzle2player1onbutton = true;
    }

    void OnTriggerExit(Collider other)
    {
        gameState.puzzle2player1onbutton = false;
    }
}
