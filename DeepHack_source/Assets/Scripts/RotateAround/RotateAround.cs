using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject Axis; //物体需要公转的参照物
    public float _RotationSpeed=2.0f; //公转速度
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         this.transform.RotateAround(Axis.transform.position, Vector3.up, _RotationSpeed);//将需要公转的参照物拖入，设置公转
    }
}
