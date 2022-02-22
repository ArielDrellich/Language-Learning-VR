using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;

public class ExitGameButton : MonoBehaviour, IClickable
{
    ReticleManager reticle;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
    }

    public void LookedAt(RaycastHit hit) {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1")) {
            Application.Quit();
        }
    }
}
