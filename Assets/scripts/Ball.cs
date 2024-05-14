using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public System.Random startDirectionChooser = new();
    public int startDirection = 0;
    public bool hasStarted = false;
    public int movementAmountX = 0;
    public int movementAmountY = 0;
    public bool collisionEnabled = true; // controls whether collision is enabled or not.
    public bool movementEnabled = true; // controls whether the ball moves.
    public bool isRespawning = false; // this stops you from losing all of your lives whilst you are respawning.
    
    // score and Endurance / arcade mode stuff:
    public int score = 0;
    public int reqScore = 3; // score required to complete the level.
    public int pts = 0; // this is only used in arcade mode.
    public int hiScorePts = 0; // this will be used later on.
    public int ptsIncrCnt = 0;
    public int ptsCntThrshHld = 10; // this is the threashold required to incrment the score, some power ups will change this.

    // declare bools for paddle directions:
    public bool pdl1MovingLeft = false;
    public bool pdl1MovingRight = false;
    public bool pdl2MovingLeft = false;
    public bool pdl2MovingRight = false;
    public GameObject ball; // ball placeholder.

    // declare int to hold lives:
    public int lives = 3; // player gets three retries before game over.

    // Start is called before the first frame update
    public void Start()
    {
        ball = findObjByName("ball");
    }

    // method to call unity's object finder and return it's GameObject:
    GameObject findObjByName(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        return obj;
    }

    public int incrementScore()
    {
        score++;
        return score;
    }

    public void bringBallBackIntoPlay()
    {
        collisionEnabled = true;
        movementEnabled = true;
        isRespawning = false;
    }

    public void respawnBall()
    {
        isRespawning = true;
        ball.transform.SetPositionAndRotation(new Vector3(Screen.width / 2, Screen.height / 2, 0), transform.rotation);
        collisionEnabled = false;
        movementEnabled = false;
        Debug.Log("movementEnabled: " + movementEnabled);
        Invoke("bringBallBackIntoPlay", 3);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            startDirection = startDirectionChooser.Next(1);
            hasStarted = true;
        }

        ptsIncrCnt++;

        if(pts < 0)
        {
            pts = 0;
        }
        if (pts % 100 == 0 && pts % 100 > 0)
        {
            lives += 1;
        }

        // check lives:
        if (lives <= 0)
        {
            Debug.Log("GAME OVER!");
            SceneManager.LoadScene(4); // this will load the game over screen.
        }

        if(score == reqScore)
        {
            Debug.Log("LEVEL Complete");
            SceneManager.LoadScene(3); // this will load the level complete screen.
        }

        // get objects:
        GameObject pdl1 = findObjByName("paddle1");
        GameObject pdl2 = findObjByName("paddle2");
        GameObject scrEdgeL = findObjByName("ScreenEdgeL");
        GameObject scrEdgeR = findObjByName("ScreenEdgeR");
        GameObject scrEdgeTop = findObjByName("ScreenEdgeT");
        GameObject scrEdgeBottom = findObjByName("ScreenEdgeB");
        GameObject livesCntrTxt = findObjByName("Lives counter");
        GameObject arcadeScoreCntrTxt = findObjByName("ArcadeScoreCounter");
        GameObject bg = findObjByName("BG");

        // get rects:
        Rect pdl1Rect = pdl1.GetComponent<RectTransform>().rect;
        Rect pdl2Rect = pdl2.GetComponent<RectTransform>().rect;
        Rect ballRect = ball.GetComponent<RectTransform>().rect; // looking for the rect of a ball feels weired.

        // get colliders:
        BoxCollider2D pdl1Col = pdl1.GetComponent<BoxCollider2D>();
        BoxCollider2D pdl2Col = pdl2.GetComponent<BoxCollider2D>();
        BoxCollider2D ballCol = ball.GetComponent<BoxCollider2D>();
        BoxCollider2D scrLCol = scrEdgeL.GetComponent<BoxCollider2D>();
        BoxCollider2D scrRCol = scrEdgeR.GetComponent<BoxCollider2D>();
        BoxCollider2D scrTCol = scrEdgeTop.GetComponent<BoxCollider2D>();
        BoxCollider2D scrBCol = scrEdgeBottom.GetComponent<BoxCollider2D>();

        // get the script compoenet from the BG:
        BG modeScript = bg.GetComponent<BG>();

        // set the reqScore value based on level:
        reqScore = modeScript.requiredSP_Pellets;

        // Get the text component for the lives counter and score counter objects:
        Text livesCounterTxt = livesCntrTxt.GetComponent<UnityEngine.UI.Text>();
        // pretty much straight away output the lives counter to the screen:
        livesCounterTxt.text = "Lives: " + lives;

        if (modeScript.arcadeMode)
        {
            if(ptsIncrCnt >= ptsCntThrshHld)
            {
                pts++; // make the score go up until the player is dead.
                ptsIncrCnt = 0;
            }
            Text arcadeScoreCounterTxt = arcadeScoreCntrTxt.GetComponent<UnityEngine.UI.Text>();
            arcadeScoreCounterTxt.text = "Score: " + pts;

        }

        // get the paddles movement:
        if (Input.GetKey("a"))
        {
            pdl1MovingLeft = true;
            pdl1MovingRight = false;
            pdl2MovingLeft = false;
            pdl2MovingRight = true;
        }

        if (Input.GetKey("d"))
        {
            pdl1MovingLeft = false;
            pdl1MovingRight = true;
            pdl2MovingLeft = true;
            pdl2MovingRight = false;
        }
        else
        {
            pdl1MovingLeft = false;
            pdl1MovingRight = false;
            pdl2MovingLeft = false;
            pdl2MovingRight = false;
        }

        // chooseDirectionOfTravel:
        if (startDirection == 0 && movementEnabled)
        {
            movementAmountY = 10;
            ball.transform.Translate(movementAmountX, movementAmountY, 0); // move the ball down.
        }
        else if(startDirection == 1 && movementEnabled)
        {
            movementAmountY = -10;
            ball.transform.Translate(movementAmountX, movementAmountY, 0); // move the ball up.
        }

        // sort out collision:
        if (ballCol.IsTouching(pdl1Col))
        {
            // invert start direction:
            if(startDirection == 0)
            {
                if (pdl1MovingLeft)
                {
                    movementAmountX = -3;
                }
                if (pdl1MovingRight)
                {
                    movementAmountX = 3;
                }
                ball.transform.Translate(movementAmountX, -32, 0);
                startDirection = 1;
            }
            else
            {
                if (pdl1MovingLeft)
                {
                    movementAmountX = -3;
                }
                if (pdl1MovingRight)
                {
                    movementAmountX = 3;
                }

                ball.transform.Translate(movementAmountX, 32, 0);
                startDirection = 0;
            }
        }
        if (ballCol.IsTouching(pdl2Col))
        {
            // invert start direction:
            if (startDirection == 0)
            {
                if (pdl2MovingLeft)
                {
                    movementAmountX = -3;
                }
                if(pdl2MovingRight)
                {
                    movementAmountX = 3;
                }
                ball.transform.Translate(movementAmountX, -32, 0);
                startDirection = 1;
            }
            else
            {
                if (pdl2MovingLeft)
                {
                    movementAmountX = -3;
                }
                if (pdl2MovingRight)
                {
                    movementAmountX = 3;
                }

                ball.transform.Translate(movementAmountX, 32, 0);
                startDirection = 0;
            }
        }

        // bounce off of the edges of the screen:
        if (ballCol.IsTouching(scrLCol))
        {
            movementAmountX = 3;
        }

        if (ballCol.IsTouching(scrRCol))
        {
            movementAmountX = -3;
        }

        if (ballCol.IsTouching(scrTCol) && collisionEnabled)
        {
            // check if arcade mode is enabled:
            if (modeScript.arcadeMode)
            {
                pts -= 5;
            }

            Invoke("respawnBall", 0);
            // lose a life:
            lives -= 1;
            Debug.Log("movementEnabled: " + movementEnabled);

            // fix the bug where your lives get reduced by 2.
            //lives += 1;
        }

        if (ballCol.IsTouching(scrBCol) && collisionEnabled)
        {
            // check if arcade mode is enabled:
            if (modeScript.arcadeMode)
            {
                pts -= 5;
            }

            
            Invoke("respawnBall", 0);
            // lose a life:
            lives -= 1;
            Debug.Log("movementEnabled: "+movementEnabled);

            // fix the bug where your lives get reduced by 2.
            //lives += 1;

            

        }

    }
}