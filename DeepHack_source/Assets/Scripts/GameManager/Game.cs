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
 *History:     (1):6.11,15:12  (2):6.12,11:25 (3):7.6,14:26
*****************************************************************/

public class Game
{
    //是否暂停
    //need to reconstruct
    public static bool isPaused = false;

    //存档数据
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

        /*
         目前存档只有一个
         */

        NumOfSaves = 1;

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

    //读档,存档只有一个
    public static void Load()
    {
        int saveId = 1;
        if (!File.Exists(Application.persistentDataPath + "/save/" + saveId.ToString() + ".save"))
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

    //初始化数据，重新开始游戏
    public static void NewGame()
    {
        globalData = new GlobalData();
        Save();
    }

    

    //产生测试数据
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

    //产生测试数据
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
        isPaused = true;
        Time.timeScale = 0;
    }

    //继续游戏
    public static void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    //重新开始关卡
    public static void ReStartScene()
    {
        Debug.Log("NAME: " + SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //重新开始关卡
    public static void StartNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("load new scene: " + sceneName);
    }


    //屏幕设置
    public static void SetFullScreen(bool _fullScreen)
    {
        Debug.Log("set screen to " + (_fullScreen ? "full" : "not full"));
        globalData.settingData.fullScreen = _fullScreen;

        if(_fullScreen)
        {
            //获取设置当前屏幕分辩率  
            Resolution[] resolutions = Screen.resolutions;
            //设置当前分辨率  
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);

            Screen.fullScreen = true;  //设置成全屏,
        }
        else
        {
            Screen.SetResolution(1366, 768, true);
        }
    }

    //设置BGM
    public static void SetBGM(float _BGM)
    {
        globalData.settingData.BGMVolum = _BGM;
    }

    //设置音效
    public static void SetMusicEffect(float _MusicEffect)
    {
        globalData.settingData.musicEffectVolum = _MusicEffect;
    }



}
