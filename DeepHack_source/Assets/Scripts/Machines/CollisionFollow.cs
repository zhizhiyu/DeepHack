using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/****************************************************************
 *Copyright(C)  2019 by luojinming All rights reserved. 
 *FileName:     CollisionFollow.cs
 *Author:       luojinming 
 *Version:      1.0 
 *Date:         6.13
 *Description:  碰撞后挂在物体上面
 *              
 *History:     (1):6.13,10:46
*****************************************************************/

public class CollisionFollow : MonoBehaviour
{
    public bool isFollow;
    public GameObject followObject;

    public int height;

    // Start is called before the first frame update
    void Start()
    {
        isFollow = false;
    }


    private void Update()
    {
        if (isFollow)
        {
            transform.parent.position = followObject.GetComponent<Transform>().position + new Vector3(0, 1, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isFollow && collision.gameObject.Equals(followObject))
        {
            Debug.Log("following");
            isFollow = true;
        }
    }
}
