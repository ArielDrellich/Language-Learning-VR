using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempScriptLoadLevel : MonoBehaviour
{
    public TMPro.TMP_Text timerTextPro;
    AsyncOperation loadingOperation;

    float loadingTime;

    // [SerializeField] Slider progressBar;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Ball") {
            GameObject.Find("Loading_Sprite").GetComponent<SpriteRenderer>().enabled = true;
            //stop the timer

            loadingTime = Time.time;
            loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            GameObject player = GameObject.Find("Player");
            GameObject aimSet = GameObject.Find("Aim Set");
            player.GetComponent<PlayerMovement>().enabled = false;
            aimSet.GetComponent<PickUp>().enabled = false;
            aimSet.GetComponent<Teleport>().enabled = false;
        }
    }

     // setting a function of our choice (in this case, 'OnSceneLoaded') to listen to the SceneManager for a level change
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        // start from when you stopped
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        loadingTime = Time.time - loadingTime;
        GameObject.Find("Timer").GetComponent<GameTimer>().accumulatedLoadingTime += loadingTime;
        //Debug.Log(mode);
    }

    // called when the game is terminated
    // void OnDisable()
    // {
    //     Debug.Log("OnDisable");
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // void Update()
    // {
    //     float progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    //     // percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
    //     print(Mathf.Round(progressValue * 100) + "%");
    // }
}
