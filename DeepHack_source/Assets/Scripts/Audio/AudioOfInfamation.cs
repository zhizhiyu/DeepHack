using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* 管理情报库界面中的所有音频 
* 在每一个使用音频的场景中需要有一个音效管理类
* 管理在这个场景中涉及的所有的音频
* 然后把这个脚本挂载到场景中作为音频管理器的空物体中
* 将需要用到的音频赋值给它，别的脚本中调用它的播放函数
*/

public class AudioOfInfamation : MonoBehaviour
{
    //音量
    public float volume;
    //所有音频
    public AudioSource bgm;//BGM音源
    public AudioSource normal;//操作音效
    public AudioClip buyRecord;//购买记录时音效
    public AudioClip readNext;//上下篇音效

    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetFloat("volume",1.0f);//读取设置中的音量
        bgm.volume = volume;//修改BGM音源的音量
        normal.volume = volume;//修改音效的音量
        //播放场景BGM
        bgm.Play();
    }

    //播放购买记录音效
    public void playBuyRecord()
    {
        playSound(buyRecord);
    }

    //播放上下篇音效
    public void playReadNext()
    {
        playSound(readNext);
    }

    //播放特定音效函数
    private void playSound(AudioClip clip)
    {
        normal.clip = clip;
        normal.Play();
    }
}
