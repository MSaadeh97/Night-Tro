using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public GameObject AboutUsCanvas;
    public Button PlayButton, AboutUsButton, ExitButton;


    public void StartGame()
    {
        SceneManager.LoadScene("Scene");
        GameManager.score = 0;
        GameManager.lives = 3;
    }

    public void AboutUs()
    {
        AboutUsCanvas.SetActive(true);
    }

    public void CloseAboutUs()
    {
        AboutUsCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
