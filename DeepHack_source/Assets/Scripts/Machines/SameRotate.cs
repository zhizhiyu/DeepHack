using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameRotate : MonoBehaviour
{
    [SerializeField]
    public float rotateAngle;
    // Start is called before the first frame update
    void Awake()
    {
        rotateAngle = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
