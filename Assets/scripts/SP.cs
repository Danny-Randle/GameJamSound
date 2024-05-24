using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SP : MonoBehaviour
{
    // rng:
    public System.Random rndDir = new();

    public int speedSelRng = 0;

    private int xSpeed = -500;
    public int ySpeed = -500;
    public int dirCntr = 0;
    public int[] speeds = {500, -500};
    public int[] directions = { 0, 1 }; // holds either up or down.
    public int dir = 0; // direction.


    // Start is called before the first frame update
    void Start()
    {
        dir = rndDir.Next(directions.Length);
    }

    // method to call unity's object finder and return it's GameObject:
    GameObject findObjByName(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        return obj;
    }

    // Update is called once per frame:
    void Update()
    {
        // get the ball object and all compinents:
        GameObject ball = findObjByName("ball");
        BoxCollider2D ballCol = ball.GetComponent<BoxCollider2D>();
        Ball script = ball.GetComponent<Ball>();

        // get this object's boxCollider:
        BoxCollider2D spCol = gameObject.GetComponent<BoxCollider2D>();

        

        if(dir == 0)
        {
            ySpeed = -500;
        }
        if(dir == 1)
        {
            ySpeed = 500;
        }

        // movement logic:
        if (dirCntr == 50)
        {
            if(dir == 0)
            {
                dir = 1;
            }
            else
            {
                dir = 0;
            }

            dirCntr = 0;
        }

        if (gameObject.transform.position[0] <= -150)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(Screen.width, gameObject.transform.position[1], 1), transform.rotation);
        }
        dirCntr += 1;
        gameObject.transform.Translate(xSpeed * Time.deltaTime, 0, 0); // move the ball down.


        if (spCol.IsTouching(ballCol))
        {
            script.score ++;
            Destroy(gameObject);
        }
        


        // check if colliding with the ball:
        
    }
}
