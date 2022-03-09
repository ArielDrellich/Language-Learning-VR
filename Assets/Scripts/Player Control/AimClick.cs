using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimClick : MonoBehaviour
{
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) {
            IClickable clickable = hit.collider.GetComponent<IClickable>();
            if (clickable != null) {
                clickable.LookedAt(hit);
            }
        }
    }
    
}
