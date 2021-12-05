using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Word
{
    public string word;
    [Header("Leave empty if you want to randomized")]
    public string desireRandom;

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
    public Transform container;
    public float space;

    List<CharObject> charObjects = new List<CharObject>();
    CharObject firstSelected;
    public int currentWord;
    public static WordScramble main;


    void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowScramble(currentWord);

    }


    void Update()
    {
        RepositionObject();

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
            charObjects[i].rectTransform.anchoredPosition =
                new Vector3((i - center) * space, 0,0);
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
            Destroy(child.gameObject);
        }

        if (index > words.Length - 1)
        {
            return;
        }
        char[] chars = words[index].GetString().ToCharArray();

        foreach (char c in chars)
        {
            CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
            clone.transform.SetParent(container);
            //clone.currentChar = c.ToString();

            // Debug.Log(clone.currentChar);
            charObjects.Add(clone.Init(c));
        }
    }

    // Swap between letters
    public void Swap(int indexA, int indexB)
    {
        CharObject tmpA = charObjects[indexA];
        charObjects[indexA] = charObjects[indexB];
        charObjects[indexB] = tmpA;

        charObjects[indexA].transform.SetAsLastSibling();
        charObjects[indexB].transform.SetAsLastSibling();

        CheckWord();
    }

    // Select letter
    public void Select (CharObject charObject)
    {
        Debug.Log("Selecting");
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
        string word = "";
        foreach (CharObject charObject in charObjects)
        {
            word += charObject.character;
        }

        if (word == words[currentWord].word)
        {
            // Word is correct, go to next word
            // If we want something else happen when there is success, here the change should be
            currentWord = (currentWord + 1) % words.Length;
            ShowScramble(currentWord);
            //Debug.Log("Success!");
            return true;
        }

        // Word isn't correct yet
        //Debug.Log("Failure :(");
        return false;
    }
}
