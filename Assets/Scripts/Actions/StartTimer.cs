using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour, IAction
{
    public void DoAction()
    {
        GameTimer.StartTimer();
    }
}
