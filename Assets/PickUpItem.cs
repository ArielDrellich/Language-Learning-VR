using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Transform destination;
    public float grabDistance = 1f;
    public float distance;//make private when done
    bool isHolding = false;

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
        
        if (isHolding)
            pickUp();
    }

    void pickUp() {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = destination.position;
        this.transform.parent = destination;
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    void putDown() {
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
