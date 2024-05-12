using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    public int powerUp = 0; // 0 = none, 1 = double length, 2 = double speed, 3 = both.
    public int speed = 5; // default speed of 5.
    public bool isSpeedHalved = false;
    // Do the same for the second player's icons:

    // Start is called before the first frame update
    public void Start()
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
        // get P1 and P2 objects:
        GameObject pdl1 = findObjByName("paddle1");
        GameObject pdl2 = findObjByName("paddle2");
        GameObject ball = findObjByName("ball");

        // get key graphic objects:
        GameObject p1KeyIcon = findObjByName("P1Key");
        GameObject p2KeyIcon = findObjByName("P2Key");

        // get rects for all of the above:
        Rect pdl1Rect = pdl1.GetComponent<RectTransform>().rect;
        Rect pdl2Rect = pdl2.GetComponent<RectTransform>().rect;
        Rect ballRect = ball.GetComponent<RectTransform>().rect; // looking for the rect of a ball feels weired.

        // get the script for the Ball:
        Ball script = ball.GetComponent<Ball>();

        // power up logic:
        if (powerUp == 0)
        {
            speed = 5;
        }
        
        if(powerUp == 1)
        {
            speed = 10;
        }

        if(powerUp == 2)
        {
            if(script.movementAmountX == 10 ||  script.movementAmountX == -10) 
            {
                script.movementAmountX = script.movementAmountX / 2;
            }
            if(script.movementAmountY == 10 || script.movementAmountY == -10)
            {
                script.movementAmountY = script.movementAmountY / 2;
            }
            else
            {
                script.movementAmountX = script.movementAmountX;
                script.movementAmountY = script.movementAmountY;
            }
            
            
        }
        // get sprite for keyIcons:
        //Sprite p1KeyIconSpr = p1KeyIcon.GetComponent<Image>().sprite;

            // try swapping sprites:
            //p1KeyIcon.GetComponent<Image>().sprite = p1IconPool[rngIntP1];
            //p2KeyIcon.GetComponent<Image>().sprite = p2IconPool[rngIntP2];

            //Debug.Log("Current Sprite Index sprite:" + p1IconPool[rngIntP1]);

            // Load the appropriate image and set it as the sprite for the key icon:
            //p1KeyIconSpr = Resources.Load(keyGraphicP1, typeof(Sprite)) as Sprite;
            //Debug.Log(keyGraphicP1);

            // output to debug console and jump the key change counter by 15 so the system doesn't get stuck cycling through keys:
            //Debug.Log("P1 Key: " + currentKeyP1 + " P2 Key: " + currentKeyP2);
            //keyChangeCntr += 15;

        // get the input for the paddles:
        if (Input.GetKey("a"))
        {
            pdl1.transform.Translate(5,0,0);
            pdl2.transform.Translate(-5,0,0);
        }
        if (Input.GetKey("d"))
        {
            pdl1.transform.Translate(-5, 0, 0);
            pdl2.transform.Translate(5, 0, 0);
        }
    }
}