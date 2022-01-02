using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameTimer : MonoBehaviour
{
	private float startTime;
	public TMPro.TMP_Text timerTextPro;

    public float accumulatedLoadingTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("accumulatedLoadingTime: " + accumulatedLoadingTime);
    }

    void OnEnable()
    {
        if (SceneManager.GetActiveScene().name.Equals("Living-Room-Scene")) {
            // Living room scene is the first scene so it sets the start time
            startTime = Time.time;
            Debug.Log(startTime);
        } else {
            // Get the start time that was set by the living room scene so the timer will remain
            // updated to the correct start time.
            // Loading time will be taken into consideration later.
            startTime = PlayerPrefs.GetFloat("timerStartTime");
            Debug.Log(startTime);
        }

        Debug.Log("startTime: " + startTime);
    }

    // Update is called once per frame
    void Update()
    {
    	float t = Time.time - startTime - accumulatedLoadingTime;
    	string minutes = ((int) t / 60).ToString();
        float seconds_i = t % 60;
    	string seconds = seconds_i.ToString("f2");
        // string time_to_show;
    	timerTextPro.text = minutes + ":" + seconds;
        Debug.Log(timerTextPro.text);

        //Debug.Log(seconds);

        /*if (Mathf.FloorToInt(seconds_i) == 5) {
            //StaticClass.CrossSceneInfo.Set(time_to_show);
            SceneManager.LoadScene("StarShip-room");
        }*/


    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("timerStartTime", startTime);
        Debug.Log("Timer disabled");
    }


}
