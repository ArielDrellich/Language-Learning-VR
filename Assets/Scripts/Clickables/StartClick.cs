using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClick : MonoBehaviour, IClickable
{
    ReticleManager reticle;
    GameObject     chooseLanguage;
    GameObject     chooseDifficulty;
    bool           canStart = true;

    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        chooseLanguage = GameObject.Find("LanguageAlarm");
        chooseDifficulty = GameObject.Find("DifficultyAlarm");
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
                StartCoroutine(ChooseLangAlarmCoroutine());
                canStart = false;
            }

            if (PlayerPrefs.GetString("Difficulty").Equals(""))
            {
                StartCoroutine(ChooseDifAlarmCoroutine());
                canStart = false;
            }

            if (canStart)
            {
                FindObjectOfType<LevelManager>().NextLevel();
            }

            canStart = true;
        }
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
