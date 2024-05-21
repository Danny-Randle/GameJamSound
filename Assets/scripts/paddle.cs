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

        if (gameObject.transform.position[0] >= Screen.width + 135)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(-130, gameObject.transform.position[1], 0), transform.rotation);
        }

        if (gameObject.transform.position[0] <= -135)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(Screen.width+130, gameObject.transform.position[1], 0), transform.rotation);
        }

        // get key graphic objects:
        GameObject p1KeyIcon = findObjByName("P1Key");
        GameObject p2KeyIcon = findObjByName("P2Key");

        // get rects for all of the above:
        Rect pdl1Rect = pdl1.GetComponent<RectTransform>().rect;
        Rect pdl2Rect = pdl2.GetComponent<RectTransform>().rect;
        Rect ballRect = ball.GetComponent<RectTransform>().rect; // looking for the rect of a ball feels weired.

        // get the script for the Ball:
        Ball script = ball.GetComponent<Ball>();

        // get the input for the paddles:
        if (Input.GetKey("left"))
        {
            pdl1.transform.Translate(5,0,0);
            pdl2.transform.Translate(-5,0,0);
        }
        if (Input.GetKey("right"))
        {
            pdl1.transform.Translate(-5, 0, 0);
            pdl2.transform.Translate(5, 0, 0);
        }
    }
}