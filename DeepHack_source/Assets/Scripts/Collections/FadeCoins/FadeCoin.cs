using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//本脚本用于控制隐藏金币的消失


public class FadeCoin : MonoBehaviour
{

    private float distance;//金币和人之间的距离
    private bool isFade = true;//判定金币是否是隐藏的，默认是隐藏的

    public FindDistance FindDistance;//脚本FindDistance用于查找距离
    public float detectDistance = 1.0f;//距离为多少时可以触发半透明
    public Material[] material;//通过调整材质来使得物体由透明变为半透明
    Renderer rend;//用于获取render组件来获取材质

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];//默认的材质（透明）
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = FindDistance.distance;//采用这个脚本来获取距离
        DetectDistance();


    }

    void DetectDistance()
    {
        if (distance <= detectDistance && isFade)
        {
            rend.sharedMaterial = material[1];//变换材质
            isFade = false;
        }
        if (distance > detectDistance && !isFade)
        {
            rend.sharedMaterial = material[0];//变换材质
            isFade = true;
        }
    }
}
