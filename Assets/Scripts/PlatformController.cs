using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    private float moveSpeed = 1f;
    public Vector3 direction;
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public Transform destination;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        setDestination(startPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(isActive) {
            platform.GetComponent<Rigidbody>().MovePosition(platform.position + direction * moveSpeed * Time.fixedDeltaTime);
            if(Vector3.Distance(platform.position, destination.position) < moveSpeed * Time.fixedDeltaTime)
            {
                setDestination(destination == startPoint ? endPoint : startPoint);
            }
        }
    }

    void onDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startPoint.position, platform.localScale);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endPoint.position, platform.localScale);
    }

    void setDestination(Transform dest)
    {
       destination = dest;
       direction = (destination.position - platform.position).normalized;
    }
}
