using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClick : MonoBehaviour, IClickable
{
    ReticleManager reticle;
    GameObject chooseLanguage;
    GameObject chooseDifficulty;
    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        chooseLanguage = GameObject.Find("LanguageAlarm");
        chooseDifficulty = GameObject.Find("DifficultyAlarm");
        //chooseLanguage.enabled = false;
        chooseLanguage.SetActive(false);
        chooseDifficulty.SetActive(false);
    }

    public void LookedAt(RaycastHit hit)
    {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1"))
        {
            if (PlayerPrefs.GetString("languageChoice").Equals(""))
            {
                chooseLanguage.SetActive(true);
                return;
            }

            if (PlayerPrefs.GetString("Difficulty").Equals(""))
            {
                chooseDifficulty.SetActive(true);
                return;
            }

            FindObjectOfType<LevelManager>().NextLevel();
        }
    }
    void Update()
    {
        if (!PlayerPrefs.GetString("languageChoice").Equals(""))
        {
            chooseLanguage.SetActive(false);
        }
        if (!PlayerPrefs.GetString("Difficulty").Equals(""))
        {
            chooseDifficulty.SetActive(false);
        }
    }
    
}
