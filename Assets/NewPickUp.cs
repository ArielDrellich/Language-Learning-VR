using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPickUp : MonoBehaviour
{
    SpriteRenderer reticle;
    RaycastHit hit;
    Rigidbody holding;
    [SerializeField] private float grabDistance = 5f;
    [SerializeField] private float distance;
    bool isHolding = false;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance);
        if (didHit && hit.collider.GetComponent<CanPickUp>()) {
            reticle.color = Color.red;
            if (Input.GetButtonDown("Fire1")) {
                holding = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (holding != null && !isHolding) {
                    PickUp();
                }
            }  
        } 

        if (!isHolding && !didHit)
            reticle.color = Color.white;

        //         distance = Vector3.Distance(holding.transform.position, transform.position);
        // if (isHolding && distance > 0.01)
        //     if (distance < 1)
        //         holding.transform.position = transform.position;
            // else holding.AddForce( (transform.position - holding.transform.position).normalized * 1 );

        if (isHolding && Input.GetButtonUp("Fire1")) {
            Drop();
        }
    }

    void PickUp() {
        isHolding = true;
        holding.transform.position = transform.position;
        holding.transform.parent = transform;
        holding.useGravity = false;
        // holding.isKinematic = true;
        holding.GetComponent<Collider>().isTrigger = true;
        holding.freezeRotation = true;
    }

    void Drop() {
        isHolding = false;            
        // holding.isKinematic = false;
        holding.GetComponent<Collider>().isTrigger = false;
        holding.freezeRotation = false;
        holding.useGravity = true;
        holding.transform.parent = null;
        holding = null;
    }
}
