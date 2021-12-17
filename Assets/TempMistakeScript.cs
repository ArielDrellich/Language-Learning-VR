using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMistakeScript : MonoBehaviour
{
      void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bad Table") {
            HealthCounter.Decrement();
        }
    }
}
