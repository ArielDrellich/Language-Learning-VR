﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    SpriteRenderer reticle;
    RaycastHit hit;
    Rigidbody holding;
    [SerializeField] private float grabDistance = 5f;
    [SerializeField] private Transform HoldDestination;
    bool isHolding = false;
    private Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        // searches for nearby colliders
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance);
        bool canPickUp = false;
        if (didHit)
            canPickUp = hit.collider.GetComponent<CanPickUp>();
            
        // if we're holding and looking at a Placement, change to green.
        if (didHit && isHolding && hit.collider.GetComponent<CanPlaceOn>())
            reticle.color = Color.green;
        // if we're holding but not looking at a Placement, stay red.
        else if (isHolding) 
                reticle.color = Color.red;
        
        
        // checks if we're looking at a closeby item
        if (didHit && canPickUp) {
            reticle.color = Color.red;
            // picks up item
            if (Input.GetButtonDown("Fire1")) {
                holding = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (holding != null && !isHolding) {
                    PickUpItem();
                }
            }  
        } 

        // Set reticle to default if not looking at anything 
        if (!isHolding && (!didHit || !canPickUp))
            reticle.color = Color.white;

        // if we let go
        if (isHolding && Input.GetButtonUp("Fire1")) {
            Drop();
        }
    }

    void PickUpItem() {

        // Save reference for if we let go
        nextPosition = holding.transform.position;
        nextPosition.y += 0.5f;

        holding.useGravity = false;
        holding.isKinematic = true;
        // So the held item doesn't bump into things or block the raycast
        holding.GetComponent<Collider>().isTrigger = true;
        holding.GetComponent<Collider>().enabled = false;
        holding.freezeRotation = true;

        // Set new position for holding
        holding.transform.parent = transform;
        holding.transform.position = HoldDestination.transform.position;
        isHolding = true;
    }

    void Drop() {
        // Detect placeable surface
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance);
        if (didHit && hit.collider.GetComponent<CanPlaceOn>()) {
            nextPosition = hit.collider.transform.position;
            nextPosition.y += 0.5f;
        }
        // new position
        holding.transform.position = nextPosition;

        // Restore everything to how it was before picking up
        isHolding = false;            
        holding.transform.parent = null;
        holding.isKinematic = false;
        holding.GetComponent<Collider>().isTrigger = false;
        holding.GetComponent<Collider>().enabled = true;
        holding.freezeRotation = false;
        holding.useGravity = true;
        holding = null;
    }
}
