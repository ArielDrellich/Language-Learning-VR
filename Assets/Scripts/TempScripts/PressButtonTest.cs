using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonTest : MonoBehaviour, IClickable
{
    ReticleManager reticle;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        animator = this.GetComponent<Animator>();
    }

    public void LookedAt(RaycastHit hit) {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1")) {
            animator.Play("button press");
        }
    }
}
