using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRAME_MAN : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // cap game FPS to 60 to stop physics issues:
        Application.targetFrameRate = 60;

        // set refreshrate to be 60 and res to be 16:9 FHD, the user will be able to change this later:
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, new RefreshRate() { numerator = 60, denominator = 1 });

    }
}
