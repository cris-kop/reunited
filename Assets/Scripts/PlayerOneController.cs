using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerOneController : MonoBehaviour
{
    // Movement properties
    public float moveSpeed;

    private bool LTpressedLastUpdate = false;
    private bool LSpressedLastUpdate = false;
    private bool RTpressedLastUpdate = false;
    private bool RSpressedLastUpdate = false;

    private bool LTpressedCurrUpdate = false;
    private bool LSpressedCurrUpdate = false;
    private bool RTpressedCurrUpdate = false;
    private bool RSpressedCurrUpdate = false;

    public AudioSource walkSound1;
    public AudioSource walkSound2;

    // Phyics/logics
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Fixed update for physics/ fixed timestep
    void FixedUpdate()
    {
        /// OLD: Move using Left Stick
        /*float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0.0f, 0.0f);
        rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));*/

        // Move forward right trigger + right shoulder
        // Move backward left trigger + left shoulder
        LTpressedCurrUpdate = Input.GetAxis("PlayerOneLT") == -1;
        LSpressedCurrUpdate = Input.GetButton("PlayerOneLS");

        RTpressedCurrUpdate = Input.GetAxis("PlayerOneRT") == -1;
        RSpressedCurrUpdate = Input.GetButton("PlayerOneRS");

        // FORWARD
        if (RTpressedLastUpdate && RSpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));

            walkSound1.Play();
        }
        else if (RSpressedLastUpdate && RTpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));

            walkSound2.Play();
        }

        // BACKWARD
        if (LTpressedLastUpdate && LSpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));

            walkSound1.Play();
        }
        else if (LSpressedLastUpdate && LTpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));

            walkSound2.Play();
        }

        LTpressedLastUpdate = LTpressedCurrUpdate;
        LSpressedLastUpdate = LSpressedCurrUpdate;

        RTpressedLastUpdate = RTpressedCurrUpdate;
        RSpressedLastUpdate = RSpressedCurrUpdate;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
