using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform destination;
    public float grabDistance = 1f;
    public float moveForce = 250;
    private GameObject heldObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            if (heldObject == null) {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance)) {
                    PickUpObject(hit.transform.gameObject);
                }
            } else DropObject();
        }

        if (heldObject != null) {
            // MoveObject();
        }
    }

    void MoveObject() {
        if (Vector3.Distance(heldObject.transform.position, destination.transform.position) > 0.1f) {
            Vector3 moveDirection = (destination.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickUpObject(GameObject pickUpObject) {
        Rigidbody objectRigid = pickUpObject.GetComponent<Rigidbody>();
        if (objectRigid != null) {
            objectRigid.useGravity = false;
            objectRigid.transform.parent = destination;
            heldObject = pickUpObject;
            objectRigid.position = destination.position;////////////////
        }
    }

    void DropObject() {
        Rigidbody body = heldObject.GetComponent<Rigidbody>();
        body.useGravity = true;
        heldObject.transform.parent = null;
        heldObject = null;
    }
}
