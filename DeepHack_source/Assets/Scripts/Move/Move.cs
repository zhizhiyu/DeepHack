using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 6.0F;//移动速度
    public float gravity = 10.0F;
    public bool isClimb = false;//是否在爬梯子

    private Vector3 moveDirection = Vector3.zero;//移动方向
    private CharacterController controller;//unity的角色控制器

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    { 
        if (!isClimb)//走
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);//从物体坐标转化到世界坐标
            moveDirection.y -= gravity * Time.deltaTime;//加重力
            controller.Move(moveDirection * speed * Time.deltaTime); 
        }
        else//爬梯子
        {
            if (Input.GetKey(KeyCode.Space))//空格键爬
            {
                controller.Move( Vector3.up* speed * Time.deltaTime);
            }

        }      
    }

    public void SetIsClimb(bool IsClimb)
    {
        isClimb = IsClimb;
    }
}
