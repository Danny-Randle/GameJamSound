using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class setPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //centrePos(50);
    }

    public void setPosRightAlign(int padx)
    {
        gameObject.transform.Translate(Screen.width  - padx,0,0); // move header to ensure it is always visible.
    }

    public void centrePos(int padx)
    {
        Rect gRect = gameObject.GetComponent<RectTransform>().rect;
        float width = gRect.width;
        int widthI = (int)width;

        Debug.Log("WidthFin =" + widthI);

        int scaleFactor = Screen.width / widthI;

        

        //float width = 758.0927;


        gameObject.transform.Translate(Screen.width / 2 - (widthI / 2) + (scaleFactor * 5), 0, 0); // move header to ensure it is always visible.
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
