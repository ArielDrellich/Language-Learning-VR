using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecWord : MonoBehaviour
{
	RaycastHit hit;
	[SerializeField] private float charDistance = 10f;
    ReticleManager reticle;

    public GameObject submit;
    [SerializeField] private float submitDistance = 10f;
    private WordScramble ws;

    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        ws = GameObject.Find("Core").GetComponent<WordScramble>();
    }
    // Update is called once per frame
    void Update()
    {
        bool didHitChar = Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward), out hit, charDistance);

        bool isChar = false;
        if (didHitChar) {
            isChar = hit.collider.GetComponent<CharObject>();
            GameObject selected = hit.collider.gameObject;
            CharObject co = (CharObject)selected.GetComponent(typeof(CharObject));
            if (co != null) 
                reticle.SetColor(Color.red);
            if (co != null && Input.GetButtonDown("Fire1")) {
            	co.Select();
            }
    	}

        Submit();
    }

    public void Submit()
    {
        bool didHitButton = Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward), out hit, submitDistance);

        if (didHitButton) {
            if (hit.collider.gameObject.name == submit.name) {
                reticle.SetColor(Color.red);
                if (Input.GetButtonDown("Fire1")) {
                    Debug.Log("Checking answer...");
                    if (ws.CheckWord()) {
                        Debug.Log("Success");
                    } else {
                        Debug.Log("You suck");
                    }
                }
            }
        }
    }
}
