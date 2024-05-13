using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ptsPelletScript;

public class Pts_Spawner : MonoBehaviour
{

    public System.Random rndTimer = new();
    public System.Random rndSpwnXY = new();

    public int spwnTmr = 0;
    public int spwnrVal = 0; // this is set in the editor for each level.
    public int spwnPosX = 0;
    public int spwnPosY = 0;
    public int spwnLimit = 10;
    public int spwnCnt = 0; // this totals up how many pts are in the scene.

    public int[] spwnPositionsX = { -64, Screen.width + 64 };
    public int[] spwnPositionsY = { -64, Screen.height + 64 };

    public GameObject ptsPelletTemplate;
    public GameObject ptsPelletObj;

    // method to call unity's object finder and return it's GameObject:
    GameObject findObjByName(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        return obj;
    }

    // Start is called before the first frame update
    GameObject Start()
    {
        ptsPelletTemplate = findObjByName("pts_pellet");
        Destroy(ptsPelletTemplate.GetComponent<ptsPellet>());

        return ptsPelletTemplate;
    }

    // Update is called once per frame
    void Update()
    {
        

        // increment the spawn timer:
        spwnTmr++;

        if( spwnTmr == spwnrVal)
        {
            ptsPelletObj = ptsPelletTemplate;
            ptsPelletObj.AddComponent<ptsPellet>();
            // reset spwnTmr when it hits the value and spawn in the pts pellets:
            spwnTmr = 0;

            if (spwnCnt < spwnLimit)
            {
                spwnCnt++;
                Debug.Log("spwnCnt =" + spwnCnt);

                // choose a spawn position from the arrays:
                spwnPosX = -64;
                spwnPosY = rndSpwnXY.Next(1080);

                // instantiate the pts pellet GameObject:
                Instantiate(ptsPelletObj, new Vector3(spwnPosX, spwnPosY, 0), Quaternion.identity);
                
            }
            
        }
    }
}
