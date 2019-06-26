using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Manager: MonoBehaviour
{
    public static float soundVolume = 1.0f;//音量大小
    public Slider soundSlider;//音量滑动条

    public void ChangeValue()
    {
        soundVolume = soundSlider.value;//改变音量值
    }

}
