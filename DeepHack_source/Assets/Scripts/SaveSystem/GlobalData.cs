using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/****************************************************************
 *Copyright(C)  2019 by luojinming All rights reserved. 
 *FileName:     GlobalData.cs
 *Author:       luojinming 
 *Version:      1.0 
 *Date:         6.12
 *Description:  游戏全局数据文件
 *              
 *History:     (1):6.12,16:02, (2):6.17,16.41
*****************************************************************/

[System.Serializable]
public class GlobalData
{
    //通关数
    public int passedSceneNums;

    //现在的关卡ID
    public int currentSceneId;

    //收集数量
    public int coin;


    //完成度
    public float completion;


    //通关后的存档文件列表
    public List<SceneSaveData> sceneSaveDatas;

    //
    public List<ReportSaveData> reports;

    //
    public List<ReportConfigData> reportConfigDatas;


    //构造函数
    public GlobalData()
    {
        Debug.Log("new global data");

        coin = 47;
        completion = 0;

        passedSceneNums = 0;
        currentSceneId = 0;

        GenerateSceneConfigDataFile();
        sceneSaveDatas = new List<SceneSaveData>();

        reports = new List<ReportSaveData>();
        GenerateReportConfigDataFile();
        reportConfigDatas = ReadReportConfigData();
        GenerateReportSaveData();
    }


    //生成关卡配置文件
    public void GenerateSceneConfigDataFile()
    {
        int sceneNum = 3;
        StreamWriter streamWriter;
        if (Directory.Exists(Application.persistentDataPath + "/sceneConfigData/"))
        {
            Debug.Log("the directory already exists");
            return;
        }
        Directory.CreateDirectory(Application.persistentDataPath + "/sceneConfigData/");
        for(int i=0; i<sceneNum; i++)
        {
            SceneConfigData newSceneConfigData = new SceneConfigData(i, "name", "imagePath", "no description", 10, 10);
            streamWriter = new StreamWriter(Application.persistentDataPath + "/sceneConfigData/" + i.ToString() + ".txt");
            string jsonData = JsonUtility.ToJson(newSceneConfigData);
            streamWriter.Write(jsonData);
            streamWriter.Close();
        }
    }

    //返回关卡配置文件
    public List<SceneConfigData> ReadSceneConfigDataFile()
    {
        int sceneNum = 3;
        List<SceneConfigData> sceneConfigDatas = new List<SceneConfigData>();

        StreamReader streamReader;
        for(int i=0; i<sceneNum; i++)
        {
            if(!File.Exists(Application.persistentDataPath + "/sceneConfigData/" + i.ToString() + ".txt"))
            {
                Debug.Log("file not exists!");
                break;
            }
            streamReader = new StreamReader(Application.persistentDataPath + "/sceneConfigData/" + i.ToString() + ".txt");
            string jsonData = streamReader.ReadToEnd();
            streamReader.Close();
            Debug.Log(jsonData);

            SceneConfigData newSceneConfigData = JsonUtility.FromJson<SceneConfigData>(jsonData);
            sceneConfigDatas.Add(newSceneConfigData);
        }

        return sceneConfigDatas;
    }



    //通关时候新增一个通关存档数据
    public void AddNewSceneSaveData(int _sceneId, int _numOfCoins, int _numOfCollectedCoins, 
        int _numOfHidedCoins, int _numOfCollectedHidedCoins, float _usedTime, string _score)
    {

        ///////////////注意这里有点问题
        if(_sceneId == passedSceneNums)
        {
            SceneSaveData newSceneSaveData = new SceneSaveData(_sceneId, _numOfCoins, _numOfCollectedCoins,
_numOfHidedCoins, _numOfCollectedHidedCoins, _usedTime, _score);
            sceneSaveDatas.Add(newSceneSaveData);
        }
        else
        {
            Debug.Log("增加通关记录失败");
        }

    }

    public void AddNewSceneSaveData(SceneSaveData _sceneSaveData)
    {
        if(_sceneSaveData.sceneId == passedSceneNums)
        {
            sceneSaveDatas.Add(_sceneSaveData);
        }
        else
        {
            Debug.Log("增加通关记录失败");
        }
    }

    //获取某个关卡的通关数据
    public SceneSaveData GetSceneSaveDataBySceneId(int _sceneId)
    {

        return sceneSaveDatas[_sceneId];
    }

    //更新某个关卡的通关数据
    public void UpdateSceneSaveData(int _sceneId, int _numOfCoins, int _numOfCollectedCoins,
        int _numOfHidedCoins, int _numOfCollectedHidedCoins, float _usedTime)
    {
        if(_sceneId > passedSceneNums)
        {
            Debug.Log("修改通关记录失败");
            return;
        }
        sceneSaveDatas[_sceneId].sceneId = _sceneId;
        sceneSaveDatas[_sceneId].numOfCoins = _numOfCoins;
        sceneSaveDatas[_sceneId].numOfCollectedCoins = _numOfCollectedCoins;
        sceneSaveDatas[_sceneId].numOfHidedCoins = _numOfHidedCoins;
        sceneSaveDatas[_sceneId].numOfCollectedHidedCoins = _numOfCollectedHidedCoins;
        sceneSaveDatas[_sceneId].usedTime = _usedTime;
    }

