using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    public AudioSource jumpSound;

    public float _min = .2f;

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
    }

    // DIY functions
    private void JumpPlayer()
    {
        //rb.velocity += jumpSpeed * new Vector3(0.0f, 1.0f, 0.0f);
        rb.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f));
        if (!jumpSound.isPlaying)
        {
            jumpSound.Play();
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
