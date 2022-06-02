using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InternetCheck : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text NoConnectionTxt;
    PickUpV2 pickUp;
    AimClick click;
    PlayerMovement movement;
    bool pickUpStatus;
    bool clickStatus;
    bool movementStatus;


    void Start()
    {
        pickUp = GameObject.Find("Aim Set").GetComponent<PickUpV2>();
        click = GameObject.Find("Aim Set").GetComponent<AimClick>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        NoConnectionTxt.outlineWidth = 0.3f;
        NoConnectionTxt.enabled = false;

        pickUpStatus = pickUp.enabled;
        clickStatus = click.enabled;
        movementStatus = movement.enabled;
    }

    void Update()
    {
        // There is no internet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
           NoConnectionTxt.enabled = true;
            // Didn't changed and was turn off
            pickUp.enabled = false;
            click.enabled = false;
            movement.enabled = false;
        }
        else
        {
            NoConnectionTxt.enabled = false;
            pickUp.enabled = pickUpStatus;
            click.enabled = clickStatus;
            movement.enabled = movementStatus;
        }
    }
}
