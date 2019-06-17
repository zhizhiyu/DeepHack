using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRotate : MonoBehaviour
{
    public GameObject controledItem;
    public float rotationSpeed = 2f;
    public float betweenDistance;

    private float rotateAngle = 0;
    [SerializeField]
    private bool isPush = false;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (!Input.GetKey(KeyCode.E)&& isPush   )
        //{
        //    isPush = false;
        //}
        Push();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")

        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isPush = true;
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                isPush = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.name == "Player")

        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isPush = true;
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                isPush = false;
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
            isPush = false;
    }

    void Push()
    {
        if (isPush)
        {
           if(Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.down * rotationSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up * rotationSpeed, Space.World);
            }
            controledItem.transform.rotation = transform.rotation;
        }
    }
}
