using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHealth : MonoBehaviour, IAction
{
    public int newHealthAmount;
    public void DoAction() {
        HealthManager.Increment(newHealthAmount - HealthManager.GetHealth());
    }
}
