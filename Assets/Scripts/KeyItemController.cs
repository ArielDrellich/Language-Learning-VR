using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeySystem
{

	public class KeyItemController : MonoBehaviour
	{
	    [SerializeField] private bool redDoor = false;
	    [SerializeField] private bool redKey = false;

	    [SerializeField] private KeyInventory _keyInventory = null;

	    private KeyDoorController doorObject;

	    private void Start()
	    {
	    	if (redDoor) 
	    	{
	    	Debug.Log(redDoor);
	    	doorObject = GetComponent<KeyDoorController>();
	    	}
	    }
	    public void ObjectInteraction() 
	    {
	    	if (redDoor)
	    	{
	    		doorObject.PlayAnimation();
	    	}
	    	else if (redKey) {
	    		Debug.Log("HERE");
	    		_keyInventory.hasRedKey = true;
	    		Debug.Log(_keyInventory.hasRedKey);
	    		//gameObject.setActive(false);
	    		gameObject.SetActive(false);
	    	}
	    }
	}
}