using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System;

[System.Serializable]
public class Word
{
    public string word;
    [Header("Leave empty if you want to randomized")]
    public string desireRandom;

    public Word(string w) {
    	word = w;
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
                int indexChar = UnityEngine.Random.Range(0, characters.Count);
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
    private string lastWrongWord = "";
    private string translatedWord;
    public Translator tr;
    public  List<Component>  successActions;
    public  List<Component>  failActions;

    //trimming Hebrew diacritics which are special because they're seperate unicode characters
    char[] toTrim = { '\u05B0', '\u05B1', '\u05B2',
         '\u05B3', '\u05B4', '\u05B5', '\u05B6', '\u05B7', '\u05B8',
          '\u05B9', '\u05BA', '\u05BB', '\u05BC', '\u05BD', '\u05C1',
          '\u05C2', '\u05C4', '\u05C5', '\u05C7',   };

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

        if (words.Length != 0)
            ShowScramble(currentWord);

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
        ShowScramble(UnityEngine.Random.Range(0, words.Length - 1));
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
            // Translate the word to the desired language

            try {
	            string userChoice = PlayerPrefs.GetString("languageChoice");
                
                // For debugging
                if (userChoice == "") {
                    print("no user choice");
                    userChoice = "en";
                }

	            translatedWord = tr.Translate(words[index].word, "en", userChoice).ToLower();

                // Remove words with spaces in them
                if (translatedWord.Any(x => Char.IsWhiteSpace(x)))
                {
                    print("WordScramble: Skipping \""+translatedWord+
                        "\" because of spaces. Original word was \""+words[index].word+"\".");
                    currentWord += 1;
                    if (currentWord >= words.Length)
                    {
                        finished = true;
                    }

                    // Special case if there was only one word and we erased it
                    if (words.Length == 1)
                    {
                        gameObject.transform.parent.gameObject.SetActive(false);
                    }

                    ShowScramble(currentWord);
                    return;
                }

                // trimming unwanted diacritics off of the word
                char[] tempArr = translatedWord.ToCharArray();
                translatedWord = "";
                foreach (char c in tempArr) 
                {
                    if (!toTrim.Contains(c))
                        translatedWord += c;
                }

	            Word word = new Word(translatedWord);

                tr.TextToSpeech(translatedWord, userChoice, "UTF-8");

	            if (translatedWord == null)
	            {
	                Debug.Log("ShowScramble: Translated word is null");
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

            // Do all SuccessActions
            foreach (Component action in successActions)
                if (action is IAction) {
                    ((IAction)action).DoAction();
                }
        }

        foreach (char c in chars)
        {
            CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
            clone.transform.SetParent(container);

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

        if (PlayerPrefs.GetInt("isRTL") == 1)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse( charArray );
            word = new string( charArray );
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

            return true;
        }

        // Word isn't correct yet

        // to not fail twice in a row on the same permutation
        if (word != lastWrongWord)
        {
            lastWrongWord = word;

            HealthManager.Decrement();

            // Do all FailActions
            foreach (Component action in failActions)
                if (action is IAction) {
                    ((IAction)action).DoAction();
                }
        }

        return false;
    }

    public void SetWords(Word[] words)
    {
        this.words = words;
        ShowScramble(currentWord);
    }

}
