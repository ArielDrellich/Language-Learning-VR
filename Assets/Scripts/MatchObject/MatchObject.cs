using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchObject : MonoBehaviour
{

    [Header("Drag an object or write it's name")]
    [SerializeField]
    private GameObject expectedObject;

    [SerializeField]
    private string expectedName;
    
    [SerializeField]
    PlayAudioButton audioButton;

    [SerializeField]
    TMPro.TMP_Text shownText;

    private Translator translator;
    private string     translatedName;
    private string     chosenLanguage;
    private bool       solved = false;
    public  List<GameObject> ignoreCollisions = new List<GameObject>();
    public  List<Component>  successActions;
    public  List<Component>  failActions;

    // Used for dragging script in the Inspector.
    void OnValidate()
    {
        if (expectedObject != null)
        {
            expectedName = expectedObject.name;
        }
    }

    void Start()
    {
        // Add this puzzles to the puzzle counter
        PuzzleManager.AddPuzzle();

        translator = gameObject.AddComponent<Translator>();

        chosenLanguage = PlayerPrefs.GetString("languageChoice");
        
        // for debugging
        if (chosenLanguage == "")
            chosenLanguage = "en";

        ignoreCollisions.Add(GameObject.Find("Player")); 

        shownText.outlineWidth = 0.2f;

        SetObject(expectedName);
    }

    private void Fail(GameObject gameObject)
    {
        // if it hasn't collided with that object in the past
        if (!ignoreCollisions.Contains(gameObject))
        {
            ignoreCollisions.Add(gameObject);

            HealthManager.Decrement();

            // for debugging
            // print("collided with: " + collider.gameObject.name);

            // Do all FailActions
            foreach (Component action in failActions)
            {
                if (action is IAction) {
                    ((IAction)action).DoAction();
                }
            }
        }
    }

    private void Succeed()
    {
        PuzzleManager.Increment();

        // Do all SuccessActions
        foreach (Component action in successActions)
        {
            if (action is IAction)
            {
                ((IAction)action).DoAction();
            }
        }

        transform.parent.Find("Billboard/Finger Point").gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!solved)
        {
            bool canPickUpCollider = collider.GetComponent<CanPickUp>();
            bool canPickUpParent = false;

            // if there's no CanPickUp on the gameobject, check for one on it's parent
            if (!canPickUpCollider)
            {
                if ((collider.transform.parent != null && 
                    collider.transform.parent.GetComponent<CanPickUp>()))
                    {
                        canPickUpParent = true;
                    }
            }
            
            if (canPickUpCollider || canPickUpParent)
            {
                if ((!canPickUpCollider || collider.transform.name        != expectedName) &&
                    (!canPickUpParent   || collider.transform.parent.name != expectedName))
                {
                    Fail(collider.gameObject);
                } 
                else
                {
                    solved = true;
                    Succeed();
                }
            }
        }

    }

    // Can be called from outside in order to set action at runtime
    public void SetObject(string name)
    {
        expectedName = name;
        translatedName = translator.Translate(expectedName, "en", chosenLanguage);
        translator.TextToSpeech(translatedName, chosenLanguage, "UTF-8");

        this.audioButton.SetTranslator(translator);      

        if (PlayerPrefs.GetInt("isRTL") == 1)
        {
            this.shownText.isRightToLeftText = true;
        }

        this.shownText.text = translatedName; 
    }

}
