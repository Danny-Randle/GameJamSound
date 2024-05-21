using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public float cntr = 0.0f;
    public int timeCntr = 3;
    public float reqTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
        gameObject.transform.SetPositionAndRotation(new Vector3(Screen.width / 2, Screen.height / 3, 10), transform.rotation);
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
        
        gameObject.GetComponent<UnityEngine.UI.Text>().text = timeCntr.ToString();
        //Debug.LogError((cntr == sec) + " " + cntr + " " + sec);
        cntr += Time.deltaTime;
    }
}
