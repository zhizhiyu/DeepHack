using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadInformation : MonoBehaviour
{
    public int index;
    //public int level;//当前文档所在阶段
    public GameObject levelButton;//当前文档关联的阶段报告按钮
    public bool isUnlocked = false;//是否已解锁
    public bool isOpen = false;//是否已开启
    public Sprite unlockedInformation;//解锁替换按钮图
    public Sprite openInformation;//开启替换按钮图
    public Text cost;//当前按钮开启消耗碎片量
    public Text sumPieces;//碎片总量
    public GameObject informationPanel;//信息详情面板
    //public Text titleText;//文档标题
    public Text informationText;//记录文档文本
    public Text indexText;
    private Image img;//当前图片组件

    //声音
    /* public float soundVolume;//音量值
     public AudioSource buySound;//购买时声音*/
    public GameObject audioManager;//音效管理器

    void OnEnable()
    {
        print("OnEnable");
    }
    void Awake()
    {
        //读取设置的音量
        //soundVolume = Audio_Manager.soundVolume;
        //根据设置音量值改变此页面音量

        img = GetComponent<Image>();//获取当前绑定按钮的图片组件
    }

    void Start()
    {
        //读取情报碎片总量
        sumPieces.text = Game.globalData.coin.ToString();

        //判断父节点是否为信息开启按钮
        if(this.transform.parent.GetComponent<LoadInformation>()==null)
        { 
            print(transform.parent.ToString()+"Unlocking");
            isUnlocked = true;
            Game.globalData.reports[index].unlocked = isUnlocked;
        }

        isUnlocked = Game.globalData.reports[index].unlocked;

        //解锁节点
        if (isUnlocked)
        {
            print("Unlock");
            img.overrideSprite = unlockedInformation;//替换图片为已解锁背景
            SetActive(cost.gameObject, true);//显示开启消耗碎片量


        }

        isOpen = Game.globalData.reports[index].isOpen;

        //开启节点
        if (isOpen)
        {
            img.overrideSprite = openInformation;//替换图片为已开启背景
            SetActive(cost.gameObject, false);
        }
    }

    void Update()
    {
        //在每一帧中检测节点是否应该解锁或开启

        //判断父节点是否已被开启
        if (!isUnlocked && (this.transform.parent.GetComponent<LoadInformation>() == null||this.transform.parent.GetComponent<LoadInformation>().isOpen))
        {
            print(transform.parent.ToString() + "Unlocking");
            isUnlocked = true;
            Game.globalData.reports[index].unlocked = true;
        }


        isUnlocked = Game.globalData.reports[index].unlocked;
        //解锁节点
        if (isUnlocked && !isOpen)
        {
            print("Unlock");
            img.overrideSprite = unlockedInformation;//替换图片为已解锁背景
            SetActive(cost.gameObject, true);//显示开启消耗碎片量
        }

        /*isOpen = Game.globalData.reports[index].isOpen;
        //开启节点
        if (isOpen)
        {
            img.overrideSprite = openInformation;//替换图片为已开启背景
            SetActive(cost.gameObject, false);
        }*/
    }


    public void OpenInformation()
    {
        //如果没解锁
        if (!isUnlocked)
            return;

        int sumOfPieces = int.Parse(sumPieces.text.ToString());//碎片总量
        int costOfPieces = int.Parse(cost.text.ToString());//打开该信息所需的碎片消耗量
        if (!isOpen && costOfPieces <= sumOfPieces)
        {
            //播放音效
            audioManager.GetComponent<AudioOfInfamation>().playBuyRecord();

            //修改为打开状态
            sumOfPieces -= costOfPieces;
            sumPieces.text = sumOfPieces.ToString();//修改显示的碎片总量
            Game.globalData.coin = sumOfPieces;//修改碎片总量存档
            isOpen = true;
            Game.globalData.reports[index].isOpen = isOpen;
            //print("Opening");
            img.overrideSprite = openInformation;//替换图片为已开启背景
            //print("change");
            SetActive(cost.gameObject, false);

            //开启后，阶段总结报告中当前阶段已打开数量增加
            if(levelButton!=null)
               levelButton.GetComponent<InformationStage>().addCount();
        }

        if (isOpen)
        {
            print("opening");
            //显示信息面板
            SetActive(informationPanel, true);
            //读取故事文档
            List<ReportConfigData> reportConfigDatas = Game.globalData.ReadReportConfigData();//读取文档列表
            string recordTitle = reportConfigDatas[index].name;//显示文档标题
            string recordCont = reportConfigDatas[index].description;//文档内容
                                                                     //titleText.text = recordTitle;//标题显示到Text中
            informationText.text = "<b><size=35>" + recordTitle + "</size></b>" + recordCont;
            //显示索引
            indexText.text = (index + 1).ToString();
        }

    }

    /*public void CloseInformation()
    {
        SetActive(informationPanel, false);
        
    }*/

    static public void SetActive(GameObject go, bool state)
    {
        if (go == null)
        {
            return;
        }

        if (go.activeSelf != state)
        {
            go.SetActive(state);
        }
    }
}
