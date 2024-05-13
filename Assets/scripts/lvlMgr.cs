using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlMgr : MonoBehaviour
{
    // This class will contain the functions required to switch to different minigames:
    public void loadLevel(int level)
    {
        SceneManager.LoadScene(level); // switch to DDD minigame;
    }
}
