using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;

public class ExitClick : MonoBehaviour, IMenu
{
    public void DoClick(GameObject clicker) {
        Application.Quit();
    }
}
