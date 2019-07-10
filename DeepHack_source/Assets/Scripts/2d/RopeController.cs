using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RopeController : MonoBehaviour
{
    public bool isPull;

    public GameObject canvas;

    public GameObject postDoor;

    // Start is called before the first frame update
    void Start()
    {
        isPull = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "CharacterRobotBoy")
        {
            Debug.Log("CharacterRobotBoy collision!");
            //isPull = !isPull;
            canvas.GetComponent<CanvasController>().SetStatus();

            postDoor.GetComponent<PostDoor>().activated = true;

        }
    }

}
