using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//次脚本用于判断物体的销毁，使用时需要开启trigger模式

public class DestroyThing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")//玩家触碰之后直接销毁
        {
            GameObject.Destroy(this.gameObject,0.1f);//销毁次物体
        }
    }
}
