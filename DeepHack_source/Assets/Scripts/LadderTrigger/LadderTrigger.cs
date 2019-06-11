using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name==player.name)//玩家触碰到梯子
        {
            print("TriggerEnter");
            other.gameObject.GetComponent<Move>().SetIsClimb(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == player.name)//玩家离开梯子
        {
            print("TriggerExit");
            other.gameObject.GetComponent<Move>().SetIsClimb(false);

        }
    }
}
