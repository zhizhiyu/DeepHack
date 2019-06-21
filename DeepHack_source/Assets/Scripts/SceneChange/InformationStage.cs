using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationStage : MonoBehaviour
{
    public int index;//文档在文件列表中的索引
    private int count;//当前这一层被打开的文件
    public int maxCount;//当前这一层的文件总数
    private bool isOpen;

    public Sprite newBackground;//解锁并打开后替换的背景图
    public Text title;//按钮标题

    private Image img;//当前图片组件

    //关于文档详情面板
    public GameObject informationPanel;//信息详情面板
    //public Text titleText;//文档标题
    public Text informationText;//记录文档文本
    public Text indexText;
    public GameObject switchImg;//不需显示的上下页按钮

    void Awake()
    {
        img = GetComponent<Image>();//获取当前绑定按钮的图片组件
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = Game.globalData.reports[index].isOpen;//读取此文档是否打开的存档
        if (isOpen)
        {
            //更换背景
            img.overrideSprite = newBackground;
            SetActive(title.gameObject, true);
        }
    }

    public void addCount()
    {
        ++count;

        //判断是否足够开启阶段总结文档
        if (count == maxCount)  //判断是否足够开启阶段总结文档
        {
            isOpen = true;
            //并修改存档
            Game.globalData.reports[index].unlocked = true;
            Game.globalData.reports[index].isOpen = true;

            //修改背景
            img.overrideSprite = newBackground;
            SetActive(title.gameObject, true);
        }
    }

    public void openStageRecord()
    {
        if (!isOpen)
            return;
        //显示信息面板
        SetActive(informationPanel, true);
        //不显示上下页按钮
        SetActive(switchImg, false);
        //读取故事文档
        List<ReportConfigData> reportConfigDatas = Game.globalData.ReadReportConfigData();//读取文档列表
        string recordTitle = reportConfigDatas[index].name;//显示文档标题
        string recordCont = reportConfigDatas[index].description;//文档内容
                                                                 //titleText.text = recordTitle;//标题显示到Text中
        informationText.text = "<b><size=35>" + recordTitle + "</size></b>" + recordCont;
        //显示索引
        indexText.text = (index + 1).ToString();
    }

    public void CloseInformation()
    {
        //把关闭的上下页按钮激活
        SetActive(switchImg, true);
        SetActive(informationPanel, false);
        
    }


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
