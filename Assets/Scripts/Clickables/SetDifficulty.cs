using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficulty : ScrollSelector, IClickable
{
    ReticleManager reticle;
    bool clicked = false;
    private GameObject hardButton;
    private GameObject mediumButton;
    private GameObject easyButton;
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        hardButton = GameObject.Find("Hard");
        mediumButton = GameObject.Find("Medium");
        easyButton = GameObject.Find("Easy");
        resetColors();
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
            string difficultyName = hit.transform.name;
            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.grey;
            setDif(difficultyName);
            ToggleClick(hit);
        }
    }


    void setDif(string difficultyChoice)
    {
        switch (difficultyChoice)
        {
            case "Easy":
                PlayerPrefs.SetString("Difficulty", "easy");
                break;
            case "Medium":
                PlayerPrefs.SetString("Difficulty", "medium");
                break;
            case "Hard":
                PlayerPrefs.SetString("Difficulty", "hard");
                break;
            default:
                break;
        }
    }


    public override void resetColors()
    {
        easyButton.GetComponent<Renderer>().material.color = Color.white;
        mediumButton.GetComponent<Renderer>().material.color = Color.white;
        hardButton.GetComponent<Renderer>().material.color = Color.white;
    }

}


