using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此代码用于电梯控制物体上升下降，电梯效果，把此代码挂接到开关上，人在上升下降机关之中

public class UpAndDown : MonoBehaviour
{
    [Tooltip("电梯本体拖到这里")]
    public Transform tran;//电梯的变换信息
    [Tooltip("谁来坐电梯（默认玩家）")]
    public GameObject player;//允许坐电梯的物体

    public float upDistance = 1.0f;//上升的高度
    public float downDistance = 1.0f;//下降的高度
    public float moveForce = 1.0f;//移动的力量（速度）
    public bool isUp = false;//是否在上方




    private Transform tranP;//允许坐电梯物体的变换信息
    private float startPoint;//开始位置
    private float endPoint;//终止位置
    private bool isMoving = false;//是否电梯在移动

    void Awake()
    {
        tranP = player.GetComponent<Transform>();
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
                tranP.Translate(0, -moveForce, 0);//因为坐电梯的人有rigbody来进行移动，所以要给一个向下的力不然物体就在空中飞起，导致又多开一次电梯
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
