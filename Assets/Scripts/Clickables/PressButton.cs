using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour, IClickable
{
    ReticleManager reticle;
    Animator       animator;
    
    [SerializeField]
    List<Component> actions;

    // Start is called before the first frame update
    void Start()
    {
        reticle  = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        animator = this.GetComponent<Animator>();
    }

    public void LookedAt(RaycastHit hit) {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1")) {
            animator.Play("button press");

            foreach (Component action in actions)
            {
                if (action is IAction)
                {
                    ((IAction)action).DoAction();
                }
            }
        }
    }
}
