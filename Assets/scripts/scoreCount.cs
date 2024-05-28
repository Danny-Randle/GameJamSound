using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scoreCount : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
//        gameObject.transform.SetPositionAndRotation(new Vector3(Screen.width / 2, Screen.height / 3, 10), transform.rotation);

    }

    // method to call unity's object finder and return it's GameObject:
    GameObject findObjByName(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        return obj;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject ball = findObjByName("ball");
        Ball ballScript = ball.GetComponent<Ball>();
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Notes: " + ballScript.score.ToString() + "/" + ballScript.reqScore.ToString();
    }
}
