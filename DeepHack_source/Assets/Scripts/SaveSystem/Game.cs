using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

using System.Collections.Generic;

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

public class Game
{
    //是否暂停
    //need to reconstruct
    public static bool isPaused = false;

    //
    public static GlobalData globalData = new GlobalData();


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
            NumOfSaves++;
        }

        string jsonGlobalData = JsonUtility.ToJson(globalData);
        Debug.Log("json global data:  " + jsonGlobalData);

        StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/save/" + NumOfSaves.ToString() + ".save");
        Debug.Log(Application.persistentDataPath + "/save/" + NumOfSaves.ToString() + ".save");

        streamWriter.Write(jsonGlobalData);
        streamWriter.Close();
        
    }

    
    //读档
    public static void Load(int saveId)
    {
        if(!File.Exists(Application.persistentDataPath + "/save/" + saveId.ToString() + ".save"))
        {
            Debug.Log("no game save exists");
            return;
        }
        StreamReader streamReader = new StreamReader(Application.persistentDataPath + "/save/" + saveId.ToString() + ".save");
        string jsonGlobalData = streamReader.ReadToEnd();
        streamReader.Close();
        Debug.Log(jsonGlobalData);

        globalData = JsonUtility.FromJson<GlobalData>(jsonGlobalData);
    }

    //
    public static List<SceneConfigData> GenerateSceneConfigData()
    {
        List<SceneConfigData> sceneConfigDatas = new List<SceneConfigData>();
        for(int i=0; i<3; i++)
        {
            SceneConfigData newSceneConfigData = new SceneConfigData(i, "scene" + i.ToString(), "", "some descriptions", 10, 0);
            sceneConfigDatas.Add(newSceneConfigData);
        }
        return sceneConfigDatas;
    }

    public static void GenerateData()
    {
        for(int i=0; i<3; i++)
        {
            globalData.AddNewSceneSaveData(i, 10, 0, 10, 0, 20, "B");
        }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //
    public static void StartNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("load new scene: " + sceneName);
    }

}
