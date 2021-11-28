using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StartMenu;

public class ExitClick : MonoBehaviour, IStartMenu
{
    public void DoClick() {
        Application.Quit();
    }
}
