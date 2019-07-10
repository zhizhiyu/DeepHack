using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostDoor : MonoBehaviour
{
    //传送门 放在地上的
    public Transform otherDoor;//另一个门
    public PostDoor otherSrpt;//另一个门


    public bool isPost = false;

    public bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("good");
        if (other.gameObject.name == "Player")
        {
            Debug.Log("a ha");
            if (!activated)
            {
                return;
            }
            if (!isPost)
            {

                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                other.gameObject.SetActive(false);
                other.gameObject.transform.position = otherDoor.position + new Vector3(0, 1f, 0);
                other.gameObject.SetActive(true);

                otherSrpt.isPost = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "Player")
        {
            if (!activated)
            {
                return;
            }
            isPost = false;
        }
    }
}
