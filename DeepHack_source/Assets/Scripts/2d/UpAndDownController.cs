using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownController : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(playerTransform.position, transform.position) <= 3 && Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.A))
        {
            GetComponentInParent<UpAndDown1>().status = 0; 
        }
        if(Vector3.Distance(playerTransform.position, transform.position) <= 3 && Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.D))
        {
            GetComponentInParent<UpAndDown1>().status = -3;
        }
    }
    
}
