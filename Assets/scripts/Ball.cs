using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO;
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
    public bool hasFinishedLoading = false; // this stops the game from over reading data.

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
    public GameObject timerPlaceHolder; // placeholder for prefab.

    // decalre mode bool:
    public bool isArcadeMode;

    // declare int to hold lives:
    public int lives = 3; // player gets three retries before game over.

    // Save Data management:
    public string ARCADE_HI_SCORE_FNAME = "hiScore.dat";
    //public string DATA_LOCATION = "../../Test";

    // hiScore value:
    public int hiScore = 0;

    // audio variables
    public AudioSource MyAudio;
    public AudioClip[] myAudioClips;
    public AudioClip damageClip;

    // Start is called before the first frame update
    public void Start()
    {
        Application.targetFrameRate = 60;
        ball = findObjByName("ball");
    }

    // methods to load data:
    public int readIntDataFromFile(string filePath)
    {
        try
        {
            // create a new stream reader object:
            StreamReader dataLdr = new StreamReader(filePath);

            // parse the data as a 32-Bit int and then return it:
            int dataLn = Int32.Parse(dataLdr.ReadLine()); // read the first line of data.

            // close the file:
            dataLdr.Close();

            // load was a success.
            return dataLn;
        }
        catch(Exception err)
        {
            // data read failed:
            return -1; // error value, it is impossible to have -1 as a hiscore.
        }
    }

    public string readStrDataFromFile(string filePath)
    {
        try
        {
            // create a new stream reader object:
            StreamReader dataLdr = new StreamReader(filePath);

            string dataLn = dataLdr.ReadLine(); // read the first line of data.

            // close the file:
            dataLdr.Close();

            // load was a success.
            return dataLn;
        }
        catch (Exception err)
        {
            // data read failed:
            return "E"; // error value, it is impossible to have -1 as a hiscore.
        }
    }

    // method to write save data:
    public bool writeDataToFile(string fileDir, string fileName, string data)
    {
        //declare local path var:
        string fPath = fileDir;
        string fName = fileName;
        string fData = data;

        // make sure folder for saveData already exists:
        DirectoryInfo dir = new DirectoryInfo(fPath);

        try
        {
            if (dir.Exists)
            {
                File.WriteAllText(Path.Combine(fPath, fName), fData);
                return true; // end execution.
            }

            // at this point the system has determined that the dir does not exist:
            dir.Create();
            File.WriteAllText(Path.Combine(fPath, fName), fData);
            return true;
        }
        catch (Exception err)
        {
            return false;
        }
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
    }


    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            startDirection = startDirectionChooser.Next(1);
            hasStarted = true;
        }

        // only run this if it is arcade mode and it hasn't loaded yet.
        if (isArcadeMode && !hasFinishedLoading)
        {
            // try to read the high score;
            hiScore = readIntDataFromFile(Application.persistentDataPath + "\\" + ARCADE_HI_SCORE_FNAME);
            if(hiScore == -1)
            {
                hiScore = 0;
            }

            hasFinishedLoading = true;
        }

        // check lives:
        if (lives <= 0)
        {
            // if it is arcade mode save the hi score, this will only happen when the player's current score beats the last hi score.
            if (isArcadeMode)
            {
                writeDataToFile(Application.persistentDataPath, "lastScore.dat", pts.ToString()); // save last score to show on game over screen.

                if(pts >= hiScore)
                {
                    writeDataToFile(Application.persistentDataPath, "hiScore.dat", pts.ToString()); // save last score to show on game over screen.
                }

                SceneManager.LoadScene(13); // this will load the game over screen arcade edition.
            }
            else
            {
                SceneManager.LoadScene(3); // this will load the game over screen.
            }
            
        }

        ptsIncrCnt++;

        if(pts < 0)
        {
            pts = 0;
        }

        if(pts > hiScore)
        {
            hiScore = pts;
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
        GameObject cntDwnTxt = findObjByName("cntDwnTxt");
        GameObject hiScoreTxt = findObjByName("HISCORE");

        // get text for cntDwnTxt:

        if(isArcadeMode == true)
        {
            Text hiScoreTxtLbl = hiScoreTxt.GetComponent<UnityEngine.UI.Text>();
            hiScoreTxtLbl.text = "HI-SCORE: "+hiScore.ToString();
        }
        else
        {
            //Text cntDwnLabel = cntDwnTxt.GetComponent<UnityEngine.UI.Text>();
        }

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

        // use the levelID to store level progress data:
        if (score == reqScore)
        {
            // write data to the level's file to let the rest of the game know it is complete.
            writeDataToFile(Application.persistentDataPath, modeScript.levelID+".dat", "COMPLETE");
            SceneManager.LoadScene(2); // this will load the level complete screen.
        }

        // set arcadeMode bool:
        isArcadeMode = modeScript.arcadeMode;

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
        if (Input.GetKey("left"))
        {
            pdl1MovingLeft = true;
            pdl1MovingRight = false;
            pdl2MovingLeft = false;
            pdl2MovingRight = true;
        }

        if (Input.GetKey("right"))
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
            movementAmountY = 300;
            ball.transform.Translate(movementAmountX * Time.deltaTime, movementAmountY * Time.deltaTime, 0); // move the ball down.
        }
        else if(startDirection == 1 && movementEnabled)
        {
            movementAmountY = -300;
            ball.transform.Translate(movementAmountX * Time.deltaTime, movementAmountY * Time.deltaTime, 0); // move the ball up.
        }

        // sort out collision:
        if (ballCol.IsTouching(pdl1Col))
        {
            // invert start direction:
            if(startDirection == 0)
            {
                if (pdl1MovingLeft)
                {
                    movementAmountX = -300;
                }
                if (pdl1MovingRight)
                {
                    movementAmountX = 300;
                }

                ball.transform.Translate(movementAmountX * Time.deltaTime, -32, 0);
                startDirection = 1;
            }
            else
            {
                if (pdl1MovingLeft)
                {
                    movementAmountX = -300;
                }
                if (pdl1MovingRight)
                {
                    movementAmountX = 300;
                }

                ball.transform.Translate(movementAmountX * Time.deltaTime, 32, 0);
                startDirection = 0;
            }
            //Get Random Audio Clip and then play it
            AudioClip randomClip = myAudioClips[UnityEngine.Random.Range(0, myAudioClips.Length)];
            MyAudio.PlayOneShot(randomClip);
        }
        if (ballCol.IsTouching(pdl2Col))
        {
            // invert start direction:
            if (startDirection == 0)
            {
                if (pdl2MovingLeft)
                {
                    movementAmountX = -300;
                }
                if(pdl2MovingRight)
                {
                    movementAmountX = 300;
                }
                ball.transform.Translate(movementAmountX * Time.deltaTime, -32, 0);
                startDirection = 1;

            }
            else
            {
                if (pdl2MovingLeft)
                {
                    movementAmountX = -300;
                }
                if (pdl2MovingRight)
                {
                    movementAmountX = 300;
                }

                ball.transform.Translate(movementAmountX * Time.deltaTime, 32, 0);
                startDirection = 0;

            }
            //Get Random Audio Clip and then play it
            AudioClip randomClip = myAudioClips[UnityEngine.Random.Range(0, myAudioClips.Length)];
            MyAudio.PlayOneShot(randomClip);
        }

        // bounce off of the edges of the screen:
        if (ballCol.IsTouching(scrLCol))
        {
            movementAmountX = 400;
        }

        if (ballCol.IsTouching(scrRCol))
        {
            movementAmountX = -400;
        }

        if (ballCol.IsTouching(scrTCol) && collisionEnabled)
        {
            // check if arcade mode is enabled:
            if (modeScript.arcadeMode)
            {
                pts -= 5;
            }
            
            Invoke("respawnBall", 0);

            GameObject timerInst = Instantiate(timerPlaceHolder, new Vector3(Screen.width / 2, Screen.height / 2, 1), Quaternion.identity);
            

            Invoke("bringBallBackIntoPlay", 3);

            // lose a life:
            lives -= 1;

            Debug.Log("movementEnabled: " + movementEnabled);
            MyAudio.PlayOneShot(damageClip);
        }

        if (ballCol.IsTouching(scrBCol) && collisionEnabled)
        {
            // check if arcade mode is enabled:
            if (modeScript.arcadeMode)
            {
                pts -= 5;
            }

            Invoke("respawnBall", 0);

            GameObject timerInst = Instantiate(timerPlaceHolder, new Vector3(Screen.width / 2, Screen.height / 2 , 1), Quaternion.identity);
            

            Invoke("bringBallBackIntoPlay", 3);

            // lose a life:
            lives -= 1;

            Debug.Log("movementEnabled: " + movementEnabled);
            MyAudio.PlayOneShot(damageClip);
        }

    }
}