using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniGameMgr : MonoBehaviour
{
    // This class will contain the functions required to switch to different minigames:
    public void switchToDingDongDitch()
    {
        SceneManager.LoadScene(2); // switch to DDD minigame;
    }

    public void lvlCompleteScr()
    {
        SceneManager.LoadScene(3); // this will load the level complete screen.
    }

    public void gameOverScreen()
    {
        SceneManager.LoadScene(4); // this will load the game over screen.
    }
}
