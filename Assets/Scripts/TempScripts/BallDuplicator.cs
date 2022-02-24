using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDuplicator : MonoBehaviour
{
	private List<GameObject> clones;

    // Start is called before the first frame update
    void Start()
    {
        clones = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    	foreach (GameObject clone in clones) {
    		Destroy(clone, 1);
    	}
    }

    void OnCollisionEnter(Collision collision) {
    	GameObject clone = Instantiate(collision.gameObject, new Vector3(10, 10, 10), Quaternion.identity);
    	clones.Add(clone);
    }
}
