using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerTwoController : MonoBehaviour
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
 /*       float moveX = Input.GetAxis("HorizontalB");
        float moveY = Input.GetAxis("VerticalB");
        Vector3 movement = new Vector3(moveX, 0.0f, 0.0f);
        rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));*/

        // Move forward right trigger + right shoulder
        // Move backward left trigger + left shoulder
        LTpressedCurrUpdate = Input.GetAxis("PlayerTwoLT") == -1;
        LSpressedCurrUpdate = Input.GetButton("PlayerTwoLS");

        RTpressedCurrUpdate = Input.GetAxis("PlayerTwoRT") == -1;
        RSpressedCurrUpdate = Input.GetButton("PlayerTwoRS");

        // FORWARD
        if (RTpressedLastUpdate && RSpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
        }
        else if (RSpressedLastUpdate && RTpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
        }

        // BACKWARD
        if (LTpressedLastUpdate && LSpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
        }
        else if (LSpressedLastUpdate && LTpressedCurrUpdate)
        {
            Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
            rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
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
