using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour, IAction {

    public AudioSource audioSource = null;

    public void DoAction()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
