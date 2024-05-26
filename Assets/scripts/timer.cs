using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class timer : MonoBehaviour
{
    public float cntr = 0.0f;
    public int timeCntr = 3;
    public float reqTime = 1.0f;
    public bool isTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);

        if (!isTimer)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(Screen.width / 2, Screen.height / 3, 10), transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cntr > reqTime)
        {
            timeCntr--;
            cntr = 0.0f;
        }

        if(timeCntr <= 0)
        {
            Destroy(gameObject);

        }

        if (isTimer)
        {
            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Time: " + timeCntr.ToString() + " Seconds";
            if(timeCntr <= 0)
            {
                SceneManager.LoadScene(3); // this will load the game over screen.
            }
        }
        else
        {
            gameObject.GetComponent<UnityEngine.UI.Text>().text = timeCntr.ToString();
        }
        //Debug.LogError((cntr == sec) + " " + cntr + " " + sec);
        cntr += Time.deltaTime;
    }
}
