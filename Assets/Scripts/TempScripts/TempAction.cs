using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAction : MonoBehaviour, IAction
{
    public void DoAction()
    {
        Debug.Log("Doing tempaction");
        HealthCounter.Increment();
    }
}
