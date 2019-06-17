using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate_New : MonoBehaviour
{

    public float rotationSpeed = 3.0f;

    public bool isX = true;
    public bool isZ = false;

    [SerializeField]
    private float range;
    [SerializeField]
    private float totaliTime = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        range = transform.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        totaliTime += Time.deltaTime;
        if (isX)
        {

            transform.localPosition = new Vector3(0, Mathf.Cos(totaliTime * rotationSpeed) * range, Mathf.Sin(totaliTime * rotationSpeed) * range);
        }

        if (isZ)
        {
            transform.localPosition = new Vector3(Mathf.Sin(totaliTime * rotationSpeed) * range, Mathf.Cos(totaliTime * rotationSpeed) * range, transform.localPosition.z);
        }
  
    }
}
