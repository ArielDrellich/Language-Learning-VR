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
            "snack", "grocery", "shop", "shopkeeper", "shopping", "money", "fence", "barrel", "fountain", "fire",
            "hay", "neighborhood"
        };

        LevelWords["Park"] = new List<string>()
        {
            "mushroom","cow","fish","bird",
            "squirrel","spider", "bee", "sheep", "turtle", "bench",
            "pig","chicken", "duck", "grass", "tree", "lake", "bridge", "butterfly",
            "hill", "mountain", "boat", "picnic", "flower"
        };

        LevelWords["Beach"] = new List<string>()
        {
            "umbrella", "sand", "sea", "chair", "tree", "surf", "bucket", /*"wheel",*/ "cloud", "whale",
            "castle", "fish", "balloon", "water", "seashell", "jellyfish", "beach", "waves", "shore",
            "towel", "sunglasses", "ball", "kite"
        };

        LevelWords["PlaygroundLowPoly"] = new List<string>() 
        {
            "swing", "adventure", "slide", "bench", "building", "cottage", "motorcycle", "vehicle", "police",
            "carrousel", "ladder", "sandbox", "climb", "traffic light", "road", "sidewalk", "grass", "lamp", "bus station",
            "flower", "plane", "helicopter", "flight", "fun", "hot", "sun"
        };

        LevelWords["Forest"] = new List<string>() 
        {
            "forest", "tree", "flower", "house", "table", "chair", "lake", "wood", "green", "nature" ,
            "leaves", "plants", "stairs", "wood", "sand", "ground", "mountain", "lake", "water", "sky", "fork",
            "spoon", "plate"
        };
        /*==================================================================*/
    }

    public void SetMatchObject(List<string> names, int numToSet = int.MaxValue, bool randomize = true)
    {
        MatchObject[] matchObjects = UnityEngine.Object.FindObjectsOfType<MatchObject>();
        int numOfNames = names.Distinct().Count();
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
        int wordIndex = 0;
        int i;

        if (randomize) 
        {
            wordScrambles = wordScrambles.OrderBy(x => random.Next()).ToArray();
        }

        int[] divArr = new int[numOfWSstofill];

        if (numOfWSstofill != 0)
        {
            // divide words evenly over WSs
            for (int j = 0; j < numOfWords; j++)
            {
                divArr[j % numOfWSstofill] += 1;
            }
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
