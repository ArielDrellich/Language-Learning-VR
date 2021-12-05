using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    Color currentColor;
    SpriteRenderer rend;

    void Start() {
        rend =  this.GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color) {
        currentColor = color;
    }


    void Update() {
        rend.color = currentColor;
        currentColor = Color.white;
    }
}
