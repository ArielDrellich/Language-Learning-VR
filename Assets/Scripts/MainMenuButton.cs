﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour, IClickable
{
    ReticleManager reticle;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
    }

    public void LookedAt(RaycastHit hit) {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1")) {
            // Resets health bar for next game
            HealthCounter.ResetHealth();
            // Calls the first scene in build
            SceneManager.LoadScene(0);
        }
    }
}
