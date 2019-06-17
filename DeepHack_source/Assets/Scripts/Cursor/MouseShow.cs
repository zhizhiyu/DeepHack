using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseShow : MonoBehaviour
{

    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = !mesh.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        mesh.enabled = !mesh.enabled;
    }

    private void OnMouseExit()
    {
        mesh.enabled = !mesh.enabled;
    }
}
