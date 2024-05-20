using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool writeDataToFile(string filePath, string data)
    {
        // write the data from the data param to the file in the filepath param:
        using (StreamWriter outputFile = new StreamWriter(filePath))
        {
            outputFile.Write(data);
            return true;
        }
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
