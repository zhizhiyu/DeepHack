using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDetect : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameObject player;


    /// <summary>
    /// 关卡控制器
    /// </summary>
    public GameObject sceneControllerObject;



    public float distance = 7f;//射线长度
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ray cast");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);//新建射线


        Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);//画出射线，在Scene窗口里可见，Game窗口里不可见
        RaycastHit hit;

        //如果射线射到玩家，玩家死亡
        if (Physics.Raycast(ray, out hit, distance)&&hit.collider.gameObject == player)
        {
            print("get hurt!!");

            sceneControllerObject.GetComponent<SceneController>().Attack();



        }
          
    }
}
