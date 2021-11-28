using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    Renderer teleportRenderer;
    // Start is called before the first frame update
    void Start()
    {
        teleportRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
            teleportRenderer.enabled = true;
        else 
            teleportRenderer.enabled = false;
    }
}
