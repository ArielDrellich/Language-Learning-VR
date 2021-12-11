using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchObject : MonoBehaviour
{
	public string expectedTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
    	if (collision.gameObject.tag == expectedTag) {
    		Vector3 pos = collision.gameObject.transform.position + new Vector3(0, 5, 5);
    		Debug.Log(pos);
    		GameObject clone = Instantiate(collision.gameObject, pos, collision.gameObject.transform.rotation);

    		// Destroy(clone);

    		Debug.Log("Collision successfull");
    	} else {
    		Debug.Log("Collided with unexpected object " + collision.gameObject.tag);
    	}
    }
}
