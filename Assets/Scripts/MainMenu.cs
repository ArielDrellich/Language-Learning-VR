using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IMenu
{
public void DoClick() {
        // Resets health bar for next game
        HealthCounter.ResetHealth();
        // Calls the first scene in build
        SceneManager.LoadScene(0);
    }
}
