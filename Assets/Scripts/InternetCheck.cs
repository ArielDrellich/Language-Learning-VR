﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InternetCheck : MonoBehaviour
{
    TMPro.TMP_Text NoConnectionTxt;
    PickUpV2 pickUp;
    AimClick click;
    PlayerMovement movement;
    bool pickUpStatus;
    bool clickStatus;
    bool movementStatus;


    void Start()
    {
        NoConnectionTxt = GameObject.Find("NoConnectionTxt").GetComponent<TMPro.TMP_Text>();

        pickUp = GameObject.Find("Aim Set").GetComponent<PickUpV2>();
        click = GameObject.Find("Aim Set").GetComponent<AimClick>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        NoConnectionTxt.enabled = false;

        pickUpStatus = pickUp.enabled;
        clickStatus = click.enabled;
        movementStatus = movement.enabled;
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
