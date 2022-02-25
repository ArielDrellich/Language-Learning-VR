using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartClick : MonoBehaviour, IClickable
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
            // Calls the next scene in build
            if (!PlayerPrefs.GetString("languageChoice").Equals("")) {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //LevelManager.NextLevel();
                SceneManager.LoadScene("Playground");
            }

        }
    }
    public void DoClick(GameObject clicker) {
        // Calls the next scene in build
        if (!PlayerPrefs.GetString("languageChoice").Equals("")) {
        	//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("Playground");

        }
    }
}
