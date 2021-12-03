using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StartMenu;
public class StartClick : MonoBehaviour, IStartMenu
{
    public void DoClick() {
        // Calls the next scene in build
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
