using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Transform destination;
    public float grabDistance = 1f;
    public float distance;
    bool isHolding = false;

    // Update is called once per frame
    void Update()
    { 
        Vector3 playerPos = destination.transform.position;
        distance = Vector3.Distance(GetComponent<Collider>().ClosestPoint(playerPos), playerPos);
        if (distance > grabDistance) {
            OnMouseUp();
        }

        if (isHolding) {
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = destination.position;
            this.transform.parent = destination;
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        // Vector3 playerPos = destination.transform.position;
        // distance = Vector3.Distance(GetComponent<Collider>().ClosestPoint(playerPos), playerPos);
        // if (distance > grabDistance) {
        //     OnMouseUp();
        // }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name != "Player")
            OnMouseUp();
    }

    void OnMouseDown() 
    {
        if (distance <= grabDistance) {
            isHolding = true;
        }
        // if (distance <= grabDistance) {
        //     GetComponent<Rigidbody>().useGravity = false;
        //     this.transform.position = destination.position;
        //     // this.transform.parent = GameObject.Find("Item Destination").transform;
        //     this.transform.parent = destination;
        //     GetComponent<Rigidbody>().freezeRotation = true;
        // }
    }

    void OnMouseUp()
    {
        isHolding = false;
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;
        // GetComponent<Rigidbody>().freezeRotation = false;
        // GetComponent<Rigidbody>().useGravity = true;
        // this.transform.parent = null;
    }
}
