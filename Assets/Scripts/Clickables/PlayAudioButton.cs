using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioButton : MonoBehaviour, IClickable
{
    private ReticleManager reticle;
    private Animator       animator;
    private Translator     translator;
    private float          maxClickDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
        animator = this.GetComponent<Animator>();
    }

    public void SetTranslator(Translator translator)
    {
        this.translator = translator;
    }

    public void LookedAt(RaycastHit hit) {
        if (hit.distance <= maxClickDistance) {
            reticle.SetColor(Color.red);
            if (Input.GetButtonDown("Fire1"))
            {
                animator.Play("button press");
                translator.GetAudio();
            }
        }
    }
}
