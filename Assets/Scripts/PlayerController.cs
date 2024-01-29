using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Gamestate
    public GamestateController gameState;

    // General
    public int playerId;

    // Movement properties
    public float moveSpeed;
    public float jumpSpeed;
    public float maxPlayerHeight;
    public float walkLimiter;
    private float lastMovement;

    private bool LTpressedLastUpdate = false;
    private bool LSpressedLastUpdate = false;
    private bool RTpressedLastUpdate = false;
    private bool RSpressedLastUpdate = false;

    private bool LTpressedCurrUpdate = false;
    private bool LSpressedCurrUpdate = false;
    private bool RTpressedCurrUpdate = false;
    private bool RSpressedCurrUpdate = false;

    public string InputWalkLeftA;
    public string InputWalkLeftB;
    public string InputWalkRightA;
    public string InputWalkRightB;

    public string InputJump;

    // Sounds
    public AudioSource walkSoundA;
    public AudioSource walkSoundB;
    public AudioSource jumpSoundPlayer1;
    public AudioSource jumpSoundPlayer2;
    public AudioSource playerDiedSound;

    // Phyics/logics
    private Rigidbody rb;
    private bool jumped = false;

    // Sprites for animations
    public Animator walkAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastMovement = Time.time;
    }

     // Fixed update for physics/ fixed timestep
    void FixedUpdate()
    {
        // Move forward right trigger + right shoulder
        // Move backward left trigger + left shoulder
        LTpressedCurrUpdate = Input.GetAxis(InputWalkLeftA) == -1;
        LSpressedCurrUpdate = Input.GetButton(InputWalkLeftB);

        RTpressedCurrUpdate = Input.GetAxis(InputWalkRightA) == -1;
        RSpressedCurrUpdate = Input.GetButton(InputWalkRightB);

        // Any trigger or shoulder pressed?
        if (LTpressedCurrUpdate || LSpressedCurrUpdate || RTpressedCurrUpdate || RSpressedCurrUpdate)
        {
            if ((LTpressedCurrUpdate && LSpressedCurrUpdate) || (RSpressedCurrUpdate && RTpressedCurrUpdate))
            {
                if (Time.time > lastMovement + walkLimiter)
                {
                    // Not pressed at the same time? how to fix?
                    MovePlayer();
                    lastMovement = Time.time;
                }
            }
            else
            {
                MovePlayer();
            }
        }

        LTpressedLastUpdate = LTpressedCurrUpdate;
        LSpressedLastUpdate = LSpressedCurrUpdate;

        RTpressedLastUpdate = RTpressedCurrUpdate;
        RSpressedLastUpdate = RSpressedCurrUpdate;

        if(jumped)
        {
            if (transform.position.y < maxPlayerHeight)
            {
                JumpPlayer();
            }
            jumped = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(InputJump))
        {
            jumped = true;
        }

        // player 1 falling, reset
        if (transform.position.y < -6.0f)
        {
            Vector3 newPos = transform.position;

            if (transform.position.x < 10.0f)
            {
                newPos.x = -44.0f;
                newPos.y = 4.85f;
            }
            if (transform.position.x > 10.0f)
            {
                newPos.x = 19.0f;
                newPos.y = 4.85f;
            }
            transform.position = newPos;

        }
        if (transform.position.y < -2.0f && transform.position.y > -3.0f)
        { 
            playerDiedSound.Play();
        }
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
    private void JumpPlayer()
    {
        //rb.velocity += jumpSpeed * new Vector3(0.0f, 1.0f, 0.0f);
        rb.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f));
        if (playerId == 2)
        {
            if (!jumpSoundPlayer1.isPlaying)
            {
                jumpSoundPlayer1.Play();
            }
        }
        if (playerId == 1)
        {
            if (!jumpSoundPlayer2.isPlaying)
            {
                jumpSoundPlayer2.Play();
            }
        }
    }

    private void MovePlayer()
    {
        var isMoving = false;

        // FORWARD
        if (RTpressedLastUpdate && RSpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
            isMoving = true;

            walkSoundA.Play();
        }
        else if (RSpressedLastUpdate && RTpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
            isMoving = true;
            walkSoundB.Play();
        }

        // BACKWARD
        if (LTpressedLastUpdate && LSpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
            isMoving = true;
            walkSoundA.Play();
        }
        else if (LSpressedLastUpdate && LTpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
            isMoving = true;
            walkSoundB.Play();
        }

        walkAnimation.SetBool("Walking", isMoving);
    }
}
