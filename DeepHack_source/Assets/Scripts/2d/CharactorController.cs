using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public GameObject killZoneLeft;
    public GameObject killZoneRight;

    //public GameObject killZoneLeft1;
    //public GameObject killZoneRight1;

    //public GameObject killZoneLeft2;
    //public GameObject killZoneRight2;

    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;

    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if(count >= 30000)
        {
            count = 30;
        }

        if(count >= 30 && transform.position.x >= killZoneRight.transform.position.x)
        {
            Vector3 position = transform.position;
            position.x = killZoneLeft.transform.position.x;
            transform.position = position;
            //transform.position = killZoneLeft.transform.position;
            count = 0;
            //transform.position.Set(killZoneLeft.transform.position.x, transform.position.y, transform.position.z);

        }



        if (count >= 30 && transform.position.x <= killZoneLeft.transform.position.x)
        {
            Vector3 position = transform.position;
            position.x = killZoneRight.transform.position.x;
            transform.position = position;
            //transform.position = killZoneRight.transform.position;
            count = 0;
            //transform.position.Set(killZoneRight.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
