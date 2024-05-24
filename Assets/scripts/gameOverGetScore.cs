using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class gameOverGetScore : MonoBehaviour
{

    //public string DATA_LOCATION = "../../Test";


    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("[ E001 ] " + err.Message); // display an error if the file cant be opened.
            return "E:"+ err.Message; // error value, it is impossible to have -1 as a hiscore.
        }
    }

    GameObject findObjByName(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        return obj;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject score = findObjByName("SCORE");
        Text scoreTxt = score.GetComponent<UnityEngine.UI.Text>();

        // load the score:
        string scoreData = readStrDataFromFile(Application.persistentDataPath + "\\lastScore.dat");
        string hiScoreData = readStrDataFromFile(Application.persistentDataPath + "\\hiScore.dat");

        if(Int32.Parse(scoreData) >= Int32.Parse(hiScoreData))
        {
            // draw it to the screen:
            scoreTxt.text = "NEW HIGH SCORE: " + scoreData;
        }
        else
        {
            // draw it to the screen:
            scoreTxt.text = "SCORE: " + scoreData;

        }
    }
}
