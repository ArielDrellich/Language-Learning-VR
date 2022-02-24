using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    SpriteRenderer reticle;
    Color          currentColor;

    void Start() {
        reticle =  this.GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color) {
        currentColor = color;
    }


    void Update() {
        reticle.color = currentColor;
        currentColor = Color.white;
    }
}
