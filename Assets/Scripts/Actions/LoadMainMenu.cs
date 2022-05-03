using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainMenu : MonoBehaviour, IAction
{

    public void DoAction()
    {
        FindObjectOfType<LevelManager>().MainMenu();
    }

}
