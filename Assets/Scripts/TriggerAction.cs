using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    public  List<Component> actions;
    public  string colliderName;
    public  bool   doOnce;
    private bool   done = false;
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == colliderName)
        {
            if (doOnce == true)
            {
                if (!done)
                    done = true;
                else
                    return;       
            }

            foreach (Component action in actions)
            {
                if ( action is IAction)
                {
                    ((IAction)action).DoAction();
                }
            }
        }
    }
}
