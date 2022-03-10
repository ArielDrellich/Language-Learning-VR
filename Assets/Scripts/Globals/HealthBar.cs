using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager
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
    TMPro.TMP_Text tmp;
    void Start() 
    {
       tmp = GetComponent<TMPro.TMP_Text>(); 
    }
    
    // Update is called once per frame
    async void Update()
    {
        int remainingHealth = HealthManager.GetHealth();
        
        string hearts = "";
        for (int i = 0; i < remainingHealth; i++)
            hearts += '♥';
        tmp.text = hearts;

        if (remainingHealth <= 0) {
            LevelManager.GameOver();
        }
    }
}
