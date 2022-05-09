using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleSetter 
{
    public  Dictionary<string, List<string>> LevelWords;
    private System.Random random;

    public PuzzleSetter() {
        random = new System.Random();

        LevelWords = new Dictionary<string, List<string>>();
                /* Set which words can be in each level's WordScramble */
        /*==================================================================*/
        LevelWords["SampleScene"] = new List<string>() {
            "banana", "orange", "pineapple", "corn", "tomato", "watermelon"};

        LevelWords["Scene 1"] = new List<string>() {
            "cat", "dog", "wolf"};

        LevelWords["Scene 2"] = new List<string>() {
            "corn", "tomato"};

        LevelWords["Asi Demo 1"] = new List<string>() {
            "game", "puzzle", "brick", "wood"};

        LevelWords["Asi Demo 2"] = new List<string>() {
            "cave", "grass", "sun"};

        LevelWords["Asi Demo 3"] = new List<string>() {
            "rocks", "sand", "mountain", "snake", "lizard"};

        LevelWords["Random"] = new List<string>() {
            "dog", "cat", "hello"};
            
        LevelWords["Door Test"] = new List<string>() {
            "dog", "cat", "hello"};


        LevelWords["Park"] = new List<string>() {
            "mushroom","cow","fish","bird",
            "squirrel","spiders", "bee", "sheep", "turtle", "bench",
            "pig","chicken", "duck", "grass", "tree", "water", "bridge", "butterfly"};

        LevelWords["Beach"] = new List<string>() {
            "umbrella", "sand", "sea", "chair", "tree", "surf", "bucket", "wheel", "clouds", "whale",
            "castle", "fish", "balloons", "water", "shells", "jellyfish", "beach", "waves", "shore",
            "towel", "sunglasses", "ball", "kite"};

        /*==================================================================*/
    }

    public void SetMatchObject(List<string> names, int numToSet = int.MaxValue, bool randomize = true) {
        MatchObject[] matchObjects = Object.FindObjectsOfType<MatchObject>();
        int numOfNames = names.Count;
        int numOfMatchObjects = matchObjects.Count();
        int toSet = Mathf.Min(numOfNames, numOfMatchObjects, numToSet);
        int i;

        if (randomize) {
            matchObjects = matchObjects.OrderBy(x => random.Next()).ToArray();
        }

        for (i = 0; i < toSet; i++) {
            matchObjects[i].SetObject(names[i]);
        }

        // turn off MatchObjects that don't have a word assigned to them
        for (; i < numOfMatchObjects; i++) {
            matchObjects[i].transform.parent.gameObject.SetActive(false);

            // puzzle would have already counted itself
            PuzzleManager.AddPuzzle(-1);
        }
    }

    public void SetWordScramble(List<string> words, bool randomize = true) {
        WordScramble[] wordScrambles = Object.FindObjectsOfType<WordScramble>();
        int numOfWSs = wordScrambles.Count();
        int numOfWords = words.Count;
        int numOfWSstofill = numOfWSs < numOfWords ? numOfWSs : numOfWords;
        int numWordsPerWS;
        int wordIndex = 0;
        int i;

        if (randomize) {
            wordScrambles = wordScrambles.OrderBy(x => random.Next()).ToArray();
        }

        if (numOfWSs != 0) {
            numWordsPerWS = numOfWords / numOfWSstofill;

            // set n-1 word scrambles
            for (i = 0; i < numOfWSstofill - 1; i++) {
                Word[] wordArr = new Word[numWordsPerWS];
                for (int j = 0; j < numWordsPerWS; j++) {
                    wordArr[j] = new Word(words[wordIndex++]);
                }
                wordScrambles[i].SetWords(wordArr);
            }

            // sets last word scramble
            int remainder = numOfWords - wordIndex;
            Word[] lastArr = new Word[remainder];
            for (int k = 0; k < remainder; k++) {
                lastArr[k] = new Word(words[wordIndex++]);
            }
            wordScrambles[i].SetWords(lastArr);

            // if we have fewer words than word scrambles
            if (numOfWSstofill != numOfWSs) {
                i++;
                for (; i < numOfWSs; i++) {
                    // deactivate word scrambles that don't have words
                    wordScrambles[i].gameObject.transform.parent.gameObject.SetActive(false);
                    // puzzle would have already counted itself
                    PuzzleManager.AddPuzzle(-1);
                }
            }
        }
    }

    public List<string> RandomizeWordScramble(string levelName, int numOfWords) {
        if (!LevelWords.ContainsKey(levelName)) {
            Debug.Log("Level name not in LevelWords dictionary.");
            return null;
        }

        List<string> randWords;
        List<string> possibleWords = LevelWords[levelName];

        randWords = possibleWords.OrderBy(x => random.Next()).Take(numOfWords).ToList();
        SetWordScramble(randWords);
        return randWords;
    }
}
