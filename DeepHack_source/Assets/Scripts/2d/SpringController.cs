using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class SpringController : MonoBehaviour
{
    public float force;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "CharacterRobotBoy")
        {
            animator.SetBool("status", true);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 800f));
            

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "CharacterRobotBoy")
        {
            animator.SetBool("status", false);

        }
    }
}
