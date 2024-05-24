using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwnPwrUp : MonoBehaviour
{
    public int spwnTmrPwrUp = 0;
    public int spwnIntervalPwrUp = 39;
    public int spwnAmntPwrUp = 10;
    public int cntr = 0;
    public int incr = 0;
    public int objCntr = 0;
    public GameObject pwrUpPrefab;
    public bool hasSpawned = false;
    public int spawnPos = 0; // this is the vertical spawn position, change this from the editor.


    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public void spawnPwrUp()
    {
        GameObject pwrUpInst = Instantiate(pwrUpPrefab, new Vector3(Screen.width, spawnPos, 1), Quaternion.identity);
        Debug.Log("Spawned POWER UP");
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawned == false)
        {
            spwnTmrPwrUp++;
            if(spwnTmrPwrUp >= spwnIntervalPwrUp * Time.deltaTime)
            {
                for (int i = 0; i < spwnAmntPwrUp; i++)
                {


                    if (spwnTmrPwrUp >= spwnIntervalPwrUp)
                    {
                        spawnPwrUp();
                        spwnTmrPwrUp = 0;
                        objCntr++;
                    }
                }

            }
            if(objCntr >= spwnAmntPwrUp)
            {
                hasSpawned = true;
            }
            
        }

    }
}
