using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InternetCheck : MonoBehaviour
{
    TMPro.TMP_Text NoConnectionTxt;
    PickUpV2 pickUp;
    AimClick click;
    PlayerMovement movement;

    bool pickUpTurnOn = false;
    bool clickTurnOn = false;
    bool movementTurnOn = false;

    void Start()
    {
        NoConnectionTxt = GameObject.Find("NoConnectionTxt").GetComponent<TMPro.TMP_Text>();

        pickUp = GameObject.Find("Aim Set").GetComponent<PickUpV2>();
        click = GameObject.Find("Aim Set").GetComponent<AimClick>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        NoConnectionTxt.enabled = false;
    }

    void Update()
    {
       //Debug.Log("pickUpTurnOn " + pickUpTurnOn);
       //Debug.Log("clickTurnOn " + clickTurnOn);
       //Debug.Log("movementTurnOn " + movementTurnOn);

        // There is no inrernet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
           //Debug.Log("NO-INTERNT");
           //Debug.Log("pickUpTurnOn " + pickUpTurnOn);
           //Debug.Log("clickTurnOn " + clickTurnOn);
           //Debug.Log("movementTurnOn " + movementTurnOn);
            NoConnectionTxt.enabled = true;
            // Didn't changed and was turn off
            if (pickUpTurnOn == true)
            {
                pickUpTurnOn = false;
            }
            if (clickTurnOn == true)
            {
                clickTurnOn = false;
            }
            if (movementTurnOn == true)
            {
                movementTurnOn = false;
            }
        }
        else
        {
            NoConnectionTxt.enabled = false;
            if (pickUp.enabled == true)
            {
                pickUpTurnOn = true;
            }
            if (click.enabled == true)
            {
                clickTurnOn = true;
            }
            if (movement.enabled == true)
            {
                movementTurnOn = true;
            }
        }
    }
}
