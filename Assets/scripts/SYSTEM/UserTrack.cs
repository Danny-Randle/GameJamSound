using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserTrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] Songs; 
        Songs = Directory.GetFiles("../Resources/UserTracks", ".mp3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
