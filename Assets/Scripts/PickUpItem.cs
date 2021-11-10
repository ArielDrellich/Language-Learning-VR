using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Transform destination;
    public float grabDistance = 0.5f;
    public float moveForce = 250f;
    public float distance;//make private when done
    bool isHolding = false;
    static object Lock = new object();//doesn't seem to work for some reason
    Holding holdingScript;
    void Start() {
        holdingScript = GameObject.Find("Item Destination").GetComponent<Holding>();
    }

    // Update is called once per frame
    void Update() { 
        Vector3 playerPos = destination.transform.position;
        distance = Vector3.Distance(GetComponent<Collider>().ClosestPoint(playerPos), playerPos);
        
        if(Input.GetButtonDown("Fire1")) {
            isHolding = true;
        }

        if(Input.GetButtonUp("Fire1") || distance > grabDistance) {
            putDown();
        }
        
        if (isHolding) {
            lock(Lock) {
                if (!holdingScript.IsHolding())
                    pickUp();
            }
            // MoveObject();
        }
    }

    void pickUp() {
        holdingScript.flip(true);
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = destination.position;
        transform.parent = destination;
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    void MoveObject() {
        if (distance > 0.1f) {
            Vector3 moveDirection = destination.position - transform.position;
            GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void putDown() {
        holdingScript.flip(false);
        isHolding = false;
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name != "Player")
            putDown();
    }

}
