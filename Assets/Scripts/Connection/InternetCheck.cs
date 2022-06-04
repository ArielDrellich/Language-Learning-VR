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
    bool statusChanged;


    void Start()
    {
        pickUp = GameObject.Find("Aim Set").GetComponent<PickUpV2>();
        click = GameObject.Find("Aim Set").GetComponent<AimClick>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        NoConnectionTxt.outlineWidth = 0.3f;
        NoConnectionTxt.enabled = false;

        statusChanged = false;
    }

    void Update()
    {
        // There is no internet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NoConnectionTxt.enabled = true;

            if (!statusChanged)
            {
                pickUpStatus = pickUp.enabled;
                clickStatus = click.enabled;
                movementStatus = movement.enabled;
                statusChanged = true;
            }

            // Didn't changed and was turn off
            pickUp.enabled = false;
            click.enabled = false;
            movement.enabled = false;

        }
        else
        {
            if (statusChanged) // return to old status
            {
                NoConnectionTxt.enabled = false;
                pickUp.enabled = pickUpStatus;
                click.enabled = clickStatus;
                movement.enabled = movementStatus;
                statusChanged = false;
            }
        }
    }
}
