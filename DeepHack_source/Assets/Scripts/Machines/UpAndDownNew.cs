using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownNew : MonoBehaviour
{
    [Tooltip("电梯本体拖到这里")]
    public Transform tran;//电梯的变换信息

    public float upDistance = 1.0f;//上升的高度
    public float downDistance = 1.0f;//下降的高度
    public float moveForce = 1.0f;//移动的力量（速度）
    public bool isUp = false;//是否在上方

    [SerializeField]
    private float startPoint;//开始位置
    [SerializeField]
    private float endPoint;//终止位置
    private bool isMoving = false;//是否电梯在移动

    void Start()
    {
        if (isUp == false)//根据初始电梯在顶端还是底端来判断开始点和结束点
        {
            startPoint = tran.position.y;
            endPoint = startPoint + upDistance;
        }
        else
        {
            endPoint = tran.position.y;
            startPoint = endPoint - downDistance;
        }
    }

    void FixedUpdate()//防止坐电梯的时候穿出去了。。。
    {
        UpDownMove();
    }

    void UpDownMove()
    {
        if (isMoving)//电梯在移动之中
        {
            if (isUp)//根据是否在顶端来进行移动
            {
                tran.Translate(0, -moveForce, 0);
             
            }
            if (!isUp)
            {
                tran.Translate(0, moveForce, 0);

            }
        }
        if (tran.position.y > endPoint)//判断电梯是否该停下了，并且对起各种flag重置
        {

            tran.position = new Vector3(tran.position.x, endPoint, tran.position.z);
            isUp = true;
            isMoving = false;
        }
        if (tran.position.y < startPoint)
        {

            tran.position = new Vector3(tran.position.x, startPoint, tran.position.z);
            isUp = false;
            isMoving = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && isMoving == false)//用于判断电梯的移动与否
        {
            isMoving = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
