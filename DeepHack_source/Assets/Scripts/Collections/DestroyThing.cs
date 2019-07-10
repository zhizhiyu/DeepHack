using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//次脚本用于判断物体的销毁，使用时需要开启trigger模式

public class DestroyThing : MonoBehaviour
{
    public GameObject sceneControllerObject;
    public GameObject fatherObject;

    /// <summary>
    /// 类型，0为碎片，1为隐藏碎片，2为加生命的
    /// </summary>
    public int type = 0;


    /// <summary>
    /// 3d场景中的碰撞处理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("destroy");
        switch(type)
        {
            case 0:
                sceneControllerObject.GetComponent<SceneController>().AddCoin();
                break;
            case 1:
                sceneControllerObject.GetComponent<SceneController>().AddHiddenCoin();
                break;
            case 2:
                sceneControllerObject.GetComponent<SceneController>().ModifyLife(1);
                break;
        }
        GameObject.Destroy(fatherObject, 0.1f);//销毁此物体

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("destroy");
        switch (type)
        {
            case 0:
                sceneControllerObject.GetComponent<SceneController>().AddCoin();
                break;
            case 1:
                sceneControllerObject.GetComponent<SceneController>().AddHiddenCoin();
                break;
            case 2:
                sceneControllerObject.GetComponent<SceneController>().ModifyLife(1);
                break;
        }
        GameObject.Destroy(fatherObject, 0.1f);//销毁此物体
    }
}
