using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;


/****************************************************************
 *Copyright(C)  2019 by luojinming All rights reserved. 
 *FileName:     MessageCenter.cs
 *Author:       luojinming 
 *Version:      1.0 
 *Date:         6.11
 *Description:  游戏管理器，具有暂停，开始，保存，读取等功能
 *              
 *History:     (1):6.11,15:12  (2):6.12,11:25
*****************************************************************/

public class Game : MonoBehaviour
{
    //是否暂停
    bool isPaused;

    //开始
    private void Start()
    {
        isPaused = false;
    }

    //update
    private void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            ReStartScene();
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
            if (isPaused)
            {
                isPaused = false;
                ContinueGame();
            }
            else
            {
                isPaused = true;
                PauseGame();
            }
        }

    }

    //存档
    public static void Save()
    {
        int NumOfSaves = 0;
        if (PlayerPrefs.HasKey("NumOfSaves"))
        {
            NumOfSaves = PlayerPrefs.GetInt("NumOfSaves");
        }
        else
        {
            PlayerPrefs.SetInt("NumOfSaves", 1);
        }
        NumOfSaves++;

        GlobalData newGlobalData = new GlobalData(0);

        string jsonGlobalData = JsonUtility.ToJson(newGlobalData);
        Debug.Log("json global data:  " + jsonGlobalData);

        StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/save/" + NumOfSaves.ToString() + ".save");

        Debug.Log(Application.persistentDataPath + "/save/" + NumOfSaves.ToString() + ".save");
        streamWriter.Write(jsonGlobalData);
        streamWriter.Close();

        
    }

    
    //读档
    public static void Load()
    {

        if(!File.Exists(Application.persistentDataPath + "game.save"))
        {
            Debug.Log("no game save exists");
            return;
        }
        StreamReader streamReader = new StreamReader(Application.persistentDataPath + "game.save");
        string jsonGlobalData = streamReader.ReadToEnd();
        Debug.Log(jsonGlobalData);

        GlobalData globalData = JsonUtility.FromJson<GlobalData>(jsonGlobalData);
        Debug.Log(globalData.userId);


    }

    //暂停游戏
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    //继续游戏
    public static void ContinueGame()
    {
        Time.timeScale = 1;
    }

    //重新开始关卡
    public static void ReStartScene()
    {
        Debug.Log("NAME: " + SceneManager.GetActiveScene().name);
        Debug.Log("TO STRING: " + SceneManager.GetActiveScene().ToString());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
