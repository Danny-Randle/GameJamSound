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

    public void switchToRPS()
    {
        SceneManager.LoadScene(0); // this will open rock paper scissors but atm it takes you to the titleScr.
    }

    public void switchToGame3()
    {
        SceneManager.LoadScene(0); // this will take you to whatever we decide game 3 is going to be.
    }
}
