using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class UpAndDown1 : MonoBehaviour
{
    public Transform originTransform;
    public Transform midTransform;
    public Transform destinationTransform;

    public int status;

    public float smooth;

    public float startTime;


    // Start is called before the first frame update
    void Start()
    {
        status = -3;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //switch (status)
        //{
        //    case 0:
        //        Debug.Log("向上");
        //        transform.position = Vector3.Lerp(transform.position, midTransform.position, Time.deltaTime * smooth);
        //        if(transform.position == midTransform.position)
        //        {
        //            startTime = Time.time;
        //            status = -1;
        //        }

        //        break;

        //    case 1:
        //        Debug.Log("向左");
        //        transform.position = Vector3.Lerp(transform.position, destinationTransform.position, Time.deltaTime * smooth);
        //        if(transform.position == destinationTransform.position)
        //        {
        //            status = -2;
        //        }

        //        break;

        //    case 2:
        //        Debug.Log("向右");
        //        transform.position = Vector3.Lerp(transform.position, midTransform.position, Time.deltaTime * smooth);
        //        if(transform.position == midTransform.position)
        //        {
        //            status = 3;
        //        }
        //        break;

        //    case 3:
        //        Debug.Log("向下");
        //        transform.position = Vector3.Lerp(transform.position, originTransform.position, Time.deltaTime * smooth);
        //        if(transform.position == originTransform.position)
        //        {
        //            status = -3;
        //        }
        //        break;

        //    case -1:
        //        Debug.Log("在中间停留0.2s");
        //        if(Time.time - startTime >= 0.2)
        //        {
        //            status = 1;
        //        }

        //        break;
        //    case -2:
        //        Debug.Log("在左边停留，直到没有玩家停留超过1s");
        //        if(Time.time - startTime >= 1)
        //        {
        //            status = 2;
        //        }
        //        break;

        //    case -3:
        //        Debug.Log("静止");
        //        break;
        //}
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "player" && status == -2)
        {
            startTime = Time.time;
        }
    }
}
