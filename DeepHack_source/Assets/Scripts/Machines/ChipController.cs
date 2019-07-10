using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipController: HighlightableObject
{
    public bool move = false;

    //public float y;
    //public float x;
    //public float z;

    public float speed;

    bool up = true;

    public Vector3 position1;
    public Vector3 position2;

    void Start()
    {
        ConstantOn(Color.yellow);//红色高亮
        //FlashingOn(Color.red, Color.green);//根据时间间隔闪烁高亮
        //Invoke("StopColor", 2f);


    }
    void StopColor()
    {
        ConstantOff();//关闭高亮
        FlashingOff();//关闭闪烁高亮
    }

    private void FixedUpdate()
    {
        if (move)
        {
            mmove();
        }

    }

    void mmove()
    {
        if(up)
        {
            if(Vector3.Distance(transform.localPosition, position1) <= 0.1)
            {
                up = false;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, position1, Time.fixedDeltaTime * speed);
            }
        }
        else
        {
            if (Vector3.Distance(transform.localPosition, position2) <= 0.1)
            {
                up = true;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, position2, Time.fixedDeltaTime * speed);
            }
        }
    }

}
