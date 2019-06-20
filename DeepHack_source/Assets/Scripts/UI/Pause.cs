using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public void ClickPause()
    {
        Game.PauseGame();
        SetActive(pausePanel, true);
    }

    public void ClickContinue()
    {
        SetActive(pausePanel, false);
        Game.ContinueGame();
    }

    public void ClickRestart()
    {
        SetActive(pausePanel, false);
        Game.ReStartScene();
    }

    public void ClickReturn()
    {
        SetActive(pausePanel, false);
        SceneManager.LoadScene("MainMenu");
    }



    static public void SetActive(GameObject go, bool state)
    {
        if (go == null)
        {
            return;
        }

        if (go.activeSelf != state)
        {
            go.SetActive(state);
        }
    }
}
