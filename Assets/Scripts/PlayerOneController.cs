using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerOneController : MonoBehaviour
{
    private Vector2 currMovement;

    private Rigidbody rb;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Fixed update for physics/ fixed timestep
    void FixedUpdate()
    {
        //Debug.Log("P1 FixedUpdate executed, movement: " + currMovement);

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0.0f, moveY);

        transform.position = transform.position + new Vector3(moveX, 0.0f, 0.0f);

//        rb.AddForce(movement * moveSpeed);        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
