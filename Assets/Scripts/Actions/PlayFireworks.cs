using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFireworks : MonoBehaviour, IAction
{
    [SerializeField] ParticleSystem fireworks;
    
    public void DoAction()
    {
        fireworks.Play();
    }
}
