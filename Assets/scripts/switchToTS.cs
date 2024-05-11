using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchToTitleScreen : MonoBehaviour
{
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(0); // goes back to the title screen.
    }
}
