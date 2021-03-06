using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour, IClickable
{
    ReticleManager      reticle;  
    CharacterController controller;  
    Renderer            teleportRenderer;
    float               maxTeleportDistance = 15f;
    private static bool canTeleport;

    // Start is called before the first frame update
    void Start()
    {
        controller       = GameObject.Find("Player").GetComponent<CharacterController>();
        reticle          = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        teleportRenderer = GetComponent<Renderer>();

        canTeleport = true;
    }

    public void LookedAt(RaycastHit hit)
    {
        if (hit.distance <= maxTeleportDistance && canTeleport) 
        {
            if (Input.GetButton("Fire2"))
                reticle.SetColor(Color.blue);
            
            if (Input.GetButtonUp("Fire2")) 
            {
                Vector3 hitPosition = hit.collider.transform.position;
                controller.enabled = false;
                controller.transform.position = new Vector3(hitPosition.x, hitPosition.y + 1f, hitPosition.z);
                controller.enabled = true;
            }
        }
    }

    public static void CanTeleport(bool option)
    {
        canTeleport = option;
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

