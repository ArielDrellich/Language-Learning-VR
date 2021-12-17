using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    private float gravity = 9.8f;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * speed;
        velocity = Camera.main.transform.TransformDirection(velocity);

        velocity.y =- gravity;
        controller.Move(velocity * Time.deltaTime);
    }
}
