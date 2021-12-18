using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    RaycastHit          hit;
    CharacterController controller;
    ReticleManager      reticle;
    float               teleportDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<CharacterController>();
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, teleportDistance);
        bool canTeleport = false;
        
        if (didHit)
            canTeleport = hit.collider.GetComponent<TeleportPad>();

        if (canTeleport && Input.GetButton("Fire2"))
            reticle.SetColor(Color.blue);

        if (canTeleport && Input.GetButtonUp("Fire2")) {
            Vector3 hitPosition =  hit.collider.transform.position;
            controller.enabled = false;
            controller.transform.position = new Vector3(hitPosition.x, hitPosition.y + 1.5f, hitPosition.z);
            controller.enabled = true;
        }
    }
}
