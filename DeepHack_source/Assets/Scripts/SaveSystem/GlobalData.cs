using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


    //通关后的存档文件列表
    public List<SceneSaveData> sceneSaveDatas;

    public List<ReportSaveData> reports;


    //构造函数
    public GlobalData()
    {
        Debug.Log("new global data");
        sceneSaveDatas = new List<SceneSaveData>(20);
        passedSceneNums = 0;
        currentSceneId = 0;

        reports = new List<ReportSaveData>();
        GenerateReportSaveData();
    }

    //通关时候新增一个通关存档数据
    public void AddNewSceneSaveData(int _sceneId, int _numOfCoins, int _numOfCollectedCoins, 
        int _numOfHidedCoins, int _numOfCollectedHidedCoins, float _usedTime, string _score)
    {
        if(_sceneId > passedSceneNums)
        {
            Debug.LogError("该关卡数比已通关数还大");
        }
        SceneSaveData newSceneSaveData = new SceneSaveData(_sceneId, _numOfCoins, _numOfCollectedCoins,
        _numOfHidedCoins, _numOfCollectedHidedCoins, _usedTime, _score);
        sceneSaveDatas.Add(newSceneSaveData);
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
        sceneSaveDatas[_sceneId].sceneId = _sceneId;
        sceneSaveDatas[_sceneId].numOfCoins = _numOfCoins;
        sceneSaveDatas[_sceneId].numOfCollectedCoins = _numOfCollectedCoins;
        sceneSaveDatas[_sceneId].numOfHidedCoins = _numOfHidedCoins;
        sceneSaveDatas[_sceneId].numOfCollectedHidedCoins = _numOfCollectedHidedCoins;
        sceneSaveDatas[_sceneId].usedTime = _usedTime;
    }

    //根据硬盘中的ReportConfigData文件产生ReportConfigData数据
    public List<ReportConfigData> ReadReportConfigData()
    {
        List<ReportConfigData> reportConfigDatas = new List<ReportConfigData>();
        

        /*
         
        To do
        硬盘操作
         
         
         */
         for(int i=0; i<10; i++)
        {
            ReportConfigData newReportConfigData = new ReportConfigData(i, i - 1, 10, "reportt", "no descriptions");
            reportConfigDatas.Add(newReportConfigData);
        }


        return reportConfigDatas;
    }

    //生成数据
    void GenerateReportSaveData()
    {
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

    //
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


