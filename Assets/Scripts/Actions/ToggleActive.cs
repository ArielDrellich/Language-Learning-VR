using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour, IAction
{
    public GameObject game_object;
    public bool       active;

    public void DoAction()
    {
        game_object.SetActive(active);
    }
}
