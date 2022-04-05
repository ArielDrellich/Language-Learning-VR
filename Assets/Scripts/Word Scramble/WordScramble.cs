using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
// using Translator;
[System.Serializable]

public class Word
{
    public string word;
    [Header("Leave empty if you want to randomized")]
    public string desireRandom;

    public Word(string w) {
    	word = w;
    	Debug.Log(word);
    }

    public string GetString()
    {
        // If you want it to be randomize
        if (!string.IsNullOrEmpty(desireRandom))
        {
            return desireRandom;
        }
        string result = word;

        while (result == word)
        {
            result = "";

            List<char> characters = new List<char>(word.ToCharArray());
            while (characters.Count > 0)
            {
                int indexChar = Random.Range(0, characters.Count);
                result += characters[indexChar];

                characters.RemoveAt(indexChar);
            }
        }

        return result;
    }
}

public class WordScramble : MonoBehaviour
{
    public Word[] words;

    [Header("UI REFERENCE")]
    public CharObject prefab;
    public Transform  container;
    CharObject        firstSelected;
    List<CharObject>  charObjects = new List<CharObject>();

    public WordScramble main;
    public int     currentWord;
    public float   space;
    private int    originalId;
    private bool   finished;
    private bool   incremented = false; 
    private string translatedWord;
    public Translator tr;

    [SerializeField]
    private Component win_action;
    private IAction   _action;

    void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalId = 0;
        finished = false;
        tr = gameObject.AddComponent<Translator>();
        ShowScramble(currentWord);

        // gets IAction from inspector
        if (win_action is IAction)
            _action = (IAction) win_action;
        else
        // if action is either null or not IAction
            _action = new DefaultAction();

        // Add this puzzles to the puzzle counter
        PuzzleManager.AddPuzzle();
    }

    // Relocate the letter
    void RepositionObject()
    {
        if (charObjects.Count == 0)
        {
            return;
        }
        float center = (charObjects.Count - 1) / 2;
        for (int i = 0; i < charObjects.Count; i++)
        {
            charObjects[i].rectTransform.anchoredPosition3D =
                new Vector3((i - center) * space, 0,0); // problem, can only face one way. Can't rotate word scramble
            charObjects[i].index = i;
        }
    }

    // Show a random word to the screen
    public void ShowScramble()
    {
        ShowScramble(Random.Range(0, words.Length - 1));
    }

    // show word from collection with desired index
    public void ShowScramble(int index)
    {
        charObjects.Clear();

        foreach (Transform child in container)
        {
            if (originalId == 0) {
                originalId = child.gameObject.GetInstanceID();
            } else if (originalId != child.gameObject.GetInstanceID()) {
                Destroy(child.gameObject);
            }
        }

        char[] chars;
        if (!finished) {
            // Translate the word to the desire language
             //Debug.Log(words[index].word);
            //chars = words[index].GetString().ToCharArray();
            try {
	            string userChoice = PlayerPrefs.GetString("languageChoice");
                
                // For debugging
                /****************************\
                if (userChoice == "") {
                    print("no user choice");
                    userChoice = "en";
                }
                \****************************/

	            translatedWord = tr.Translate(words[index].word, "en", userChoice);
	            Word word = new Word(translatedWord);

                tr.TextToSpeech(translatedWord, userChoice, "UTF-8");
	            // Debug.Log(translatedWord);
	            if (translatedWord == null)
	            {
	                Debug.Log("Its null!");
	            }
	            chars = word.GetString().ToCharArray(); // ADD GetString()
                
            } catch (System.Exception exc) {
                Debug.Log(exc);
            	chars = exc.ToString().ToCharArray();
            }
        } else {
            string done = "DONE!";
            chars = done.ToCharArray();

            PuzzleManager.Increment();
            _action.DoAction();
        }

        foreach (char c in chars)
        {
            CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
            clone.transform.SetParent(container);
            //clone.currentChar = c.ToString();

            // Debug.Log(clone.currentChar);
            charObjects.Add(clone.Init(c, this));
        }

        RepositionObject();
    }

    // Swap between letters
    public void Swap(int indexA, int indexB)
    {
        CharObject tmpA = charObjects[indexA];
        charObjects[indexA] = charObjects[indexB];
        charObjects[indexB] = tmpA;

        charObjects[indexA].transform.SetAsLastSibling();
        charObjects[indexB].transform.SetAsLastSibling();
        RepositionObject();
    }

    // Select letter
    public void Select (CharObject charObject)
    {
        if (finished) return;

        if (firstSelected)
        {
            Swap(firstSelected.index, charObject.index);
            // Unselect
            firstSelected.Select();
            charObject.Select();
        }
        else {
            firstSelected = charObject;
        }
    }

    public void UnSelect()
    {
        firstSelected = null;
    }

    public bool CheckWord()
    {
        if (finished) return true;

        string word = "";
        foreach (CharObject charObject in charObjects)
        {
            word += charObject.character;
        }

        if (word == translatedWord)
        {
            // Word is correct, go to next word
            // If we want something else happen when there is success, here the change should be
            currentWord += 1;
            if (currentWord == words.Length) {
                finished = true;
            }
            ShowScramble(currentWord);
            //Debug.Log("Success!");
            return true;
        }

        // Word isn't correct yet
        //Debug.Log("Failure :(");
        return false;
    }

    public void SetWords(Word[] words)
    {
        this.words = words;
        ShowScramble(currentWord);
    }

}
