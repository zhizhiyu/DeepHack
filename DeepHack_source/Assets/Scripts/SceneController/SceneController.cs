using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneController : MonoBehaviour
{
    public Text lifeText;
    public Text pieceText;
    public Text timeText;

    /// <summary>
    /// 玩家，用于播放闪烁
    /// </summary>
    public GameObject player;

    /// <summary>
    /// 暂停面板
    /// </summary>
    public GameObject pausePanelObject;

    /// <summary>
    /// 失败面板
    /// </summary>
    public GameObject failPanelObject;

    /// <summary>
    /// 音效控制脚本
    /// </summary>
    public GameObject audioObject;


    /// <summary>
    /// 通关面板
    /// </summary>
    public GameObject passPanel;

    /// <summary>
    /// 对话面板
    /// </summary>
    public GameObject dialogPanel;

    /// <summary>
    /// 设置面板
    /// </summary>
    public GameObject settingPanel;

    //关卡的配置列表
    List<SceneConfigData> sceneConfigDatas;

    //该关卡存档文件
    [SerializeField]
    SceneSaveData sceneSaveData;

    //关卡Id，必须在Inspector上修改
    public int sceneId;


    //防止多次保存，测试用
    public bool canSave;

    //public enum status { normal, panelShowed};

    //当前状态
    //public status currentStatus;

    //生命值
    public int life = 3;

    //受伤后无敌时间
    public float time = 3f;

    //上次受伤的时间
    float lastTime = -10;

    //是否已经失败
    bool isFail = false;


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
        DetectFail();
    }

    //初始化
    void Init()
    {
        //设置当前关卡
        Game.globalData.currentSceneId = sceneId;

        //读取关卡配置文件
        sceneConfigDatas = Game.globalData.ReadSceneConfigDataFile();

        //初始化关卡存档文件
        sceneSaveData = new SceneSaveData(sceneId, sceneConfigDatas[sceneId - 1].numOfCoins, 0, sceneConfigDatas[sceneId - 1].numOfHidedCoins,
            0, 0, "0");

        Game.globalData.life = Game.globalData.life >= 3 ? Game.globalData.life : 3;
        life = Game.globalData.life;

        lifeText.text = life.ToString();

    }

    //玩家达到胜利点
    public void Win()
    {
        audioObject.GetComponent<AudioOfLevel>().playPassLevel();

        if (!canSave)
        {
            return;
        }
        Debug.Log("win!");
        //记录通关所用时间
        sceneSaveData.usedTime = Time.timeSinceLevelLoad;

        //增加通关存档
        Game.globalData.AddNewSceneSaveData(sceneSaveData);

        //
        if (sceneId == Game.globalData.passedSceneNums)
        {
            Game.globalData.passedSceneNums++;
        }

        //保存
        Game.Save();

        canSave = false;

        timeText.gameObject.SetActive(false);

        passPanel.SetActive(true);
        passPanel.GetComponent<StagePass>().Init(sceneSaveData.numOfCollectedCoins + sceneSaveData.numOfCollectedHidedCoins,
            sceneSaveData.numOfCoins + sceneSaveData.numOfHidedCoins, sceneSaveData.usedTime.ToString(), 
            CalculateScore(sceneSaveData.usedTime, sceneSaveData.numOfCollectedCoins + sceneSaveData.numOfCollectedHidedCoins), true);
    }

    /// <summary>
    /// 根据关卡id， 使用的时间，以及获取的碎片数量来计算出最终的评分
    /// </summary>
    /// <param name="usedTime">使用的时间</param>
    /// <param name="coinNum">获取的金币数量</param>
    /// <returns></returns>
    public string CalculateScore(float usedTime, int coinNum)
    {
        string str = "";
        int score = 0;

        //下面是一些配置数据
        int[][] numArr = new int[][] { new int[]{ int.MaxValue, 20, 15, 10, 6, 0 },
            new int[] {int.MaxValue, 27, 23, 19, 12, 0 },
            new int[] {int.MaxValue, 19, 14, 10, 6, 0 } };
        int[] numScoreArr = new int[] { 60, 50, 40, 30, 0 };

        float[][] timeArr = new float[][] { new float[] { 0, 1 * 60, 3 * 60, 5 * 60, float.MaxValue },
            new float[]{ 0, 1 * 60, 3 * 60, 5 * 60, float.MaxValue},
            new float[]{ 0, 1 * 60, 3 * 60, 5 * 60, float.MaxValue } };
        int[] timeScoreArr = new int[] { 20, 15, 10, 5 };


        int[][] scoreArr = new int[][] { new int[]{ 90, 80, 75, 65, 50, 35 },
            new int[]{ 90, 80, 75, 65, 50, 35 },
            new int[]{ 90, 80, 75, 65, 50, 35} };
        string[] resArr = new string[] { "S", "A", "B", "C", "D" };

        for (int i = 0; i < numArr.Length - 1; i++)
        {
            if (coinNum <= numArr[sceneId][i] && coinNum > numArr[sceneId][i + 1])
            {
                score += numScoreArr[i];
            }
        }

        for (int i = 0; i < timeArr.Length - 1; i++)
        {
            if (usedTime >= timeArr[sceneId][i] && usedTime < timeArr[sceneId][i + 1])
            {
                score += timeScoreArr[i];
            }
        }

        for (int i = 0; i < scoreArr.Length - 1; i++)
        {
            if (score <= scoreArr[sceneId][i] && score > scoreArr[sceneId][i + 1])
            {
                str = resArr[i];
            }
        }
        return str;
    }

    //增加金币
    public void AddCoin()
    {
        sceneSaveData.numOfCollectedCoins++;
        pieceText.text = (sceneSaveData.numOfCollectedCoins + sceneSaveData.numOfCollectedHidedCoins).ToString();


        audioObject.GetComponent<AudioOfLevel>().playPickPieces();
    }

    //增加隐藏的金币
    public void AddHiddenCoin()
    {
        sceneSaveData.numOfCollectedHidedCoins++;
        pieceText.text = (sceneSaveData.numOfCollectedCoins + sceneSaveData.numOfCollectedHidedCoins).ToString();

        audioObject.GetComponent<AudioOfLevel>().playPickPieces();

    }

    //测试用
    void ProcessInput()
    {
        if(dialogPanel.activeInHierarchy == true || settingPanel.activeInHierarchy == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(Game.isPaused);
            if (Game.isPaused)
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

    /// <summary>
    /// 受到攻击
    /// </summary>
    public void Attack()
    {
        if(Time.time - lastTime >= time)
        {
            Debug.Log("hurt!");
            ModifyLife(-1);

            player.GetComponent<PersonHurt>().PlayOneTime();

            lastTime = Time.time;

            audioObject.GetComponent<AudioOfLevel>().playDetected();

        }
    }

    /// <summary>
    /// 检测是否失败
    /// </summary>
    void DetectFail()
    {
        if(life <= 0 && !isFail)
        {
            Debug.Log("fail!");
            failPanelObject.SetActive(true);

            /*failPanelObject.GetComponent<StageFailed>().Init(sceneId, Time.timeSinceLevelLoad, "fail", sceneSaveData.numOfCollectedCoins + sceneSaveData.numOfCollectedHidedCoins,
                sceneSaveData.numOfCoins + sceneSaveData.numOfHidedCoins);*/
            
            isFail = true;
        }
    }

    /// <summary>
    /// 修改生命值
    /// </summary>
    /// <param name="num">生命值数量</param>
    public void ModifyLife(int num)
    {
        life += num;
        Game.globalData.life += num;
        audioObject.GetComponent<AudioOfLevel>().playPickLife();

        lifeText.text = life.ToString();

    }

}
