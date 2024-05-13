using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BadSP : MonoBehaviour
{
    // rng:
    public System.Random rndDir = new();
    public System.Random rndMult = new();

    public int speedSelRng = 0;

    public int xSpeed = -10;
    public int ySpeed = -10;
    public int dirCntr = 0;
    public int[] speeds = {10, -10};
    public int[] speedMultipliersY = { 0, 1, 2, 3, 4 }; // array containing multiplier values
    public int[] directions = { 0, 1 }; // holds either up or down.
    public int dir = 0; // direction.
    public int speedMultY = 0; // speed multiplier


    // Start is called before the first frame update
    void Start()
    {
        dir = rndDir.Next(directions.Length);
        speedMultY = rndMult.Next(speedMultipliersY.Length);

        if(speedMultY <= 0)
        {
            speedMultY = 1;
        }
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

        // get the BG GameObject and its script:
        GameObject bg = findObjByName("BG");
        BG modeScript = bg.GetComponent<BG>();


        // get this object's boxCollider:
        BoxCollider2D spCol = gameObject.GetComponent<BoxCollider2D>();

        

        if(dir == 0)
        {
            ySpeed = -10 * speedMultY;
        }
        if(dir == 1)
        {
            ySpeed = 10 * speedMultY;
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
            gameObject.transform.SetPositionAndRotation(new Vector3(Screen.width + 90, Screen.height / 2, 0), transform.rotation);
        }
        dirCntr += 1;
        gameObject.transform.Translate(xSpeed, ySpeed, 0); // move the ball down.

        
        if (spCol.IsTouching(ballCol))
        {
            if (modeScript.arcadeMode == false)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.transform.SetPositionAndRotation(new Vector3(Screen.width + 90, Screen.height / 2, 0), transform.rotation);
            }
            script.lives --;
        }
        


        // check if colliding with the ball:
        
    }
}
