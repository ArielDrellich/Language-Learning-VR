using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchObject : MonoBehaviour
{
    [Header("Drag an object or write it's name")]
    public  GameObject expectedObject;
    public string     expectedName;
    [SerializeField]
    private Component action;
    private IAction   _action;
    private List<GameObject> previousCollisions = new List<GameObject>();
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
        // Add this puzzles to the puzzle counter
        PuzzleManager.AddPuzzle();

        // gets IAction from inspector
        if (action is IAction)
            _action = (IAction) action;
        else
        // if action is either null or not IAction
            _action = new DefaultAction();

        previousCollisions.Add(GameObject.Find("Player")); 
        /// delete if/when we make MatchObject be on an invisible plane instead of the object itself///
        previousCollisions.Add(GameObject.Find("Floor")); 
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!solved) {
    	    if (collider.gameObject.name != expectedName) {
                // if it hasn't been collided with that object in the past
                if (!previousCollisions.Contains(collider.gameObject)) {
                    HealthManager.Decrement();
                    previousCollisions.Add(collider.gameObject);
                    // for debugging
                    // print("collided with: "+collider.gameObject.name);
                }
            } else {
                // what to do if it's correct
                PuzzleManager.Increment();
                solved = true;
                _action.DoAction();
            }
        }
    }

    // Can be called from outside in order to set action at runtime
    public void SetAction(IAction newAction)
    {
        _action = newAction;
    }

}
