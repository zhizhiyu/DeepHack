using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostDoor2 : MonoBehaviour
{
    public Transform otherDoor;
    public Transform point;
    public PostDoor2 otherSrpt;
    public bool isPost = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (!isPost)
            {
                other.gameObject.transform.position = point.position/* + new Vector3(0, 1f, 0)*/;
                Quaternion r = otherDoor.rotation;
                other.gameObject.transform.rotation = otherDoor.rotation;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isPost = false;
        }
    }
}
