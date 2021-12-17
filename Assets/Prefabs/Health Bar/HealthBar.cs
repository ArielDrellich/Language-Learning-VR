﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthCounter
{
    private static int defaultHealth = 3;
    private static int remainingHealth = defaultHealth;

    public static void Increment(int amount = 1) 
    {
        remainingHealth += amount;
    }

    public static void Decrement(int amount = 1) 
    {
        remainingHealth -= amount;
    }

    public static int GetHealth() 
    {
        return remainingHealth;
    }

    public static void ResetHealth() {
        remainingHealth = defaultHealth;
    }
}

public class HealthBar : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        int remainingHealth = HealthCounter.GetHealth();
        GetComponent<TMPro.TMP_Text>().text = "Health: " + remainingHealth.ToString();
        if (remainingHealth <= 0) {
            SceneManager.LoadScene("Game Over Screen");
        }
    }
}
