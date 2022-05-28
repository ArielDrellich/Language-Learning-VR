using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClick : MonoBehaviour, IClickable
{
    ReticleManager reticle;
    GameObject     chooseLanguage;
    GameObject     chooseDifficulty;
    MeshRenderer   rend;
    bool           canStart         = false;
    bool           languagePicked   = false;
    bool           difficultyPicked = false;

    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        chooseLanguage = GameObject.Find("LanguageAlarm");
        chooseDifficulty = GameObject.Find("DifficultyAlarm");
        rend = GetComponent<MeshRenderer>();
        chooseLanguage.SetActive(false);
        chooseDifficulty.SetActive(false);
    }

    public void LookedAt(RaycastHit hit)
    {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1"))
        {
            if (canStart)
            {
                FindObjectOfType<LevelManager>().NextLevel();
            }
            else
            {
                if (!languagePicked)
                {
                    StartCoroutine(ChooseLangAlarmCoroutine());
                }

                if (!difficultyPicked)
                {
                    StartCoroutine(ChooseDifAlarmCoroutine());
                }
            }
        }
    }

    void Update()
    {
        languagePicked = PlayerPrefs.GetString("languageChoice").Equals("") ? false : true;

        difficultyPicked = PlayerPrefs.GetString("Difficulty").Equals("") ? false : true;


        if (languagePicked && difficultyPicked)
        {
            canStart = true;
            rend.material.color = new Color(1f,1f,1f,1f);
        }
        else
        {
            canStart = false;
            rend.material.color = new Color(1f,1f,1f,0.4f);
        }

        // if (canStart)
        // {
        //     rend.material.color = new Color(1f,1f,1f,1f);
        // }
        // else
        // {
        //     rend.material.color = new Color(1f,1f,1f,0.4f);
        // }
    }

    IEnumerator ChooseLangAlarmCoroutine()
    {
        chooseLanguage.SetActive(true);
        yield return new WaitForSeconds(1);
        chooseLanguage.SetActive(false);
    }

    IEnumerator ChooseDifAlarmCoroutine()
    {
        chooseDifficulty.SetActive(true);
        yield return new WaitForSeconds(1);
        chooseDifficulty.SetActive(false);
    }

}
