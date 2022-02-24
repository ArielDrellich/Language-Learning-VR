using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleLevels : MonoBehaviour, IClickable
{
    ReticleManager reticle;
    bool shuffle = true;
    [SerializeField] SpriteRenderer dontShuffle;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<ReticleManager>();
    }

    public void LookedAt(RaycastHit hit) {
        reticle.SetColor(Color.red);
        if (Input.GetButtonDown("Fire1")) {
            LevelManager.ShuffleLevels(shuffle);
            shuffle = !shuffle;
            dontShuffle.enabled = shuffle;
        }
    }
}
