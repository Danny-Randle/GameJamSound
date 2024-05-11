using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SP : MonoBehaviour
{
    // rng:
    public System.Random rndDir = new();

    public int speedSelRng = 0;

    public int xSpeed = -10;
    public int ySpeed = -10;
    public int dirCntr = 0;
    public int[] speeds = {10, -10};


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // method to call unity's object finder and return it's GameObject:
    GameObject findObjByName(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        return obj;
    }

    // Update is called once per frame
    void Update()
    {
        // get the ball object and all compinents:
        GameObject ball = findObjByName("ball");
        BoxCollider2D ballCol = ball.GetComponent<BoxCollider2D>();
        Ball script = ball.GetComponent<Ball>();

        // get this object's boxCollider:
        BoxCollider2D spCol = gameObject.GetComponent<BoxCollider2D>();

        // movement logic:
        if (dirCntr == 50)
        {
            if(ySpeed > 0)
            {
                ySpeed = -10;
            }
            else
            {
                ySpeed = 10;
            }
            dirCntr = 0;

        }

        if (gameObject.transform.position[0] <= -150)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(Screen.width + 90, Screen.height / 2, 0), transform.rotation);
        }
        dirCntr += 1;
        gameObject.transform.Translate(xSpeed, ySpeed, 0); // move the ball down.


        if (spCol.IsTouching(ballCol))
        {
            script.score ++;
        }
        


        // check if colliding with the ball:
        
    }
}
