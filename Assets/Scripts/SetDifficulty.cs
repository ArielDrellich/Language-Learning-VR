using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficulty : MonoBehaviour, IClickable
{
    ReticleManager reticle;
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookedAt(RaycastHit hit)
    {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1"))
        {
            string difficultyName = hit.transform.GetChild(0).name; // LanguageXtext
            TMPro.TMP_Text difficultyTxt = GameObject.Find(difficultyName).GetComponent<TMPro.TMP_Text>();
            PlayerPrefs.SetString("difficultyChoice", difficultyTxt.text);
            switch (difficultyTxt.text)
            {
                case "easy":
                    Debug.Log("easy");
                break;
                case "hard":
                    Debug.Log("hard");
                break;
                case "medium":
                    Debug.Log("medium");
                break;
                default:
                    Debug.Log("default");
                    break;
            }
        }
    }
}


