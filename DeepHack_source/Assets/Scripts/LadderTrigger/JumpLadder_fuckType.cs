using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLadder_fuckType : MonoBehaviour
{
    public float jumpHeight = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.transform.Translate(0, jumpHeight, 0);
        }
    }
}
