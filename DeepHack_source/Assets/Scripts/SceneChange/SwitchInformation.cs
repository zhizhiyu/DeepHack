using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchInformation : MonoBehaviour
{

    public Text indexText;
    public Text informationText;//记录文档文本
    public AudioSource readNextAudio;//翻页声音

    public void LastInformation()
    {
        print("last record loading");
        int index = int.Parse(indexText.text) - 1;

        //读取上一个索引的文档
        //读取上一索引的记录，判断是否已开启
        while (--index >= 0 && !Game.globalData.reports[index].isOpen) { } //循环直到上一条记录已打开，或已是第一条记录

        if (index >= 0) //已打开的上一条记录索引大于等于0
        {
            //翻页声音播放
            this.readNextAudio.Play();

            //显示当前索引
            indexText.text = (index + 1).ToString();
            //读取记录文档
            List<ReportConfigData> reportConfigDatas = Game.globalData.ReadReportConfigData();//读取文档列表
            string recordTitle = reportConfigDatas[index].name;//显示文档标题
            string recordCont = reportConfigDatas[index].description;//文档内容
                                                                     //titleText.text = recordTitle;//标题显示到Text中
            informationText.text = "<b><size=35>" + recordTitle + "</size></b>" + recordCont;
        }
    }

    public void NextInformation()
    {
        print("next record loading");
        int index = int.Parse(indexText.text) - 1;


        //读取下一个索引的文档
        //读取下一索引的记录，判断是否已开启
        while (++index < 13 && !Game.globalData.reports[index].isOpen) { } //循环直到下一条记录已打开，或已是最后一条记录

        if (index < 13) //已打开的下一条记录索引小于等于最后一条
        {

            //翻页声音播放
            this.readNextAudio.Play();

            //显示当前索引
            indexText.text = (index + 1).ToString();
            //读取记录文档
            List<ReportConfigData> reportConfigDatas = Game.globalData.ReadReportConfigData();//读取文档列表
            string recordTitle = reportConfigDatas[index].name;//显示文档标题
            string recordCont = reportConfigDatas[index].description;//文档内容
                                                                     //titleText.text = recordTitle;//标题显示到Text中
            informationText.text = "<b><size=35>" + recordTitle + "</size></b>" + recordCont;
        }
    }
}
