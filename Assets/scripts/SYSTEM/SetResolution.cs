using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setResFHD()
    {
        // set refreshrate to be 60 and res to be 16:9 FHD, the user will be able to change this later:
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, new RefreshRate() { numerator = 60, denominator = 1 });
    }

    public void setResFHD16x10()
    {
        // set refreshrate to be 60 and res to be FHD+, the user will be able to change this later:
        Screen.SetResolution(1920, 1200, FullScreenMode.ExclusiveFullScreen, new RefreshRate() { numerator = 60, denominator = 1 });
    }

    public void setResQHD()
    {
        // set refreshrate to be 60 and res to be 16:9 QHD, the user will be able to change this later:
        Screen.SetResolution(2560, 1440, FullScreenMode.ExclusiveFullScreen, new RefreshRate() { numerator = 60, denominator = 1 });
    }

    public void setResQHD16x10()
    {
        // set refreshrate to be 60 and res to be QHD+, the user will be able to change this later:
        Screen.SetResolution(2560, 1600, FullScreenMode.ExclusiveFullScreen, new RefreshRate() { numerator = 60, denominator = 1 });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
