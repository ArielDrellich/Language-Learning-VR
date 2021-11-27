using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
	public float startTime;
	public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    	float t = Time.time - startTime;
    	string minutes = ((int) t / 60).ToString();
    	string seconds = (t % 60).ToString("f2");
    	timerText.text = minutes + ":" + seconds;
    	// if (timeValue > 0) {
     //    	timeValue -= Time.deltaTime;
    	// } else
    	// {
    	// 	timeValue = 0;
    	// }
    	// DisplayTime(timeValue);
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
