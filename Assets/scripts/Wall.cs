using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool canMove = false; // sets whether the wall is movable.
    public int speed = 500; // speed of the platform.
    public int moveBoundery = 600; // limit of how far the wall can move before turning around.
    public int moveCntr = 0;
    public int direction = 0; // 0 = l 1 = r;

    private GameObject ball;
    private BoxCollider2D ballCol;
    private Ball ballData;


    public void Start()
    {
        ball = GameObject.Find("ball");
        ballCol = ball.GetComponent<BoxCollider2D>();
        ballData = ball.GetComponent<Ball>();
    }

    void Update()
    {
        if (canMove)
        {
            if(moveCntr >= moveBoundery)
            {
                if(direction == 0)
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
            }


            if(moveCntr <= moveBoundery - (moveBoundery * 2))
            {
                if (direction == 0)
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;
                }
            }


            if(direction == 0) // move left:
            {
                gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
                moveCntr++;
            }
            else //move right:
            {
                gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
                moveCntr--;
            }
            
            
        }

        if (gameObject.GetComponent<BoxCollider2D>().IsTouching(ballCol))
        {
            if (ballData.startDirection == 0)
            {
                ball.transform.Translate(ballData.movementAmountX * Time.deltaTime, -32, 0);
                ballData.startDirection = 1;
            }
            else
            {
                ball.transform.Translate(ballData.movementAmountX * Time.deltaTime, 32, 0);
                ballData.startDirection = 0;
            }
        }

    }
}

public class VerticalWall : MonoBehaviour
{
    public bool canMove = false; // sets whether the wall is movable.
    public int speed = 500; // speed of the platform.


    public void Start()
    {


    }

    void Update()
    {
        if (canMove)
        {
            gameObject.transform.Translate(0, speed * Time.deltaTime, 0);
        }

    }
}
