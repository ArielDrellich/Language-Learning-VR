using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempScriptLoadLevel : MonoBehaviour
{
    public TMPro.TMP_Text timerTextPro;
    public GameObject collisionTriggerObj;
    GameObject player;
    GameObject aimSet;
    GameObject loadingSprite;
    AsyncOperation loadingOperation;

    float loadingTime;
    bool loading;

    void Start()
    {
        loading = true;
        player = GameObject.Find("Player");
        aimSet = GameObject.Find("Aim Set");
        loadingSprite = GameObject.Find("Loading_Sprite");
    }

    // [SerializeField] Slider progressBar;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == collisionTriggerObj.name && loading) {
            loadingSprite.GetComponent<SpriteRenderer>().enabled = true;
            //stop the timer

            loadingTime = Time.time;
            loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            player.GetComponent<PlayerMovement>().enabled = false;
            aimSet.GetComponent<PickUp>().enabled = false;
            aimSet.GetComponent<Teleport>().enabled = false;
            loading = false;
        }
    }

     // setting a function of our choice (in this case, 'OnSceneLoaded') to listen to the SceneManager for a level change
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        loadingTime = Time.time - loadingTime;
        // GameObject.Find("Timer").GetComponent<GameTimer>().accumulatedLoadingTime += loadingTime;
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
