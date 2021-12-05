using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempScriptLoadLevel : MonoBehaviour
{
    AsyncOperation loadingOperation;
    // [SerializeField] Slider progressBar;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Square Table") {
            GameObject.Find("Loading_Sprite").GetComponent<SpriteRenderer>().enabled = true;
            loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            GameObject player = GameObject.Find("Player");
            GameObject aimSet = GameObject.Find("Aim Set");
            player.GetComponent<PlayerMovement>().enabled = false;
            aimSet.GetComponent<PickUp>().enabled = false;
            aimSet.GetComponent<Teleport>().enabled = false;
        }
    }

    // void Update()
    // {
    //     float progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    //     // percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
    //     print(Mathf.Round(progressValue * 100) + "%");
    // }
}
