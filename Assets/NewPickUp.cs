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
    private Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance);
        if (didHit && isHolding && hit.collider.GetComponent<CanPlaceOn>())
            reticle.color = Color.green;
        else if (isHolding) 
                reticle.color = Color.red;
        
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

        if (isHolding && Input.GetButtonUp("Fire1")) {
            Drop();
        }
    }

    void PickUp() {

        // test
        // holding.transform.localScale = new Vector3(5,5,5);
        nextPosition = holding.transform.position;///
        isHolding = true;
        holding.transform.position = transform.position;
        holding.transform.parent = transform;
        holding.useGravity = false;
        // holding.isKinematic = true;///
        holding.GetComponent<Collider>().isTrigger = true;
        // holding.freezeRotation = true;
    }

    void Drop() {
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance);
        if (didHit && hit.collider.GetComponent<CanPlaceOn>()) {
            nextPosition = hit.transform.position;
            nextPosition.y += 0.25f;
        }
        isHolding = false;            
        // holding.isKinematic = false;///
        holding.GetComponent<Collider>().isTrigger = false;
        // holding.freezeRotation = false;
        holding.useGravity = true;
        holding.transform.parent = null;
        nextPosition.y += 0.25f;
        holding.transform.position = nextPosition;
        holding = null;
    }
}
