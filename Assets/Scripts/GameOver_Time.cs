using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Time : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TMPro.TMP_Text>().text = GameTimer.GetPlaytimeString();
    }

}
