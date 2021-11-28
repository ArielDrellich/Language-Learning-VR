using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    RaycastHit hit;
    GameObject player;
    float teleportDistance = 15f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, teleportDistance);
        bool canTeleport = false;
        if (didHit)
            canTeleport = hit.collider.GetComponent<TeleportPad>();
        if (canTeleport && Input.GetButtonUp("Fire2")) {
            Vector3 hitPosition =  hit.collider.transform.position;
            // player.transform.position = new Vector3(hitPosition.x, hitPosition.y + 1.5f, hitPosition.z);////
            CharacterController controller = player.GetComponent<CharacterController>();
            controller.enabled = false;
            controller.transform.position = new Vector3(hitPosition.x, hitPosition.y + 1.5f, hitPosition.z);
            controller.enabled = true;
        }
    }
}
