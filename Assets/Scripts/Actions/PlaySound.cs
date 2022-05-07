using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Music for tutorial: Royalty Free Music from https://timtaj.com

public class PlaySound : MonoBehaviour, IAction {

    public AudioSource audioSource = null;

    public void DoAction()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
