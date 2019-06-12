using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏全局数据文件
[System.Serializable]
public class GlobalData
{
    public int passedSceneNums;
    public int currentSceneId;

    public List<SceneSaveData> sceneSaveDatas;


    public GlobalData()
    {
        Debug.Log("new global data");
        sceneSaveDatas = new List<SceneSaveData>();
        passedSceneNums = 0;
        currentSceneId = 0;
    }

    public void AddNewSceneSaveData(int _sceneId, int _numOfCoins, int _numOfCollectedCoins, 
        int _numOfHidedCoins, int _numOfCollectedHidedCoins, float _usedTime)
    {
        SceneSaveData newSceneSaveData = new SceneSaveData(_sceneId, _numOfCoins, _numOfCollectedCoins,
        _numOfHidedCoins, _numOfCollectedHidedCoins, _usedTime);
        sceneSaveDatas.Add(newSceneSaveData);
    }

    public SceneSaveData GetSceneSaveDataBySceneId(int _sceneId)
    {
        return sceneSaveDatas[_sceneId];
    }

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


}


//关卡配置文件
[System.Serializable]
public class SceneConfigData
{
    //该关卡的ID
    public int sceneId;

    //关卡全部普通的金币
    public int numOfCoins;

    //关卡全部隐藏的金币
    public int numOfHidedCoins;

    public SceneConfigData(int _sceneId, int _numOfCoins, int _numOfHidedCoins)
    {
        sceneId = _sceneId;
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

    public SceneSaveData(int _sceneId, int _numOfCoins, int _numOfCollectedCoins, int _numOfHidedCoins, int _numOfCollectedHidedCoins, float _usedTime)
    {
        sceneId = _sceneId;
        numOfCoins = _numOfCoins;
        numOfCollectedCoins = _numOfCollectedCoins;
        numOfHidedCoins = _numOfHidedCoins;
        numOfCollectedHidedCoins = _numOfCollectedHidedCoins;
        usedTime = _usedTime;
    }
}