using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchObject : MonoBehaviour
{
    [Header("Either drag an object or just write it's name")]
    public  GameObject expectedObject;
    public  string     expectedName;
    public  Component  action;
    private IAction    _action;
    private GameObject lastCollision;
    private bool       solved = false;

    void OnValidate()
    {
        if (expectedObject != null) {
            expectedName = expectedObject.name;
        }
    }

    // Used for dragging script in the Inspector. If we don't need that in the end, we can remove
    //this and action, and use only _action.
    void Start()
    {
        if (action) {
            Component[] list = action.GetComponents(typeof(Component));
            foreach (Component component in list) {
                if (component is IAction) {
                    _action = (IAction) component;
                    return;
                }
            }
        }
        
        // if action is either null or not IAction
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
