using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1ButtonController : MonoBehaviour
{
    public GameObject puzzleWall;
    public float downSpeed;
    public float upSpeed;

    private float initPosY;
    
    // Start is called before the first frame update
    void Start()
    {
        puzzleWall = GameObject.Find("puzzle1wall");
        
        initPosY = puzzleWall.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(puzzleWall.transform.position.y < initPosY)
        {
            Vector3 newPos = puzzleWall.transform.position;
            newPos.y = newPos.y + (upSpeed * Time.deltaTime);

            puzzleWall.transform.position = newPos;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Player hit button!");

            Vector3 newPos = puzzleWall.transform.position;
            newPos.y = newPos.y - downSpeed;

            // deltaT???? moet dit?
            puzzleWall.transform.position = newPos;
        }

    }
}
