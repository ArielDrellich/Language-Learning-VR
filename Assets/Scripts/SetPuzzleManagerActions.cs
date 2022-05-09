using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPuzzleManagerActions : MonoBehaviour
{
    public List<Component> actions;
    
    // Start is called before the first frame update
    void Start()
    {
        List<IAction> _actions = new List<IAction>();

        foreach (Component action in actions)
        {
            if (action is IAction) {
                _actions.Add((IAction)action);
            }
        }

        PuzzleManager.SetActions(_actions);
    }

}
