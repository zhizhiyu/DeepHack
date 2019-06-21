using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StagePass : MonoBehaviour
{
    public int currPassLevel;//当前通关号
    public Text collectionText;//本关收集物
    public Text timeText;//本关所用时间
    public Text scoreText;//本关评级
    // Start is called before the first frame update
    void Start()
    {
        int currCollect = 0;
        int sumOfCollection = 0;
        collectionText.text = currCollect.ToString() + "/" + sumOfCollection.ToString();//显示本关收集情况
        timeText.text = null;//显示本关通关时间
        scoreText.text = null;//显示本关评级
    }

    //各按钮点击函数
    
    //下一关
    void clickNext()
    {
        string nextLevel = "level_" + (currPassLevel + 1).ToString();//下一关场景名
        SceneManager.LoadScene(nextLevel);//加载下一关
    }

    //重玩
    void clickRestart()
    {
        Game.ReStartScene();//重新开始游戏
    }

    //返回菜单
    void clickReturn()
    {
        SceneManager.LoadScene("MainMenu");//返回主菜单
    }
}
