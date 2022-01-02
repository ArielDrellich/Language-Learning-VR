using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour, IAction
{
    AsyncOperation loadingOperation;
    public void DoAction()
    {
        GameObject.Find("Loading_Sprite").GetComponent<SpriteRenderer>().enabled = true;
        loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject player = GameObject.Find("Player");
        GameObject aimSet = GameObject.Find("Aim Set");
        player.GetComponent<PlayerMovement>().enabled = false;
        aimSet.GetComponent<PickUp>().enabled = false;
        aimSet.GetComponent<Teleport>().enabled = false;
    }
}
