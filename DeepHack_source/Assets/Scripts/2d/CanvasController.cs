using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CanvasController : MonoBehaviour
{
    public int status;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        status = -1;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(status)
        {
            case -1:
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                break;
            case 0:
                animator.SetBool("up", true);
                animator.SetBool("down", false);
                break;
            case 1:
                animator.SetBool("down", true);
                animator.SetBool("up", false);
                break;
        }
    }

    public void SetStatus()
    {
        if(status == 0)
        {
            status = 1;
        }
        else
        {
            status = 0;
        }
    }
}
