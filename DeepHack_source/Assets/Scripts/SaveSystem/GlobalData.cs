using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalData
{
    public int userId;
    public int collicionCount;
    public GlobalData(int id)
    {
        userId = id;
        collicionCount = 0;
    }

}
