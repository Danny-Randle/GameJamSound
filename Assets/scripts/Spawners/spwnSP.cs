using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwnSP : MonoBehaviour
{
    public int spwnTmrSP = 0;
    public int objCntr = 0;
    public int spwnIntervalSP = 90;
    public int spwnAmntSP = 10;
    public GameObject SpPrefab;
    public bool hasSpawned = false;
    public int spawnPos = 0; // this is the vertical spawn position, change this from the editor.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawnSP()
    {
        GameObject spInst = Instantiate(SpPrefab, new Vector3(Screen.width, spawnPos, 1), Quaternion.identity);
        Debug.Log("Spawned SP");
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawned == false)
        {
            spwnTmrSP++;
            if (spwnTmrSP >= spwnIntervalSP)
            {
                for (int i = 0; i < spwnAmntSP; i++)
                {
                    if (spwnTmrSP >= spwnIntervalSP)
                    {
                        spawnSP();
                        spwnTmrSP = 0;
                        objCntr++;
                    }
                }

            }
            if (objCntr >= spwnAmntSP)
            {
                hasSpawned = true;
            }

        }
    }
}
