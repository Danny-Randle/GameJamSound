using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG : MonoBehaviour
{
    public bool arcadeMode = true; // sets arcade mode;
    public int requiredSP_Pellets = 3; // change this value in the editor per level. It will have no consequence in arcade mode.
    public string levelName = "Template Level Name"; // this will be the level's name change this in the unity editor per level scene.
    // Start is called before the first frame update
    void Start()
    {
        if(arcadeMode == false) // fix error where it would try to find levelName in arcade mode where there are no levels.
        {
            GameObject lvlName = GameObject.Find("levelName");
            Text lvlNameTxt = lvlName.GetComponent<UnityEngine.UI.Text>();
            lvlNameTxt.text = levelName; // tell the level to display its level name.
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
