using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//触发旋转
public class TriggerRotate : MonoBehaviour
{

    public GameObject gobj;//要旋转的物体
    public float rotateX = 0;//旋转后的X
    public float rotateSpeed = 10f;//旋转速度

    private bool isTrigger=false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       //旋转
        if (isTrigger&& gobj.transform.eulerAngles.x!=rotateX)
        {
            if(gobj.transform.eulerAngles.x>rotateX)
            {
                if(gobj.transform.eulerAngles.x-rotateX<rotateSpeed)
                {
                    gobj.transform.Rotate(-(gobj.transform.eulerAngles.x - rotateX), 0, 0);
                }
                else
                gobj.transform.Rotate(new Vector3(-rotateSpeed, 0, 0));
            }
            else if (gobj.transform.eulerAngles.x < rotateX)
            {
                if (rotateX-gobj.transform.eulerAngles.x < rotateSpeed)
                {
                    gobj.transform.Rotate(rotateX - gobj.transform.eulerAngles.x, 0, 0);
                }
                else
                gobj.transform.Rotate(new Vector3(rotateSpeed, 0, 0));
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        Debug.Log("trigger enter");
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    isTrigger = true;
    //    Debug.Log("collision enter");
    //}
}
