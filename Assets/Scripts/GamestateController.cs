using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateController : MonoBehaviour
{
    public bool puzzle1solved = false;
    public bool puzzle2solved = false;

    public GameObject player1;
    public GameObject player2;
    public PlayerController player1controller;
    public PlayerController player2controller;

    public GameObject primaryCamera;

    private float puzzle1solvedXplayer1 = -47.2f;
    public Puzzle1ButtonController puzzle1;
    public GameObject puzzle1blocker;
    public GameObject puzzle1blockercollider;

    private int currCamPos = 0;
    public float camPosAfterPuzzle1 = -23.0f;

    public bool puzzle2player1onbutton = false;
    public bool puzzle2player2onbutton = false;
    public PuzzleTwoFenceLowController puzzle2fencelow;
    public PuzzleTwoFenceLowController puzzle2fencehigh;

    public AudioSource releaseBlockerSound;

    // awake
    void Awake()
    {
        puzzle1solved = false;
        puzzle2solved = false;
    }

    // Update is called once per frame
    void Update()
    {
        // PUZZLE 1
        if(player1.transform.position.x > puzzle1solvedXplayer1)
        {
            Debug.Log("Here the sound should play!");
            
            puzzle1solved = true;
            puzzle1.finished = true;
            puzzle1.upSpeed = puzzle1.upSpeed * 2.0f;

            if (puzzle1blocker && puzzle1blocker.transform.position.y < 53.0f)
            {
                releaseBlockerSound.Play();
                puzzle1blocker.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 6.0f, 0.0f));
            }
            else
            {
                Destroy(puzzle1blocker);

                Vector3 newColliderPos = puzzle1blockercollider.transform.position;
                newColliderPos.x = -55.7f;
                puzzle1blockercollider.transform.position = newColliderPos;
            }
        }

        if (puzzle1solved && currCamPos == 0)
        {
            Vector3 newCamPos = primaryCamera.transform.position;
            newCamPos.x = newCamPos.x + 0.5f;

            primaryCamera.transform.position = newCamPos;

            if (newCamPos.x > camPosAfterPuzzle1)
            {
                currCamPos = 1;
                player1controller.maxPlayerHeight = 10.5f;
                player2controller.maxPlayerHeight = 27.0f;
            }
         }

        // PUZZLE 2
        if (!puzzle2solved)
        {
            if (puzzle2player1onbutton && puzzle2player2onbutton)
            {
                puzzle2fencelow.isReleasing = true;
                puzzle2fencehigh.isReleasing = true;

                puzzle2solved = true;
            }
        }
    }
}
