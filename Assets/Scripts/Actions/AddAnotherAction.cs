using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnotherAction : MonoBehaviour, IAction
{
    public  Component  action_1;
    private IAction    _action_1;
    public  Component  action_2;
    private IAction    _action_2;
    void Start()
    {
        if (action_1 is IAction)
            _action_1 = (IAction) action_1;
        else
        // if action_1 is either null or not IAction
            _action_1 = new DefaultAction();

        if (action_2 is IAction)
            _action_2 = (IAction) action_2;
        else
        // if action_2 is either null or not IAction
            _action_2 = new DefaultAction();
    }

    public void DoAction() {
        _action_1.DoAction();
        _action_2.DoAction();
    }
}
