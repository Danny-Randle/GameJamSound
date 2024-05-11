using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMiniGameScreen : MonoBehaviour
{
    public void LoadMiniGameMenu()
    {
        SceneManager.LoadScene(1); // loads the minigame menu
    }
}
