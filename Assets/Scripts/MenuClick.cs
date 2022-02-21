using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;

public class MenuClick : MonoBehaviour
{
    SpriteRenderer reticle;
    RaycastHit hit;
    Collision currentCollision;
    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);

        if (didHit) {
            reticle.color = Color.red;
            if (Input.GetButtonDown("Fire1")) {
                hit.collider.GetComponent<IMenu>().DoClick(hit.collider.gameObject);
            }
        } else {
            reticle.color = Color.white;
        }
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     currentCollision = collision;
    // }
}
