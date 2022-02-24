using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] 
    private Transform           HoldDestination;
    private CharacterController controller;
    private ReticleManager      reticle;
    private GameObject          player;
    private RaycastHit          hit;
    private Rigidbody           holding;
    private Vector3             nextPosition;
    private Collider[]          colliders;
    private bool                isHolding = false;
    private float               grabDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        controller = player.GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {        
        // searches for nearby colliders
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance);
        // useful for debugging only.
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*10, Color.green);

        bool canPickUp = false;
        if (didHit)
            canPickUp = hit.collider.GetComponent<CanPickUp>();
            
        // if we're holding and looking at a Placement, change to green.
        if (didHit && isHolding && hit.collider.GetComponent<CanPlaceOn>())
            reticle.SetColor(Color.green);
        // if we're holding but not looking at a Placement, stay red.
        else if (isHolding) 
                reticle.SetColor(Color.red);
        
        
        // checks if we're looking at a closeby item
        if (didHit && canPickUp) {
            reticle.SetColor(Color.red);
            // picks up item
            if (Input.GetButtonDown("Fire1")) {
                holding = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (holding != null && !isHolding) {
                    PickUpItem();
                }
            }  
        } 

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
        colliders = holding.GetComponents<Collider>();
        foreach (Collider collider in colliders) {
            collider.isTrigger = true;
            collider.enabled = false;
        }
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

        // turn off player temporarily to fix weird jumping bug when releasing
        player.SetActive(false);

        // move to new position
        holding.transform.position = nextPosition;

        // Restore everything to how it was before picking up
        holding.transform.parent = null;
        holding.isKinematic = false;
        foreach (Collider collider in colliders) {
            collider.enabled = true;
            collider.isTrigger = false;
        }
        holding.freezeRotation = false;
        holding.useGravity = true;
        holding = null;
        isHolding = false;  
        player.SetActive(true);        
    }
}
