using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneController : MonoBehaviour
{
    public Text lifeText;
    public Text pieceText;
    public Text timeText;

    public GameObject pausePanelObject;

    //关卡的配置列表
    List<SceneConfigData> sceneConfigDatas;

    //该关卡存档文件
    [SerializeField]
    SceneSaveData sceneSaveData;

    //关卡Id，必须在Inspector上修改
    public int sceneId;


    //防止多次保存，测试用
    public bool canSave;

    // Start is called before the first frame update
    void Start()
    {
        //初始化

        canSave = true;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        timeText.text = Time.timeSinceLevelLoad.ToString("0.01");
    }

    private void FixedUpdate()
    {


    }

    //初始化
    void Init()
    {
        //设置当前关卡
        Game.globalData.currentSceneId = sceneId;

        //读取关卡配置文件
        sceneConfigDatas = Game.globalData.ReadSceneConfigDataFile();

        //初始化关卡存档文件
        sceneSaveData = new SceneSaveData(sceneId, sceneConfigDatas[sceneId].numOfCoins, 0, sceneConfigDatas[sceneId].numOfHidedCoins,
            0, 0, "0");

    }

    //玩家达到胜利点
    public void Win()
    {
        if(!canSave)
        {
            return;
        }
        Debug.Log("win!");
        //记录通关所用时间
        sceneSaveData.usedTime = Time.timeSinceLevelLoad;

        //增加通关存档
        Game.globalData.AddNewSceneSaveData(sceneSaveData);

        //
        if(sceneId == Game.globalData.passedSceneNums)
        {
            Game.globalData.passedSceneNums++;
        }

        //保存
        Game.Save();

        canSave = false;

        timeText.gameObject.SetActive(false);
        Game.StartNewScene("Level_2");


    }
    
    //增加金币
    public void AddCoin()
    {
        sceneSaveData.numOfCollectedCoins++;
        pieceText.text = sceneSaveData.numOfCollectedCoins.ToString();
    }

    //增加隐藏的金币
    public void AddHiddenCoin()
    {
        sceneSaveData.numOfCollectedHidedCoins++;
    }

    //测试用
    void ProcessInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(Game.isPaused);
            if(Game.isPaused)
            {
                Debug.Log("continue");
                pausePanelObject.GetComponent<Pause>().ClickContinue();
            }
            else
            {
                Debug.Log("show pause panel");
                pausePanelObject.GetComponent<Pause>().ClickPause();
            }

        }
    }



}
