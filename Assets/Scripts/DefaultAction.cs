using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAction : MonoBehaviour, IAction
{
    public void DoAction()
    {
        Debug.Log("Doing default action");
    }
}
