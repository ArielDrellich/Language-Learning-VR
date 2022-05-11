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
        if (expectedObject != null) {
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

        SetObject(expectedName);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<CanPickUp>() || 
            (collider.transform.parent != null && collider.transform.parent.GetComponent<CanPickUp>()))
        {
            if (!solved) {
                if (collider.gameObject.name != expectedName &&
                    collider.gameObject.name != expectedName + "Model") {

                    // if it hasn't been collided with that object in the past
                    if (!ignoreCollisions.Contains(collider.gameObject)) {
                        HealthManager.Decrement();
                        ignoreCollisions.Add(collider.gameObject);

                        // for debugging
                        // print("collided with: " + collider.gameObject.name);

                        // Do all FailActions
                        foreach (Component action in failActions)
                            if (action is IAction) {
                                ((IAction)action).DoAction();
                            }
                    }
                } else {

                    // what to do if it's correct
                    PuzzleManager.Increment();
                    solved = true;

                    // Do all SuccessActions
                    foreach (Component action in successActions)
                        if (action is IAction) {
                            ((IAction)action).DoAction();
                        }

                    transform.parent.Find("Billboard/Finger Point").gameObject.SetActive(false);
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
