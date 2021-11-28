using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartClick : MonoBehaviour
{
    // [SerializeField] Scene scene;
    public void LoadScene() {
        SceneManager.LoadScene("SampleScene");
    }
}
