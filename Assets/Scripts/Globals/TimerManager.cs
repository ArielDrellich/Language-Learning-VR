using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static float   startTime      = -1;
    private static float   pauseStartTime = -1;
    private static bool    paused         = true;
    private static string  timeString;
    private TMPro.TMP_Text timerText;

    public static void StartTimer()
    {
        // if first time clicking start
        if (startTime == -1)
            startTime = Time.time;
        // if resuming, not clicking start a second time without pausing
        else if (pauseStartTime != -1)
            startTime += (Time.time - pauseStartTime);

        paused = false;
        pauseStartTime = -1;
    }

    public static void PauseTimer()
    {
        paused = true;
        pauseStartTime = Time.time;
    }

    public static void StopTimer()
    {
        paused = false;
        startTime = -1;
        pauseStartTime = -1;
    }

    public static string GetPlaytimeString() 
    {
        return timeString;
    }

    public static float GetPlaytime() 
    {
        float playtime = 0;

        if (paused)
        {
            playtime = pauseStartTime - startTime;
        }
        else
        {
            Debug.Log("TimerManager: Don't call GetPlaytime() when timer not paused.");
        }

        return playtime;
    }

    // Maybe as a penalty, or for debugging purposes
    public static void AddPlaytime(float toAdd)
    {
        startTime -= toAdd;
    }

    // To remove loading time, and possibly as a reward
    public static void SubtractPlaytime(float toSubtract)
    {
        startTime += toSubtract;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timerText = this.GetComponent<TMPro.TMP_Text>();
        timerText.text = "Not started";
        timerText.outlineWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused) {
            float time = Time.time - startTime;
            string minutes = ((int) time / 60).ToString("00");
            float fseconds = time % 60;
            string seconds = ((int) fseconds).ToString("00");
            timerText.text = timeString = minutes + ":" + seconds;
        }
    }
}