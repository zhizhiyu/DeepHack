using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVertical : MonoBehaviour
{
    public float targetAngel = -90;
    public float speed = 1.0f;
    public GameObject gameobj;

    private bool isTrigger = false;
    private  Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger)
        {
            targetRotation = Quaternion.Euler(targetAngel, 0, 0) * Quaternion.identity;
            gameobj.transform.rotation = Quaternion.Slerp(gameobj.transform.rotation, targetRotation, speed * Time.deltaTime);
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        print("istriggle");
    }
}
