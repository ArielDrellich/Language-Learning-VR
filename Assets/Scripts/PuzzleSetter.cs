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
            "rocks", "sand", "mountain", "snake", "lizard"};

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
            matchObjects[i].gameObject.SetActive(false);

            // puzzle would have already counted itself
            PuzzleManager.AddPuzzle(-1);
        }
    }

    public void SetWordScramble(List<string> words) {
        WordScramble[] wordScrambles = Object.FindObjectsOfType<WordScramble>();
        int numOfWordScrambles = wordScrambles.Count();
        int numOfWords = words.Count;
        int numWordsPerWordScramble;
        int wordIndex = 0;
        int i;

        if (numOfWordScrambles != 0) {
            numWordsPerWordScramble = numOfWords / numOfWordScrambles;

            // set n-1 word scrambles
            for (i = 0; i < numOfWordScrambles - 1; i++) {
                Word[] wordArr = new Word[numWordsPerWordScramble];
                for (int j = 0; j < numWordsPerWordScramble; j++) {
                    wordArr[j] = new Word(words[wordIndex++]);
                }
                // wordScrambles[i].words = wordArr;
                wordScrambles[i].SetWords(wordArr);
            }

            // sets last word scramble
            int remainder = numOfWords - wordIndex;
            Word[] lastArr = new Word[remainder];
            for (int k = 0; k < remainder; k++) {
                lastArr[k] = new Word(words[wordIndex++]);
            }
            wordScrambles[i].SetWords(lastArr);
            // wordScrambles[i].words = lastArr;
        }
    }

    public List<string> RandomizeWordScramble(string levelName, int numOfWords) {
        if (!LevelWords.ContainsKey(levelName)) {
            return null;
        }

        List<string> randWords;
        List<string> possibleWords = LevelWords[levelName];

        randWords = possibleWords.OrderBy(x => random.Next()).Take(numOfWords).ToList();
        SetWordScramble(randWords);
        return randWords;
    }
}
