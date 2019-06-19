using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRotate2 : MonoBehaviour
{

    //按钮 按E让物体绕X轴旋转90度
    public GameObject controledItem;//控制的物体

    public float rotationSpeed = 2f;//旋转的速度
    public bool isX = false;
    //public bool isY = true;
    //public bool isZ = false;
    [SerializeField]
    private float origionY;                //声明初始的X轴旋转值

    [SerializeField]
    private Quaternion targetRotation;    //声明旋转目标角度
    public float RotateAngle = 90;       //定义每次旋转的角度
    private int count;                  //声明一个量记录到目标角度需要进行旋转RotateAngle度的个数
// 由于每次旋转转  60(RotateAngle）度，所以从（0，0，0）到（0，120，0）需要旋转两个 60(RotateAngle) 度

    [SerializeField]
    private bool isPush = false;
    [SerializeField]
    private bool isOn = false;

    private void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         Push();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isOn = true;
            origionY = controledItem.transform.eulerAngles.x;    //获取当前Y轴旋转值赋给origionY
        }
           
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.name == "Player")

        {


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {

            isOn = false;
        }
    }



    void Push()
    {
        if(isOn)
        {
            if (Input.GetKeyDown(KeyCode.Q))  //按下赋值
            {
                count++;                                     //当按下Q键记录将count+1,一次转60,再按一次就应该从初始的Y变为Y+120,所以每按一次count+1
                targetRotation = Quaternion.Euler(RotateAngle * count + origionY, 0, 0) * Quaternion.identity;  //给旋转目标值赋值，由于只有Y轴动，所以目标值应是  (旋转角(RotateAngle)*需要旋转的个数(count)+origionY(物体初始Y轴旋转角))*Quarternion.identity(四元数的初始值,记住写就行！)
                Debug.Log("count为：" + count + "  角度为：" + targetRotation);
            }

            else  //当不按下就进行旋转
            {
                controledItem.transform.rotation = Quaternion.Slerp(controledItem.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);//利用Slerp插值让物体进行旋转  2是旋转速度 越大旋转越快

                if (Quaternion.Angle(targetRotation, controledItem.transform.rotation) < 1)
                {
                    controledItem.transform.rotation = targetRotation;                        //当物体当前角度与目标角度差值小于1度直接将目标角度赋予物体 让旋转角度精确到我们想要的度数
                }
            }

            //if (Input.GetKeyDown(KeyCode.E))  //按下赋值
            //{
            //    count++;                                     //当按下Q键记录将count+1,一次转60,再按一次就应该从初始的Y变为Y+120,所以每按一次count+1
            //    targetRotation = Quaternion.Euler(-RotateAngle * count + origionY, 0, 0) * Quaternion.identity;  //给旋转目标值赋值，由于只有Y轴动，所以目标值应是  (旋转角(RotateAngle)*需要旋转的个数(count)+origionY(物体初始Y轴旋转角))*Quarternion.identity(四元数的初始值,记住写就行！)
            //}

            //else  //当不按下就进行旋转
            //{
            //    controledItem.transform.rotation = Quaternion.Slerp(controledItem.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);//利用Slerp插值让物体进行旋转  2是旋转速度 越大旋转越快

            //    if (Quaternion.Angle(targetRotation, controledItem.transform.rotation) < 0.5)
            //    {
            //        controledItem.transform.rotation = targetRotation;                        //当物体当前角度与目标角度差值小于1度直接将目标角度赋予物体 让旋转角度精确到我们想要的度数
            //    }
            //}
        }
    }
        


}
