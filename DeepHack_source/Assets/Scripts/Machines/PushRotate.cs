using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRotate : MonoBehaviour
{

    //这个版本不太OK
    public GameObject controledItem;
    public float rotationSpeed = 2f;
    //public float betweenDistance;
    public bool isX = false;
    public bool isY = true;
    public bool isZ = false;
    //public float rotateAngle = 0;
    //private float rotateOneTime = 30f;

    public SameRotate same;

    [SerializeField]
    private bool isPush = false;
    [SerializeField]
    private bool isOn = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPush)
            Push();
        if(!isPush&&isOn)
        {
            AngleCheck();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isOn = true;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.name == "Player")

        {
            if (Input.GetKey(KeyCode.F))
            {
                isPush = true;
            }
            if (!Input.GetKey(KeyCode.F))
            {
                isPush = false;
                AngleCheck();
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
            isPush = false;
        AngleCheck();
        isOn = false;
    }

    void Push()
    {

           if(Input.GetKey(KeyCode.Q))
            {
                if(isX)
                {
                    controledItem.transform.Rotate(new Vector3(rotationSpeed, 0, 0));
                    transform.Rotate(new Vector3(0, rotationSpeed, 0));
                    same.rotateAngle += rotationSpeed;

                }

            }
            if (Input.GetKey(KeyCode.E))
            {
                
                if (isX)
                {
                    
                    controledItem.transform.Rotate(new Vector3(-rotationSpeed, 0, 0));
                    transform.Rotate(new Vector3(0, -rotationSpeed, 0));
                    same.rotateAngle += rotationSpeed;
                }
            }
    }

    void AngleCheck()
    {
        int x = (int)(same.rotateAngle / 90);
        controledItem.transform.rotation = Quaternion.Euler(new Vector3(90 * x, 0, 0));
    }
}
