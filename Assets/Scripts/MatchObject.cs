using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchObject : MonoBehaviour
{
    public  string     expectedName;
    public  Component  action;
    private IAction    _action;
    private GameObject lastCollision;
    private bool       solved = false;

    // Used for dragging script in the Inspector. If we don't need that in the end, we can remove
    //this and action, and use only _action.
    void Start()
    {
        Component[] list = action.GetComponents(typeof(Component));
        foreach (Component component in list) {
            if (component is IAction) {
                action = component;
                break;
            }
        }

        if (action is IAction)
            _action = (IAction) action;
        else 
            _action = new DefaultAction();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (!solved) {
    	    if (collision.gameObject.name != expectedName) {
                if (collision.gameObject != lastCollision && collision.gameObject.name != "Floor") {
                    HealthCounter.Decrement();
                    lastCollision = collision.gameObject;
                }
            } else {
                // what to do if it's correct
                _action.DoAction();
                solved = true;
            }
        }
    }

    // Can be called from outside in order to set action at runtime
    public void SetAction(IAction newAction)
    {
        _action = newAction;
    }

}
