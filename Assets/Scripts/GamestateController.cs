using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateController : MonoBehaviour
{
    public bool puzzle1solved = false;
    public bool puzzle2solved = false;
    public bool puzzle3solved = false;

    public GameObject player1;
    public GameObject player2;
    public PlayerController player1controller;
    public PlayerController player2controller;

    public GameObject primaryCamera;

    private float puzzle1solvedXplayer1 = -47.2f;
    public Puzzle1ButtonController puzzle1;
    public GameObject puzzle1blocker;
    public GameObject puzzle1blockercollider;
    public GameObject blockerAfterPuzzle2;

    public GameObject instructions;

    public int currCamPos = 0;
    public float camPosAfterPuzzle1 = -23.0f;
    public float camPosAfterPuzzle2 = 33.0f;
    public float camPosAfterPuzzle3 = 67.0f;

    public bool puzzle2player1onbutton = false;
    public bool puzzle2player2onbutton = false;
    public PuzzleTwoFenceLowController puzzle2fencelow;
    public PuzzleTwoFenceLowController puzzle2fencehigh;

    public AudioSource releaseBlockerSound;
    private bool blockerSoundPlayed = false;

    public AudioSource jokeSound1;
    public AudioSource jokeSound2;
    public AudioSource jokeSound3;

    private bool instructionsVisible = true;
    private float gameStartTime;

    void Start()
    {
        gameStartTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(instructionsVisible)
        {
            if(Time.time > gameStartTime + 10)
            {
                Destroy(instructions);
                instructionsVisible = false;
            }
        }
        
        // PUZZLE 1
        if(player1.transform.position.x > puzzle1solvedXplayer1 && !puzzle1solved)
        {           
            puzzle1.upSpeed = puzzle1.upSpeed * 2.0f;

            if (puzzle1blocker && puzzle1blocker.transform.position.y < 53.0f)
            {
                if (!blockerSoundPlayed)
                {
                    releaseBlockerSound.Play();
                    blockerSoundPlayed = true;
                }
                puzzle1blocker.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 20.0f, 0.0f));
            }
            else
            {
                Destroy(puzzle1blocker);

                Vector3 newColliderPos = puzzle1blockercollider.transform.position;
                newColliderPos.x = -55.7f;
                puzzle1blockercollider.transform.position = newColliderPos;

                puzzle1solved = true;
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
                jokeSound1.Play();
            }
         }

        // PUZZLE 2
        if (puzzle1solved && currCamPos == 1)
        {
            if (puzzle2player1onbutton && puzzle2player2onbutton && !puzzle2solved)
            {
                puzzle2fencelow.isReleasing = true;
                puzzle2fencehigh.isReleasing = true;

                puzzle2solved = true;
                blockerAfterPuzzle2.SetActive(true);

                jokeSound2.Play();
            }
        }
        if(puzzle2solved && currCamPos == 1 && puzzle2fencelow.isReleased)
        { 
            Vector3 newCamPos = primaryCamera.transform.position;
            newCamPos.x = newCamPos.x + 0.5f;

            primaryCamera.transform.position = newCamPos;

            if (newCamPos.x > camPosAfterPuzzle2)
            {
                currCamPos = 2;
                Vector3 newColliderPos = puzzle1blockercollider.transform.position;
                newColliderPos.x = 2.5f;
                puzzle1blockercollider.transform.position = newColliderPos;
            }
        }

        // PUZZLE 3 solved?
        if(currCamPos == 2 && player1.transform.position.x > 48.0f)
        {
            Vector3 newCamPos = primaryCamera.transform.position;
            newCamPos.x = newCamPos.x + 0.5f;

            primaryCamera.transform.position = newCamPos;

            if (newCamPos.x > camPosAfterPuzzle3)
            {
                currCamPos = 3;

                Vector3 newColliderPos = puzzle1blockercollider.transform.position;
                newColliderPos.x = 29.0f;
                puzzle1blockercollider.transform.position = newColliderPos;

                jokeSound3.Play();
            }
        }
    }
}
