using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTimer : MonoBehaviour, IAction
{
    public void DoAction()
    {
        GameTimer.PauseTimer();
    }
}
