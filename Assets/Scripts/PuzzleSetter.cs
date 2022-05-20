using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PuzzleSetter 
{
    public  Dictionary<string, List<string>> LevelWords;
    private System.Random random;

    public PuzzleSetter()
    {
        random = new System.Random();

        LevelWords = new Dictionary<string, List<string>>();
                /* Set which words can be in each level's WordScramble */
        /*==================================================================*/
        LevelWords["SampleScene"] = new List<string>() 
        {
            "banana", "orange", "pineapple", "corn", "tomato", "watermelon"
        };

        LevelWords["Scene 1"] = new List<string>() 
        {
            "dog", "wolf"
        };

        LevelWords["Scene 2"] = new List<string>()
        {
            "corn", "tomato"
        };

        LevelWords["Asi Demo 1"] = new List<string>() 
        {
            "game", "puzzle", "brick", "wood"
        };

        LevelWords["Asi Demo 2"] = new List<string>() 
        {
            "cave", "grass", "sun"
        };

        LevelWords["Asi Demo 3"] = new List<string>() 
        {
            "rocks", "sand", "mountain", "snake", "lizard"
        };

        LevelWords["Random"] = new List<string>() 
        {
            "dog", "cat", "hello"
        };
            
        LevelWords["Door Test"] = new List<string>() 
        {
            "dog", "cat", "hello"
        };

        LevelWords["Market"] = new List<string>() 
        { 
            "market", "fruit", "vegetable", "food", "vendor", "stands", "cart", "bonfire",
            "snacks", "grocery", "shop", "shopkeeper", "shopping", "money", "fence", "barrel", "fountain", "fire",
            "hay", "neighborhood"
        };

        LevelWords["Park"] = new List<string>()
        {
            "mushroom","cow","fish","bird",
            "squirrel","spiders", "bee", "sheep", "turtle", "bench",
            "pig","chicken", "duck", "grass", "tree", "lake", "bridge", "butterfly",
            "hill", "mountain", "boat", "picnic", "flowers"
        };

        LevelWords["Beach"] = new List<string>()
        {
            "umbrella", "sand", "sea", "chair", "tree", "surf", "bucket", "wheel", "clouds", "whale",
            "castle", "fish", "balloons", "water", "shells", "jellyfish", "beach", "waves", "shore",
            "towel", "sunglasses", "ball", "kite"
        };

        LevelWords["PlaygroundLowPoly"] = new List<string>() 
        {
            "swing", "adventure", "slid", "bench", "building", "cottage", "motorcycle", "vehicle", "police",
            "carrousel", "ladder", "sandbox", "climb", "traffic lights", "road", "sidewalk", "grass", "lamp", "bus station",
            "flowers", "plane", "helicopter", "flight", "fun", "hot", "sun"
        };

        LevelWords["Forest"] = new List<string>() 
        {
            "forest", "tree", "flower", "house", "table", "chair", "lake", "wood", "green", "nature" ,
            "leaves", "plants", "stairs", "wood", "sand", "ground", "mountains", "lake", "water", "sky"
        };
        /*==================================================================*/
    }

    public void SetMatchObject(List<string> names, int numToSet = int.MaxValue, bool randomize = true)
    {
        MatchObject[] matchObjects = UnityEngine.Object.FindObjectsOfType<MatchObject>();
        int numOfNames = names.Count;
        int numOfMatchObjects = matchObjects.Count();
        int toSet = Mathf.Min(numOfNames, numOfMatchObjects, numToSet);
        int i;

        if (randomize) 
        {
            matchObjects = matchObjects.OrderBy(x => random.Next()).ToArray();
        }

        for (i = 0; i < toSet; i++) 
        {
            matchObjects[i].SetObject(names[i]);
        }

        // turn off MatchObjects that don't have a word assigned to them
        for (; i < numOfMatchObjects; i++) 
        {
            matchObjects[i].transform.parent.gameObject.SetActive(false);

            // puzzle would have already counted itself
            PuzzleManager.AddPuzzle(-1);
        }
    }

    public void SetWordScramble(List<string> words, bool randomize = true)
    {
        WordScramble[] wordScrambles = UnityEngine.Object.FindObjectsOfType<WordScramble>();
        int numOfWSs = wordScrambles.Count();
        int numOfWords = words.Count;
        int numOfWSstofill = numOfWSs < numOfWords ? numOfWSs : numOfWords;
        // int numWordsPerWS;
        int wordIndex = 0;
        int i;

        if (randomize) 
        {
            wordScrambles = wordScrambles.OrderBy(x => random.Next()).ToArray();
        }

        int[] divArr = new int[numOfWSstofill];

        // divide words evenly over WSs
        for (int j = 0; j < numOfWords; j++)
        {
            divArr[j % numOfWSstofill] += 1;
        }

        // fill WSs with the amount found above
        for (i = 0; i < numOfWSstofill; i++)
        {
            Word[] wordArr = new Word[divArr[i]];
            for (int j = 0; j < divArr[i]; j++)
            {
                wordArr[j] = new Word(words[wordIndex++]);
            }
            wordScrambles[i].SetWords(wordArr);
        }
        
        
        // if we have fewer words than word scrambles
        if (numOfWSstofill != numOfWSs)
        {
            for (; i < numOfWSs; i++) 
            {
                // deactivate word scrambles that don't have words
                wordScrambles[i].gameObject.transform.parent.gameObject.SetActive(false);
                // puzzle would have already counted itself
                PuzzleManager.AddPuzzle(-1);
            }
        }

    }

    public List<string> RandomizeWordScramble(string levelName, int numOfWords)
    {
        if (!LevelWords.ContainsKey(levelName))
        {
            Debug.Log("RandomizeWordScramble: Level name not in LevelWords dictionary.");
            SetWordScramble(new List<string>()); // to remove ws's from the scene
            return null;
        }

        List<string> randWords;
        List<string> possibleWords = LevelWords[levelName];

        randWords = possibleWords.OrderBy(x => random.Next()).Take(numOfWords).ToList();
        SetWordScramble(randWords);
        return randWords;
    }
}
