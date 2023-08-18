using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void replayGame()
    {
        AudioManger.instance.playSound("button");
        SceneManager.LoadScene("Level_1");
    }

    public void quitGame()
    {
        Application.Quit();
        AudioManger.instance.playSound("button");
    }
}
