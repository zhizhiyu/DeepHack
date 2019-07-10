using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerPositionController : MonoBehaviour
{
    public Transform playerTransform;
    public float z;
    public float smooth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Transform newTransform = playerTransform;
        //newTransform.position.z = transform.position.z;

        //Vector3 newPosition = playerTransform.position;
        Vector3 newPosition = Vector3.Lerp(transform.position, playerTransform.position, smooth * Time.deltaTime);
        newPosition.z = transform.position.z;
        //transform.forward = playerTransform.forward;
        transform.position = newPosition;
        
    }
}
