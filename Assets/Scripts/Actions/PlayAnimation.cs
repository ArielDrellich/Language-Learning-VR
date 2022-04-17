using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour, IAction
{
    public Animator anim;
    public string   state;

    [Range(0.0f, 10.0f)]
    public float animationSpeed = 1;
    
    public void DoAction()
    {
        anim.speed = animationSpeed;
        anim.Play(state, -1);
    }
}
