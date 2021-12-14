using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecWord : MonoBehaviour
{
	RaycastHit hit;
	[SerializeField] private float charDistance = 10f;

    // Update is called once per frame
    void Update()
    {
        bool didHitChar = Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward), out hit, charDistance);

        bool isChar = false;
        if (didHitChar) {
            isChar = hit.collider.GetComponent<CharObject>();
            GameObject selected = hit.collider.gameObject;
            CharObject co = (CharObject)selected.GetComponent(typeof(CharObject));

            if (co != null && Input.GetButtonDown("Fire1")) {
            	co.Select();
            }
    	}
    }
}
