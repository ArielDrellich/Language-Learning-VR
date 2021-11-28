using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameTimer : MonoBehaviour
{
	private float startTime;
	public TMPro.TMP_Text timerTextPro;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        if (SceneManager.GetActiveScene().name.Equals("Living-Room-Scene")) {
            startTime = Time.time;
        } else {
            startTime = PlayerPrefs.GetFloat("timerStartTime") - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    	float t = Time.time - startTime;
    	string minutes = ((int) t / 60).ToString();
        float seconds_i = t % 60;
    	string seconds = seconds_i.ToString("f2");
        // string time_to_show;
    	timerTextPro.text = minutes + ":" + seconds;

        //Debug.Log(seconds);

        if (Mathf.FloorToInt(seconds_i) == 10) {
            // StaticClass.CrossSceneInfo.Set(time_to_show);
            SceneManager.LoadScene("SampleScene");
        }
    	// if (timeValue > 0) {
     //    	timeValue -= Time.deltaTime;
    	// } else
    	// {
    	// 	timeValue = 0;
    	// }
    	// DisplayTime(timeValue);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("timerStartTime", startTime);
    }

    // void DisplayTime(float TimeToDisplay)
    // {
    // 	if (TimeToDisplay < 0)
    // 	{
    // 		TimeToDisplay = 0;
    // 	}

    // 	// calculate the minutes
    // 	float minutes = Mathf.FloorToInt(TimeToDisplay / 60);

    // 	// calculate the seconds
    // 	float seconds = Mathf.FloorToInt(TimeToDisplay % 60);

    // 	timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    // }
}
