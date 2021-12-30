using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Menu;
public class StartClick : MonoBehaviour, IMenu
{
    public void DoClick(GameObject clicker) {
        // Calls the next scene in build
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
