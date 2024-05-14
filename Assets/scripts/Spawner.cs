using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ptsPelletScript;

public class Spawner : MonoBehaviour
{

    public System.Random rndTimer = new();
    public System.Random rndSpwnXY = new();

    // spawn timers for each prefab:
    public int spwnTmrSP = 0;
    public int spwnTmrBadSP = 0;
    public int spwnTmrPwrUp = 0;

    // These are the spawn intervals for each pellet:
    public int spwnIntervalSP = 30; // this is set in the editor for each level.
    public int spwnIntervalBadSP = 30;
    public int spwnIntervalPwrUp = 30;

    // Theese are the amounts of each thing we want to spawn:
    public int spwnAmntSP = 10;
    public int spwnAmntBadSP = 10;
    public int spwnAmntPwrUp = 10;

    public int spwnCnt = 0; // this totals up how many pts are in the scene.

    // references to prefabs:
    public GameObject SpPrefab;
    public GameObject badSpPrefab;
    public GameObject pwrUpPrefab;

    // method to call unity's object finder and return it's GameObject:
    GameObject findObjByName(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        return obj;
    }

    public void spawnSP()
    {
        GameObject spInst = Instantiate(SpPrefab, new Vector3(Screen.width, 400, 1), Quaternion.identity);
        Debug.Log("Spawned SP");
    }

    public void spawnBadSP()
    {
        GameObject badSpInst = Instantiate(badSpPrefab, new Vector3(Screen.width, -400, 1), Quaternion.identity);
        Debug.Log("Spawned BAD SP");
    }

    public void spawnPwrUp()
    {
        GameObject pwrUpInst = Instantiate(pwrUpPrefab, new Vector3(Screen.width, 900, 1), Quaternion.identity);
        Debug.Log("Spawned POWER UP");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // increment the timers:
        spwnTmrSP++;
        spwnTmrBadSP++;
        spwnTmrPwrUp++;

        if(spwnTmrSP == spwnIntervalSP)
        {
            spawnSP();
            spwnTmrSP = 0;
        }

        if(spwnTmrBadSP == spwnIntervalBadSP)
        {
            spawnBadSP();
            spwnTmrBadSP = 0;
        }

        if(spwnTmrPwrUp == spwnIntervalPwrUp)
        {
            spawnBadSP();
            spwnTmrPwrUp = 0;
        }

    }
}
