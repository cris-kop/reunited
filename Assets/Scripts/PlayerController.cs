using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    public string InputWalkLeftA;
    public string InputWalkLeftB;
    public string InputWalkRightA;
    public string InputWalkRightB;

    // Phyics/logics
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        string[] controllerNames;
        controllerNames = Input.GetJoystickNames();

        Debug.Log("Joystick name: " + controllerNames[0]);
        Debug.Log("Joystick name: " + controllerNames[1]);
        Debug.Log("Joystick name: " + controllerNames[2]);
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
