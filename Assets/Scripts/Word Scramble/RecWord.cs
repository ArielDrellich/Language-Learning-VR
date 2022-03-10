using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecWord : MonoBehaviour, IClickable
{
	RaycastHit hit;
	[SerializeField] private float charDistance = 10f;
    ReticleManager reticle;

    [SerializeField] private float submitDistance = 10f;


    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
    }

    public void LookedAt(RaycastHit hit) 
    {    
        if (hit.distance <= charDistance ) {
            reticle.SetColor(Color.red);
            if (Input.GetButtonDown("Fire1")) {
                CharObject co = (CharObject)hit.collider.gameObject.GetComponent(typeof(CharObject));
            	co.Select();
            }
    	}

    }



}
