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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawnBadSP()
    {
        GameObject badSpInst = Instantiate(badSpPrefab, new Vector3(Screen.width, spawnPos, 1), Quaternion.identity);
        Debug.Log("Spawned Bad SP");
    }

    // Update is called once per frame
    void Update()
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