    //产生ReportConfigData txt文件，但是需要手动修改这些文件的内容
    public void GenerateReportConfigDataFile()
    {
        int configDataNum = 15;
        StreamWriter streamWriter;

        if (!Directory.Exists(Application.persistentDataPath + "/ReportConfigData/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/ReportConfigData/");
        }
        else
        {
            Debug.Log("ReportConfigDataFile already exists!");
            return;
        }

        for (int i = 0; i < configDataNum; i++)
        {
            ReportConfigData newReportConfigData = new ReportConfigData(i, 0, 10, "no name", "no description");
            string jsonNewReportConfigData = JsonUtility.ToJson(newReportConfigData);

            streamWriter = new StreamWriter(Application.persistentDataPath + "/ReportConfigData/" + i.ToString() + ".txt");
            streamWriter.Write(jsonNewReportConfigData);
            streamWriter.Close();
        }
    }

    //读取ReportConfigData txt文件，返回ReportConfigData列表数据
    public List<ReportConfigData> ReadReportConfigData()
    {
        List<ReportConfigData> reportConfigDatas = new List<ReportConfigData>();

        int configDataNum = 15;

        StreamReader streamReader;
        for (int i = 0; i < configDataNum; i++)
        {
            if (!File.Exists(Application.persistentDataPath + "/ReportConfigData/" + i.ToString() + ".txt"))
            {
                Debug.Log("no report config data exists!");
                break;
            }
            streamReader = new StreamReader(Application.persistentDataPath + "/ReportConfigData/" + i.ToString() + ".txt");

            string StringNewReportConfigData = streamReader.ReadToEnd();
            streamReader.Close();

            //Debug.Log(StringNewReportConfigData);

            ReportConfigData newReportConfigData = JsonUtility.FromJson<ReportConfigData>(StringNewReportConfigData);

            reportConfigDatas.Add(newReportConfigData);

        }
        return reportConfigDatas;
    }

    //生成数据
    void GenerateReportSaveData()
    {
        //if(reports != null)
        //{
        //    Debug.Log("report save data already exists");
        //    return;
        //}
        List<ReportConfigData> reportConfigDatas = ReadReportConfigData();
        foreach(ReportConfigData re in reportConfigDatas)
        {
            ReportSaveData newReportSaveData = new ReportSaveData(re.index, re.parentIndex, false, false);
            reports.Add(newReportSaveData);
        }
    }


    //更新报告解锁数据
    public void UpdateReportDataUnLocked(int index)
    {
        for(int i=0; i<reports.Count; i++)
        {
            if(reports[i].index == index)
            {
                Debug.Log("find the report");
                if(reports[i].unlocked == true)
                {
                    Debug.LogError("already unlocked!");
                    return;
                }
                reports[i].unlocked = true;
            }
        }
        
    }

    //更新报告是否打开数据
    public void UpdateReportDataIsOpen(int index, bool isOpen)
    {
        foreach(ReportSaveData re in reports)
        {
            if(re.index == index)
            {
                re.isOpen = isOpen;
            }
        }
    }

}


//关卡配置文件
[System.Serializable]
public class SceneConfigData
{
    //该关卡的ID
    public int sceneId;

    //关卡名字
    public string sceneName;

    //图片名字
    public string sceneImage;

    //关卡描述
    public string sceneDescription;

    //关卡全部普通的金币
    public int numOfCoins;

    //关卡全部隐藏的金币
    public int numOfHidedCoins;

    public SceneConfigData(int _sceneId, string _sceneName, string _sceneImage ,string _sceneDescription, int _numOfCoins, int _numOfHidedCoins)
    {
        sceneId = _sceneId;
        sceneName = _sceneName;
        sceneImage = _sceneImage;
        sceneDescription = _sceneDescription;
        numOfCoins = _numOfCoins;
        numOfHidedCoins = _numOfHidedCoins;
    }
}


//关卡记录文件
[System.Serializable]
public class SceneSaveData
{
    //该关卡的ID
    public int sceneId;

    //关卡全部普通的金币
    public int numOfCoins;

    //角色收集的所有普通金币
    public int numOfCollectedCoins;

    //关卡全部隐藏的金币
    public int numOfHidedCoins;

    //角色收集的所有隐藏金币
    public int numOfCollectedHidedCoins;

    //用掉的时间
    public float usedTime;

    //评级
    public string score;

    public SceneSaveData(int _sceneId, int _numOfCoins, int _numOfCollectedCoins, int _numOfHidedCoins, int _numOfCollectedHidedCoins, float _usedTime, string _score)
    {
        sceneId = _sceneId;
        numOfCoins = _numOfCoins;
        numOfCollectedCoins = _numOfCollectedCoins;
        numOfHidedCoins = _numOfHidedCoins;
        numOfCollectedHidedCoins = _numOfCollectedHidedCoins;
        usedTime = _usedTime;
        score = _score;
    }
}



//具体报告
[System.Serializable]
public class ReportSaveData
{
    //标号
    public int index;

    //父报告的标号
    public int parentIndex;

    //是否解锁
    public bool unlocked;

    //是否打开
    public bool isOpen;


    public ReportSaveData(int _index, int _parentIndex, bool _unlocked, bool _isOpen)
    {
        index = _index;
        parentIndex = _parentIndex;
        unlocked = _unlocked;
        isOpen = _isOpen;
    }

}


//具体报告
[System.Serializable]
public class ReportConfigData
{
    //标号
    public int index;

    //父报告的标号
    public int parentIndex;

    //解锁需要的
    public int cost;

    //标题
    public string name;

    //描述
    public string description;


    public ReportConfigData(int _index, int _parendIndex, int _cost, string _name, string _description)
    {
        index = _index;
        parentIndex = _parendIndex;
        cost = _cost;
        name = _name;
        description = _description;
    }
}


