using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float distance;
    // Update is called once per frame
    void Update()
    { 
        // should probably calculate to end outside distance of object
        distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        if (distance > grabDistance)
            OnMouseUp();
    
    }

    public float grabDistance = 4f;

    void OnMouseDown() 
    {
        if (distance <= grabDistance) {
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = destination.position;
            this.transform.parent = GameObject.Find("Item Destination").transform;
            GetComponent<Rigidbody>().freezeRotation = true;
        }

    }

    void OnMouseUp()
    {
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;
    }
}
