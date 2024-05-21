using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwnBadSP : MonoBehaviour
{
    public int spwnTmrBadSP = 0;
    public int objCntr = 0;
    public int spwnIntervalBadSP = 90;
    public int spwnAmntBadSP = 10;
    public GameObject badSpPrefab;
    public bool hasSpawned = false;
    public int spawnPos = 0; // this is the vertical spawn position, change this from the editor.
    public System.Random randInt = new(); // new random object.
    public bool arcadeMode = false;


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

    public void spawnBadSP()
    {
        if (arcadeMode)
        {
            int spawnPosArcadeMode = randInt.Next(200, 950);
            GameObject badSpInst = Instantiate(badSpPrefab, new Vector3(Screen.width, spawnPosArcadeMode, 1), Quaternion.identity);
            Debug.Log("Spawned Bad SP");
        }
        else
        {
            GameObject badSpInst = Instantiate(badSpPrefab, new Vector3(Screen.width, spawnPos, 1), Quaternion.identity);
            Debug.Log("Spawned Bad SP");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        GameObject bg = findObjByName("BG");
        BG modeScript = bg.GetComponent<BG>();
        arcadeMode = modeScript.arcadeMode;

        Debug.Log("arcadeMode:" + modeScript.arcadeMode);
        if (modeScript.arcadeMode)
        {
            // get the ball script for getting access to the ball's score component:
            Ball ballScript = GameObject.Find("ball").GetComponent<Ball>();
            spwnIntervalBadSP = 500000 / (ballScript.pts+1);

            if (hasSpawned == false)
            {
                spwnTmrBadSP++;
                if (spwnTmrBadSP >= spwnIntervalBadSP)
                {
                    if (spwnTmrBadSP >= spwnIntervalBadSP)
                    {
                        spawnBadSP();
                        spwnTmrBadSP = 0;
                    }
                }
                if (objCntr >= spwnAmntBadSP)
                {
                    hasSpawned = true;
                }

            }
        }

        // if not using arcade mode, use limited spawning.
        else
        {
            if (hasSpawned == false)
            {
                spwnTmrBadSP++;
                if (spwnTmrBadSP >= spwnIntervalBadSP)
                {
                    for (int i = 0; i < spwnAmntBadSP; i++)
                    {
                        if (spwnTmrBadSP >= spwnIntervalBadSP)
                        {
                            spawnBadSP();
                            spwnTmrBadSP = 0;
                            objCntr++;
                        }
                    }

                }
                if (objCntr >= spwnAmntBadSP)
                {
                    hasSpawned = true;
                }

            }
        }
        
    }
}
