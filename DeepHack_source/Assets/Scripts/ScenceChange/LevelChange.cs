﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*场景跳转脚本
 通过挂载到canvas上并修改button的onclick使用
 所以的场景跳转都可以调用这个函数
 不知道需不需要使用异步场景跳转，我现在还不太了解
     */
public class LevelChange : MonoBehaviour
{
    private int currLevel=0;//当前选择关卡
    private GameObject[] levelModel;//关卡模型
    public int levelCount = 3;//关卡总数
    public GameObject levelDetail;//当前关卡细节
    public float xPos=15f;//关卡按钮点击后移动的距离
    public float zRota = -10f;//关卡按钮点击后旋转的角度
    //当前关卡细节
    public Text levelText;
    public Text nameText;
    public Text storyText;
    public Text pieceText;
    public Text achivementText;
    public Text timeText;
    public Image levelImg;

    // Start is called before the first frame update
    void Start()
    {
        currLevel = 0;
        levelDetail.SetActive(false);
        //获得所有关卡模型并隐藏
        levelModel = GameObject.FindGameObjectsWithTag("LevelModel").OrderBy(g => g.transform.GetSiblingIndex()).ToArray();
        foreach(var i in levelModel)
        {
            print(i);
            i.SetActive(false);
        }
    }

    /*场景跳转函数
     参数为需要跳转的场景名称
         */

    public void LoadNextLevel(string nextLevel)
    {
        print("nextLevel");
        SceneManager.LoadScene(nextLevel);
    }

    public void EnterLevel()
    {
        print("enterLevel");
        string levelName = "Level" + currLevel;
        SceneManager.LoadScene(levelName);
    }

    public void SelectLevel(int levelNum)
    {
        /*先进行判断是否已选中该关卡按钮*/
        if (currLevel != levelNum)
        {
            /*先还原上一个关卡按钮*/
            if (currLevel > 0)
            {
                //根据关卡号获得上一个关卡按钮GO
                string lastName = "Level" + currLevel.ToString();
                GameObject lastButton = GameObject.Find(lastName);
                print(lastButton.name);
                //把上一个关卡按钮位置还原
                lastButton.GetComponent<Transform>().position -= new Vector3(xPos, 0f, 0f);
                lastButton.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
                //上一关的关卡细节设为不显示
                levelDetail.SetActive(false);
                //获得上一关模型并设置为不显示
                if(currLevel<=levelModel.Length)
                    levelModel[currLevel - 1].SetActive(false);
            }

            /*调整当前选中的关卡按钮*/
            //根据关卡号获得当前点击关卡按钮GO
            currLevel = levelNum;
            string levelName = "Level" + currLevel.ToString();
            print(levelName);
            //Button levelButton = <Button>GameObject.Find(levelName);
            GameObject levelButton = GameObject.Find(levelName);
            print(levelButton.name);
            //当前点击关卡按钮弹出
            /*Vector3 target = levelButton.GetComponent<Transform>().position + new Vector3(15, 0f, 0f);
            levelButton.GetComponent<Transform>().position = Vector3.Lerp(levelButton.GetComponent<Transform>().position, target,0.5f);*/
            levelButton.GetComponent<Transform>().position += new Vector3(xPos, 0f, 0f);
            levelButton.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, zRota);
            
            //显示关卡细节
            levelDetail.SetActive(true);
            levelText.text = "stage " + currLevel.ToString();


            //显示关卡模型
            if (currLevel <= levelModel.Length)
                levelModel[currLevel - 1].SetActive(true);
        }
        
    }

}