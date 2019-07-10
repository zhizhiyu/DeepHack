using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingLgihtController : MonoBehaviour
{
    MeshRenderer mr;
    public float i = 0;
    public bool isOne = false;// 代表 达不达到 1，默认一开始不为1 【按照i去算】
    // Update is called once per frame
    void Update()
    {
        mr = GetComponent<MeshRenderer>();
        if (!isOne)
        {
            i += Time.deltaTime / 2;
            mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, i);
            if (i >= 1)
                isOne = true;
        }
        else
        {
            i -= Time.deltaTime / 2;
            mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, i);
            if (i <= 0)
                isOne = false;
        }
    }
}

    
