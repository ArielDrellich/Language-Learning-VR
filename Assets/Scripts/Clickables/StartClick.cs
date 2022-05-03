using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClick : MonoBehaviour, IClickable
{
    ReticleManager reticle;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
    }

    public void LookedAt(RaycastHit hit)
    {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1"))
        {
            if (PlayerPrefs.GetString("languageChoice").Equals(""))
            {
                //flash red square
                Debug.Log("No language selected"); //temporary
                return;
            }

            if (PlayerPrefs.GetString("Difficulty").Equals(""))
            {
                //flash red square
                Debug.Log("No difficulty selected"); //temporary
                return;
            }

            FindObjectOfType<LevelManager>().NextLevel();
        }
    }
    
}
