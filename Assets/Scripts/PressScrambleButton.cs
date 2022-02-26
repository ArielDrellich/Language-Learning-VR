using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressScrambleButton : MonoBehaviour, IClickable
{
    // drag & drop

	private WordScramble ws;
	public GameObject submit;
	public GameObject speaker;
	ReticleManager reticle;
	Animator animator;
	float maxTeleportDistance = 15f;
    // Start is called before the first frame update
    void Start()
    {
    	reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        ws = GameObject.Find("Core").GetComponent<WordScramble>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookedAt(RaycastHit hit)
    {

    	reticle.SetColor(Color.red);
    	if (hit.distance <= maxTeleportDistance) {
	        if (Input.GetButtonDown("Fire1")) {
	            if (hit.collider.gameObject.name == submit.name) {
	      			 //Call SetColor using the shader property name "_Color" and setting the color to red
	                Debug.Log("Checking answer...");
	        		animator.Play("button press");

	                if (ws.CheckWord()) {
	                    Debug.Log("Success");
	                } else {
	                    Debug.Log("You suck");
	                }
	            }
	            else if (hit.collider.name == speaker.name)
	            {
	                ws.tr.GetAudio();
	            }
	        }
	    }
    }
}
