using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public Text lifeText;
    public Text pieceText;
    private int pieceNum=0;
    private int lifeNum=0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Piece")
        {
            ++pieceNum;
            pieceText.text = pieceNum.ToString();
            Destroy(other.gameObject);
        }

        if(other.tag == "Life")
        {
            ++lifeNum;
            lifeText.text = lifeNum.ToString();
            Destroy(other.gameObject);
        }
    }
}
