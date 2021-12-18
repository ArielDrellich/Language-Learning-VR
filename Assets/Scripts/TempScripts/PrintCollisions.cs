using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintCollisions : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding with: "+collision.gameObject);
    }
}
