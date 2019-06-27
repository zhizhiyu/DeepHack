using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Manager: MonoBehaviour
{
    //public static float soundVolume = 1.0f;//音量大小
    public Slider soundSlider;//音量滑动条

    private void Start()
    {
        //数据持久化
        soundSlider.value = PlayerPrefs.GetFloat("volume", 1.0f);
    }

    public void ChangeValue()
    {
        //改变音量值
        PlayerPrefs.SetFloat("volume", soundSlider.value);
    }

    /*后面在退出游戏脚本中可以将playerprefs删除*/
}
