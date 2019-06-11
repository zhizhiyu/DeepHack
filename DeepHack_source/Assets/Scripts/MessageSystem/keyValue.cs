using UnityEngine;
using System.Collections;

public class KeyValue
{
    private string name;
    private object value;

    public KeyValue(string n, object v)
    {
        name = n;
        value = v;
    }

    public string getName()
    {
        return name;
    }
    public object getValue()
    {
        return value;
    }

}
