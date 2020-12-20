using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public bool isMenu;
    public Text bestScore;

    private void Start()
    {
        Time.timeScale = 1;

        if (PlayerPrefs.HasKey("BestScore") && isMenu)
        {
            bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        }
    }

    public void OnClick(string name)
    {
        switch (name)
        {
            case "Restart":
                SceneManager.LoadScene(1);
                break;

            case "Close":
                SceneManager.LoadScene(0);
                break;

            case "Menu":
                SceneManager.LoadScene(0);
                break;

            case "Play":
                SceneManager.LoadScene(1);
                break;
        }
    }
}
