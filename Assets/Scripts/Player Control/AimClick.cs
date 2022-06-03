using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimClick : MonoBehaviour
{
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        /* DrawRay is useful for debugging only. */
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*15, Color.green);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) 
        {
            IClickable clickable = hit.collider.GetComponent<IClickable>();

            if (clickable != null) 
            {
                clickable.LookedAt(hit);
            }
        }
    }
    
}
