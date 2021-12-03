using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace KeySystem
{
	public class KeyRaycast : MonoBehaviour
	{
	   [SerializeField] private int rayLength = 5;
	   [SerializeField] private LayerMask layerMaskInteract;
	   [SerializeField] private string excluseLayerName = null;

	   private KeyItemController raycastedObject;
	   [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
	   [SerializeField] private GameObject crosshair = null;
	   private bool isCrosshairActive;
	   private bool doOnce;

	   private string interactableTag = "InteractiveObject";

	   private void Update()
	   {
		   	RaycastHit hit;
		   	Vector3 fwd = transform.TransformDirection(Vector3.forward);

		   	int mask = 1 << LayerMask.NameToLayer(excluseLayerName) | layerMaskInteract.value;

		   	if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask)) {
		   		if (hit.collider.CompareTag(interactableTag))
		   		{
		   			if (!doOnce)
		   			{
		   				raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
		   				CrosshairChange(true);
		   			}

		   			isCrosshairActive = true;
		   			doOnce = true;

		   			if (raycastedObject && Input.GetKeyDown(openDoorKey)) 
		   			{
		   				raycastedObject.ObjectInteraction();
		   			}
		   		}
	  	 	}
	  	 	else
	  	 	{
	  	 		if (isCrosshairActive)
	  	 		{
	  	 			CrosshairChange(false);
	  	 			doOnce = false;

	  	 		}
	  	 	}

	   }
	   void CrosshairChange(bool on)
	   {
		   	// if (on && !doOnce)
		   	// {
		   	// 	crosshair.color = Color.red;
		   	// }
		   	// else
		   	// {
		   	// 	crosshair.color = Color.white;
		   	// 	isCrosshairActive = false;
 		   // 	}

	   }

	}
}