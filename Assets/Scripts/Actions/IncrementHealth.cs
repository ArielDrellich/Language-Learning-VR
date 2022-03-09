using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementHealth : MonoBehaviour, IAction
{
    // just temporary. will probably switch to ++ instead of 10
    public void DoAction() {
        HealthManager.Increment(10 - HealthManager.GetHealth());
    }
}
