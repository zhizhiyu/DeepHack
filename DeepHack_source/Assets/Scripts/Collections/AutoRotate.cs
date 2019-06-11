using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此脚本用于使物体绕某一固定轴以一定速度自转

public class AutoRotate : MonoBehaviour
{
    public float rotationSpeed = 3.0f;//旋转的速度

    public bool isX = false;//默认Y轴旋转
    public bool isY = true;
    public bool isZ = false;

    // Update is called once per frame
    void Update()
    {
        if(isX)
        {
            transform.Rotate(Vector3.left * rotationSpeed, Space.World); //物体自转
        }
        if (isY)
        {
            transform.Rotate(Vector3.down * rotationSpeed, Space.World); //物体自转
        }
        if (isZ)
        {
            transform.Rotate(Vector3.back * rotationSpeed, Space.World); //物体自转
        }
    }
}
