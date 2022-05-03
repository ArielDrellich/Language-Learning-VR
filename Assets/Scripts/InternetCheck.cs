using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InternetCheck : MonoBehaviour
{
    TMPro.TMP_Text NoConnectionTxt;
    PickUpV2 PickUp;
    AimClick click;
    PlayerMovement movement;
    void Start()
    {
        NoConnectionTxt = GameObject.Find("NoConnectionTxt").GetComponent<TMPro.TMP_Text>();
        NoConnectionTxt.enabled = false;
        PickUp = GameObject.Find("Aim Set").GetComponent<PickUpV2>();
        PickUp.enabled = true;
        click = GameObject.Find("Aim Set").GetComponent<AimClick>();
        click.enabled = true;
        // if we are in the start scene, the player movement already enabled
        Scene scene = SceneManager.GetActiveScene();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if (scene.name != "Start Menu" && scene.name != "Game Over Screen" && scene.name != "Win Screen")
        {
            movement.enabled = true;
        }
        Debug.Log(movement.enabled);


    }

    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NoConnectionTxt.enabled = true;
            PickUp.enabled = false;
            click.enabled = false;
            movement.enabled = false;
        }
    }
}
