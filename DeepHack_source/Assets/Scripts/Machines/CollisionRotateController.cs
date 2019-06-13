using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/****************************************************************
 *Copyright(C)  2019 by luojinming All rights reserved. 
 *FileName:     CollisionRotateController.cs
 *Author:       luojinming 
 *Version:      1.0 
 *Date:         6.13
 *Description:  机关碰撞后旋转90度
 *              
 *History:     (1):6.13,9:53
*****************************************************************/
public class CollisionRotateController : MonoBehaviour
{
    public bool isX;
    public bool isY;
    public bool isZ;
    public bool isRotate;

    public int angle;



    // Start is called before the first frame update
    void Start()
    {
        isRotate = false;
        angle = 0;
    }

    private void FixedUpdate()
    {
        if(isRotate)
        {
            if(isX)
            {
                transform.Rotate(new Vector3(1, 0, 0));
            }
            if(isY)
            {
                transform.Rotate(new Vector3(0, 1, 0));
            }
            if(isZ)
            {
                transform.Rotate(new Vector3(0, 0, 1));
            }
            angle++;
        }
        if(angle >= 90)
        {
            isRotate = false;
            angle = 0;
        }

    }

    //碰撞后旋转
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Sphere")
        {
            Debug.Log("cube1 collision entered!");
            isRotate = true;

            angle = 0;

        }
    }

}
