using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAction : MonoBehaviour, IAction
{
    private static bool alreadyDone = false;
    public void DoAction()
    {
        Debug.Log("Doing tempaction");
        if (!alreadyDone) {
            alreadyDone = true;
            HealthManager.Increment(10 - HealthManager.GetHealth());
        }
    }
}
