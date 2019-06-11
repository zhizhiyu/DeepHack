using UnityEngine;
using System.Collections;
using System.IO;


/****************************************************************
 *Copyright(C)  2019 by luojinming All rights reserved. 
 *FileName:     messageCenter.cs
 *Author:       luojinming 
 *Version:      1.0 
 *Date:         6.11
 *Description:  游戏进度管理器，具有保存，读取等功能
 *              
 *History:     1: 6.11,15:12 
*****************************************************************/

public class Game
{ 
    //存档
    public static void save()
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
    public static void load()
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



}
