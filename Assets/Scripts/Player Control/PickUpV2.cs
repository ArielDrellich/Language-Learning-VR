using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpV2 : MonoBehaviour
{
    [SerializeField] 
    Transform      holdDestination;
    [SerializeField] 
    float          grabDistance = 5f;
    GameObject     heldObject;
    Rigidbody      heldObjectRigidbody;
    ReticleManager reticle;
    Collider       playerCollider;
    RaycastHit     hit;
    float          pickUpForce = 150f;
    int            objectLayer;
    int            ignoreRaycastLayer;
    public float   releaseDistance;
    const  float   epsilon     = 0.00001f;

    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
     
        ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
        
        releaseDistance = Mathf.Abs(grabDistance - 
                                    Vector3.Distance(transform.position, holdDestination.position));

        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
    }

    void Update()
    {
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*grabDistance, Color.green);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance)) 
        {
            if (hit.transform.gameObject.GetComponent<CanPickUp>())
            {
                reticle.SetColor(Color.red);

                if (Input.GetButtonDown("Fire1") && heldObject == null)
                {
                    PickUp(hit.transform.gameObject);
                }
            }
            
        }

        if (heldObject != null)
        {
            reticle.SetColor(Color.red);

            if (Input.GetButtonUp("Fire1"))
                Drop();
            else
                MoveObj();
        }
    }


    void PickUp(GameObject pickObject)
    {
        if (pickObject.GetComponent<Rigidbody>())
        {
            heldObjectRigidbody = pickObject.GetComponent<Rigidbody>();
            heldObjectRigidbody.useGravity = false;
            heldObjectRigidbody.drag = 15;
            heldObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjectRigidbody.transform.parent = holdDestination;
            heldObject = pickObject;
            
            foreach (Collider collider in heldObject.GetComponentsInChildren<Collider>())
                Physics.IgnoreCollision(collider, playerCollider);

            // orients object towards camera
            Vector3 cameraForward = Camera.main.transform.forward;
            heldObject.transform.forward = new Vector3(cameraForward.x + epsilon,
                                             cameraForward.y, cameraForward.z + epsilon);

            objectLayer = heldObject.layer;
            heldObject.layer = ignoreRaycastLayer;
        }
    }

    void Drop()
    {
        heldObjectRigidbody.useGravity = true;
        heldObjectRigidbody.drag = 1;
        heldObjectRigidbody.constraints = RigidbodyConstraints.None;

        foreach (Collider collider in heldObject.GetComponentsInChildren<Collider>())
                Physics.IgnoreCollision(collider, playerCollider, false);

        heldObject.layer = objectLayer;

        heldObjectRigidbody.transform.parent = null;//
        heldObject = null;
    }

    void MoveObj()
    {
        float objDistance = Vector3.Distance(heldObject.transform.position, holdDestination.position);

        float minDist = grabDistance + 1; // +1 so if we didn't find anything closer it'll drop in the next if
        foreach (Collider collider in heldObject.GetComponentsInChildren<Collider>())
        {
            float closestDist =  Vector3.Distance(collider.ClosestPoint(transform.position), holdDestination.position);
            minDist = minDist < closestDist ? minDist : closestDist;
        }

        if (minDist > grabDistance )
        {
            Drop();
            return;
        }

        if (objDistance > 0.1f)
        {
            Vector3 moveDirection = (holdDestination.position - heldObject.transform.position);

            heldObjectRigidbody.AddForce(moveDirection * pickUpForce);
        }
    }

}
