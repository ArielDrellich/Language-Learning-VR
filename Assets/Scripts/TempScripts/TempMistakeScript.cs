using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMistakeScript : MonoBehaviour
{
    private bool recentMistake = false;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bad Table" && recentMistake == false) {
            recentMistake = true;
            HealthCounter.Decrement();
        }
            StartCoroutine(MistakeDelay());
    }


    IEnumerator MistakeDelay()
    {
        yield return new WaitForSeconds(1.0f);
        recentMistake = false;
    }
}
