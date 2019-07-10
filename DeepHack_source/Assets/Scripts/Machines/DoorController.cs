using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    /// <summary>
    /// 3d场景中的人物
    /// </summary>
    public  GameObject player;

    /// <summary>
    /// 3d场景中的摄像机
    /// </summary>
    public GameObject camera;

    /// <summary>
    /// 2d场景中的人物
    /// </summary>
    public GameObject player2D;

    /// <summary>
    /// 2d场景中的摄像机跟踪位置
    /// </summary>
    public GameObject playerPosition;


    /// <summary>
    /// 2d场景中的摄像机
    /// </summary>
    public GameObject secondaryCamera;


    // Start is called before the first frame update
    void Start()
    {
        player2D.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject == player)
        {
            Debug.Log("collide");
            if(Input.GetKeyDown(KeyCode.E))
            {
                player.SetActive(false);
                player2D.SetActive(true);
                camera.SetActive(false);
                secondaryCamera.SetActive(true);
                //camera.GetComponent<ThirdPersonCamera>().player = playerPosition.transform;
                //float y = camera.transform.rotation.eulerAngles.y;

                //Vector3 newRotation = camera.transform.rotation.eulerAngles;
                //newRotation.y += 180;

                //Quaternion quaternion = Quaternion.Euler(newRotation) * Quaternion.identity;

                //camera.transform.rotation = quaternion;
            }
        }
        
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{

    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player2D)
        {
            Debug.Log("collide 2d");
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.SetActive(true);
                player2D.SetActive(false);
                camera.SetActive(true);
                secondaryCamera.SetActive(true);
                //camera.GetComponent<ThirdPersonCamera>().player = player.transform;
            }
        }
    }
}
