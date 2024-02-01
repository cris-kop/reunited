using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Global Gamestate
    public GamestateController gameState;

    // General
    public int playerId;
    private bool usesController;

    // Player input
    public string CtrlWalkLeftA;
    public string CtrlWalkLeftB;
    public string CtrlWalkRightA;
    public string CtrlWalkRightB;
    public string CtrlJump;
    
    public string KbdWalkLeftA;
    public string KbdWalkLeftB;
    public string KbdWalkRightA;
    public string KbdWalkRightB;
    public string KbdJump;

    // Movement properties
    public float moveSpeed;
    public float jumpSpeed;
    public float walkLimiter;
    private float lastMovement;
    private bool isGrounded = false;

    private bool walkLeftALastUpdate = false;
    private bool walkLeftBLastUpdate = false;
    private bool walkRightALastUpdate = false;
    private bool walkRightBLastUpdate = false;

    private bool walkLeftACurrUpdate = false;
    private bool walkLeftBCurrUpdate = false;
    private bool walkRightACurrUpdate = false;
    private bool walkRightBCurrUpdate = false;

    private bool jumped = false;
    
    // Sounds
    public AudioSource walkSoundA;
    public AudioSource walkSoundB;
    public AudioSource jumpSound;
    public AudioSource playerDiedSound;

    // Phyics/logics
    private Rigidbody rb;
    
    // Sprites for animations
    public Animator walkAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastMovement = Time.time;
        usesController = UseController();
    }

    // Fixed update for physics/ fixed timestep
    void FixedUpdate()
    {
        PlayerMove();

        if(jumped)
        {
            if (isGrounded)
            {
                // needs to be in fixedUpdate because of AddForce
                PlayerJump();
            }
        }
        ResetInput();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandlePlayerDied();
    }

    void OnCollisionEnter(Collision other)
    {
        if (playerId == 1)
        {
            if (other.gameObject.name == "PlayerTwo")
            {
                SceneManager.LoadScene("EndSceneWithVideo");
            }
        }
    }

    // DIY functions
    private void HandlePlayerDied()
    {
        // only player 1 can die, ground/lower floor
        if (transform.position.y < -6.0f)
        {
            Vector3 newPos = transform.position;
            newPos.y = 4.85f;

            // hardcoded currently, based on puzzle X location
            // when adding more puzzles, refactor!!!
            if (transform.position.x < 10.0f)
            {
                newPos.x = -44.0f;

                if(gameState.puzzle2solved)
                {
                    newPos.x = 1.2f;
                }
             }
            else if (transform.position.x > 10.0f)
            {
                if (gameState.currCamPos == 2)
                {
                    newPos.x = 19.0f;
                }
                if(gameState.currCamPos == 3)
                {
                    newPos.x = 49.0f;
                }
            }
            transform.position = newPos;
        }
        // play sound before respawning
        if (transform.position.y < -2.0f && transform.position.y > -3.0f)
        {
            playerDiedSound.Play();
        }
    }

    private void GetInput()
    {
        if (usesController)
        {
            walkLeftACurrUpdate = Input.GetAxis(CtrlWalkLeftA) == -1;
            walkLeftBCurrUpdate = Input.GetButton(CtrlWalkLeftB);

            walkRightACurrUpdate = Input.GetAxis(CtrlWalkRightA) == -1;
            walkRightBCurrUpdate = Input.GetButton(CtrlWalkRightB);

            if (Input.GetButtonDown(CtrlJump))
            {
                jumped = true;
            }
        }
        else
        {
            walkLeftACurrUpdate = Input.GetButton(KbdWalkLeftA);
            walkLeftBCurrUpdate = Input.GetButton(KbdWalkLeftB);

            walkRightACurrUpdate = Input.GetButton(KbdWalkRightA);
            walkRightBCurrUpdate = Input.GetButton(KbdWalkRightB);

            if (Input.GetButtonDown(KbdJump))
            {
                jumped = true;
            }
        }
    }

    private void ResetInput()
    {   
        walkLeftALastUpdate = walkLeftACurrUpdate;
        walkLeftBLastUpdate = walkLeftBCurrUpdate;

        walkRightALastUpdate = walkRightACurrUpdate;
        walkRightBLastUpdate = walkRightBCurrUpdate;

        jumped = false;
    }

    private bool PlayerMoved()
    {
        return walkLeftACurrUpdate || walkLeftBCurrUpdate || walkRightACurrUpdate || walkRightBCurrUpdate;
    }

    private bool PlayerTriedCheatMovement()
    {
        return (walkLeftACurrUpdate && walkLeftBCurrUpdate) || (walkRightACurrUpdate && walkRightBCurrUpdate);
    }

    private float GetPlayerMoveDirection()
    {
        if ((walkRightALastUpdate && walkRightBCurrUpdate) ||
            (walkRightBLastUpdate && walkRightACurrUpdate))
        {
            return 1.0f;
        }
        if ((walkLeftALastUpdate && walkLeftBCurrUpdate) ||
            (walkLeftBLastUpdate && walkLeftACurrUpdate))
        {
            return -1.0f;
        }
        return 0.0f;
    }

    private void PlayMoveSound()
    {
        if((walkRightALastUpdate && walkRightBCurrUpdate) || (walkLeftALastUpdate && walkLeftBCurrUpdate))
        {
            walkSoundA.Play();
        }
        if ((walkRightBLastUpdate && walkRightACurrUpdate) || (walkLeftBLastUpdate && walkLeftACurrUpdate))
        {
            walkSoundB.Play();
        }
    }
    private void PlayerJump()
    {
        //rb.velocity += jumpSpeed * new Vector3(0.0f, 1.0f, 0.0f);
        rb.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f));
        
        if(!jumpSound.isPlaying)
        {
            jumpSound.Play();
        }
    }

    private void PlayerMove()
    {
        if (PlayerMoved())
        {
            if (PlayerTriedCheatMovement())
            {
                if (Time.time > lastMovement + walkLimiter)
                {
                    lastMovement = Time.time;
                }
                else return;
            }

            float moveX = GetPlayerMoveDirection();

            if (moveX != 0.0f)
            {
                Vector3 movement = new Vector3(moveX, 0.0f, 0.0f);
                rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));

                PlayMoveSound();
            }
            walkAnimation.SetBool("Walking", true);
            return;
        }
        walkAnimation.SetBool("Walking", false);
    }

    private int GetNumControllersConnected()
    {
        int result = 0;
        string[] controllers = Input.GetJoystickNames();

        for(int i=0;i<controllers.Length;++i)
        {
            if(controllers[i] != null && controllers[i] != "")
            {
                result++;
            }
        }
        return result;
    }
    private bool UseController()
    {
        int numControllers = GetNumControllersConnected();

        if (numControllers == 1)
        {
            if (playerId == 1)
            {
                return true;
            }
            return false;
        }
        return numControllers > 1;
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = false;
        }
    }

}
