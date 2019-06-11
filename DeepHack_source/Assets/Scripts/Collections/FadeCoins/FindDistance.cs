using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此脚本用于集成化计算距离，比如一组隐藏金币，要是每一个都计算距离的话可能对性能有一定要求，所以化整为零，进行整体的判断

public class FindDistance : MonoBehaviour
{
    public Transform player;//此脚本所在物体检测距离的物体是什么

    public float distance = 999f;//距离初始化
    public float upLimit = 1.5f;//判断物体在哪个平面或者层面开始进行距离检测，如果一直进行距离检测的话，会比较浪费
    public float downLimit = 1.4f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.position.y > downLimit && player.position.y < upLimit)//物体在这个高度范围时开始计算距离
        {
            Vector3 offset = transform.position - player.position;//直接计算距离
            distance =  offset.magnitude;
        }
    }
}
