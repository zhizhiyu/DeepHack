using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameObject cameraObject;
    /// <summary>
    /// 
    /// </summary>
    public GameObject pausePanel;

    public void ClickPause()
    {
        print("pausing");
        Game.PauseGame();
        SetActive(pausePanel, true);

        cameraObject.SetActive(false);
    }

    public void ClickContinue()
    {
        Debug.Log("continue button clicked");
        SetActive(pausePanel, false);
        Game.ContinueGame();

        cameraObject.SetActive(true);
    }

    public void ClickRestart()
    {
        Debug.Log("restart button clicked");
        SetActive(pausePanel, false);
        Game.ContinueGame();
        Game.ReStartScene();
    }

    public void ClickReturn()
    {
        Debug.Log("return button clicked");
        SetActive(pausePanel, false);
        Game.ContinueGame();
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
