﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchObject : MonoBehaviour
{
    [Header("Drag an object or write it's name")]
    [SerializeField]
    private GameObject expectedObject;

    [SerializeField]
    private string     expectedName;

    [SerializeField]
    private Component  action;
    private IAction    _action;
    private Translator translator;
    private string     translatedName;
    private string     chosenLanguage;
    private bool       solved = false;
    private List<GameObject> previousCollisions = new List<GameObject>();

    // Used for dragging script in the Inspector. If we don't need that in the end, we can remove
    //this and action, and use only _action.
    void OnValidate()
    {
        if (expectedObject != null) {
            expectedName = expectedObject.name;
        }
    }

    void Start()
    {
        // Add this puzzles to the puzzle counter
        PuzzleManager.AddPuzzle();

        // Used for dragging script in the Inspector. If we don't need that in the end, we can remove
        //this and action, and use only _action.
        if (action is IAction)
            _action = (IAction) action;
        else
        // if action is either null or not IAction
            _action = new DefaultAction();

        translator = gameObject.AddComponent<Translator>();

        chosenLanguage = PlayerPrefs.GetString("languageChoice");
        // for debugging
        if (chosenLanguage == "")
            chosenLanguage = "en";

        previousCollisions.Add(GameObject.Find("Player")); 

        SetObject(expectedName);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!solved) {
    	    if (collider.gameObject.name != expectedName) {
                // if it hasn't been collided with that object in the past
                if (!previousCollisions.Contains(collider.gameObject)) {
                    HealthManager.Decrement();
                    previousCollisions.Add(collider.gameObject);
                    // for debugging
                    print("collided with: "+collider.gameObject.name);
                }
            } else {
                // what to do if it's correct
                PuzzleManager.Increment();
                solved = true;
                _action.DoAction();
            }
        }
    }

    // Can be called from outside in order to set action at runtime
    public void SetObject(string name)
    {
        expectedName = name;
        translatedName = translator.Translate(expectedName, "en", chosenLanguage);
        translator.TextToSpeech(translatedName, chosenLanguage, "UTF-8");

        // don't try to save components in a local variable. It doesn't work
        this.GetComponentInChildren<PlayAudioButton>().SetTranslator(translator);      
        this.GetComponentInChildren<TMPro.TMP_Text>().text = translatedName; 
    }

    public void SetAction(IAction newAction)
    {
        _action = newAction;
    }

}
