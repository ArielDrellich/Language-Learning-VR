using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchObject : MonoBehaviour
{
    public string expectedName;
    public Component action;
    private IAction _action;
    private GameObject lastCollision;

    void OnCollisionEnter(Collision collision)
    {
    	if (collision.gameObject.name != expectedName) {
            if (collision.gameObject != lastCollision && collision.gameObject.name != "Floor") {
                HealthCounter.Decrement();
                lastCollision = collision.gameObject;
            }
        } else {
            // what to do if it's correct
            _action.DoAction();
        }
    }

    void Awake()
    {
        if (action is IAction)
            _action = (IAction)action;
        else 
            _action = new DefaultAction();
    }

    public void SetAction(IAction newAction)
    {
        _action = newAction;
    }

	// public string expectedTag;
    	// if (collision.gameObject.tag == expectedTag) {
    		// Vector3 pos = collision.gameObject.transform.position + new Vector3(0, 5, 5);
    		// Debug.Log(pos);
    		// GameObject clone = Instantiate(collision.gameObject, pos, collision.gameObject.transform.rotation);

    		// Destroy(clone);

    		// Debug.Log("Collision successfull");
    	// } else {
    		// Debug.Log("Collided with unexpected object " + collision.gameObject.tag);
    	// }
}
