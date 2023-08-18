using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGamestarted;
    public GameObject startedText;
    public static int coinNumber;
    public Text coinText;

    private void Start()
    {
        coinNumber = 0;
        gameOver = false;
        isGamestarted = false;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }

        coinText.text = "Coins: " + coinNumber;

        if (SwipeManager.tap) {
            isGamestarted =true;
            Destroy(startedText);
        }

    }
}
