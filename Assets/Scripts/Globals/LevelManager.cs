using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelPuzzleVars
{
    public List<string>  items;
    public List<Vector3> itemPositions;
    public List<string>  words;

    public LevelPuzzleVars(List<string> items = null,
                List<Vector3> positions = null, List<string> words = null)
    {
        this.items = items;
        this.itemPositions = positions;
        this.words = words;
    }
}
public class LevelManager : MonoBehaviour
{
    /*---------------------------------------------------------------------------*\
    |  Build settings:                                                            |
    |                 1) Keep all menu scenes in the build before other levels.   |
    |                 2) Keep Forest scene as the last in the build.              |
    \*---------------------------------------------------------------------------*/

    /*=================================================*\
                         KEEP UPDATED                   
    =====================================================
    /**/
    /**/    const int Num_of_menu_screens_in_build = 4;
    /**/
    /*=================================================*/
    

    private Dictionary<string, LevelPuzzleVars> visitedLevels;
    private AsyncOperation loadingOperation;
    private ItemSpawner    itemSpawner;
    private PuzzleSetter   puzzleSetter;
    private GameObject     player;
    private GameObject     aimSet;
    private SpriteRenderer loadingSprite;
    private int[]          sceneOrder;
    private int            sceneIndex;
    private int            amountOfLevels;

    /*-------------------------------*/
    public int             score = 0;
    private int            firstLevelScore;
    private int            levelScoreIncreaseBy;
    private float          timeMultiplier;
    private int            lastLevelTime = 0;
    /*-------------------------------*/

    private bool           launchApp     = true;
    private bool           startGame     = true;
    private bool           shuffleLevels = false;
    private bool           isTutorial    = false;
    private string         difficulty;
    public  int            checkpoint;



    public void NextLevel()
    {
        if (startGame) 
        {
            startGame = false;
            TimerManager.StartTimer();

            if (shuffleLevels) 
            {
                System.Random rand = new System.Random();

                // we want to keep the forest scene as the last since it is a special case.
                // NOTE: this requires leaving the Forest scene as the last in the build.
                sceneOrder =
                        sceneOrder.Take(0).Concat(sceneOrder.Take(sceneOrder.Count() - 1)
                        .OrderBy(x => rand.Next()))
                        .Concat(sceneOrder.Skip(sceneOrder.Count() - 1)).ToArray();
            }
        }

        TimerManager.PauseTimer(); 
        loadingSprite.enabled = true;

        if (sceneIndex != 0)
        {
            updateScore();
        }

        // Set checkpoint every 2 levels. Can change to be more or less frequent.
        if (sceneIndex % 2 == 0) 
        {
            checkpoint = sceneIndex;
        }

        player.GetComponent<PlayerMovement>().enabled = false;
        aimSet.GetComponent<AimClick>().enabled = false;
        TeleportPad.CanTeleport(false);

        if (sceneIndex != amountOfLevels)
            StartCoroutine(LoadNextSceneAsync());
        else
            SceneManager.LoadSceneAsync("Win Screen");
    }

    private IEnumerator LoadNextSceneAsync()
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneOrder[sceneIndex]);

        // wait for scene to be fully loaded before continuing
        while (!loadingOperation.isDone)
        {
            yield return null;
        }

        SetLevelPuzzleVars();

        sceneIndex++;
    }

    private void GetLevelPuzzleNums(int scene_index, out int numOfItems,
                                    out int numOfMatchObjects, out int numOfWords)
    {
        switch (difficulty)
        {
            case "easy":
                numOfItems = 6 + (scene_index * 1);
                numOfMatchObjects = 3 + scene_index;
                numOfWords = 2 + scene_index;

                firstLevelScore = 200;
                levelScoreIncreaseBy = 50;
                timeMultiplier = 1/3f;
                break;

            case "medium":
                numOfItems = 12 + (scene_index * 2);
                numOfMatchObjects = 5 + scene_index;
                numOfWords = 6 + scene_index;

                firstLevelScore = 400;
                levelScoreIncreaseBy = 100;
                timeMultiplier = 1/2f;
                break;

            case "hard":
                numOfItems = 18 + (scene_index * 3);
                numOfMatchObjects = 7 + scene_index;
                numOfWords = 10 + scene_index;

                firstLevelScore = 1000;
                levelScoreIncreaseBy = 200;
                timeMultiplier = 1;
                break;

            default:
                numOfItems = 0;
                numOfMatchObjects = 0;
                numOfWords = 0;
                firstLevelScore = 0;
                levelScoreIncreaseBy = 0;
                timeMultiplier = 0;
                Debug.Log("SetLevelPuzzleVars: Invalid difficulty choice \""+difficulty+"\".");
                break;

        }
    }

    private void SetLastLevelPuzzleVars()
    {
        List<string>  forestItems = new List<string>();
        List<string>  forestWords = new List<string>();
        List<Vector3> positions   = itemSpawner.GetPossiblePositions();
        
        int totalNumOfMatchObjects = 0;

        for(int i = 0; i < amountOfLevels - 1; i++)
        {
            int    numOfItems        = 0;
            int    numOfWords        = 0;
            int    numOfMatchObjects = 0;
            string levelName         = GetLevelNameByIndex(i);

            GetLevelPuzzleNums(i, out numOfItems, out numOfMatchObjects, out numOfWords);

            forestItems.AddRange(visitedLevels[levelName].items.Take(numOfMatchObjects));

            forestWords.AddRange(visitedLevels[levelName].words.Take(numOfWords));

            totalNumOfMatchObjects += numOfMatchObjects;
        }

        itemSpawner.SpawnItems(forestItems, positions);
        puzzleSetter.SetMatchObject(forestItems, totalNumOfMatchObjects);

        puzzleSetter.SetWordScramble(forestWords);
    }

    private void SetLevelPuzzleVars()
    {
        if (sceneIndex == amountOfLevels - 1)
        {
            SetLastLevelPuzzleVars();
            return;
        }

        string levelName = GetLevelNameByIndex(sceneIndex);
        
        /* "Difficulty scaling" */
        int numOfItems;
        int numOfMatchObjects;
        int numOfWords;

        GetLevelPuzzleNums(sceneIndex, out numOfItems, out numOfMatchObjects, out numOfWords);
        
        // Spawn level items and MatchObject puzzles
        if (itemSpawner.LevelItems.ContainsKey(levelName)) 
        {
            this.SpawnItems(levelName, numOfItems);

            puzzleSetter.SetMatchObject(visitedLevels[levelName].items, numOfMatchObjects);
        }

        // Set WordScramble words
        this.SetWordScramble(levelName, numOfWords);
    }

    private void SetWordScramble(string levelName, int numOfWords)
    {
        List<string> wordsInScramble;

        // if we haven't already been to this level since last checkpoint
        if (!visitedLevels.ContainsKey(levelName)
                        || visitedLevels[levelName].words == null) 
        {

            wordsInScramble = puzzleSetter.RandomizeWordScramble(levelName, numOfWords);

            // save words for if we return to checkpoint
            if (!visitedLevels.ContainsKey(levelName))
            {
                visitedLevels[levelName] = new LevelPuzzleVars(words: wordsInScramble);
            }
            // if visitedLevels contains the levelname but not yet words
            else 
            {
                visitedLevels[levelName].words = wordsInScramble;
            }

        // if we have been to this level since last checkpoint, use the same words
        } 
        else
        {
            puzzleSetter.SetWordScramble(visitedLevels[levelName].words);
        }

    }

    private void SpawnItems(string levelName, int numOfItems)
    {
        List<Vector3> positions;
        List<string>  itemList;

        // if we haven't already been to this level since last checkpoint
        if (!visitedLevels.ContainsKey(levelName)
                        || visitedLevels[levelName].items == null)
        {

            positions = itemSpawner.GetPossiblePositions();

            itemList = itemSpawner.ChooseSpawnItems(levelName, numOfItems);

            itemSpawner.RandomizeItemAndPositionOrder(itemList, positions);
            
            // save items and positions for if we return to checkpoint
            if (!visitedLevels.ContainsKey(levelName)) 
            {
                visitedLevels[levelName] = 
                    new LevelPuzzleVars(items: new List<string>(itemList),
                                        positions: new List<Vector3>(positions));
            }
            // if visitedLevels contains the levelname but not yet items
            else 
            {
                visitedLevels[levelName].items = itemList;
                visitedLevels[levelName].itemPositions = positions;
            }

        // if we have been to this level since last checkpoint, use the same items and positions
        } 
        else
        {
            itemList = visitedLevels[levelName].items;
            positions = visitedLevels[levelName].itemPositions;
            
            //just to delete ItemSpawners
            itemSpawner.GetPossiblePositions();
        }

        /* Duplicate items in list to make it easier to find */
        itemList.AddRange(itemList);

        itemSpawner.SpawnItems(itemList, positions);
    }

    public void LoadCheckpoint()
    {
        sceneIndex = checkpoint;
        loadingSprite.enabled = true;
        HealthManager.ResetHealth();
        StartCoroutine(LoadNextSceneAsync());
    }

    public void GameOver()
    {
        loadingSprite.enabled = true;
        aimSet.GetComponent<AimClick>().enabled = false;
        TimerManager.PauseTimer();
        PuzzleManager.ResetCounters();
        loadingOperation = SceneManager.LoadSceneAsync("Game Over Screen");
    }

    public void MainMenu()
    {
        startGame = true;
        isTutorial = false;
        loadingSprite.enabled = true;
        aimSet.GetComponent<AimClick>().enabled = false;
        ResetPref();
        TimerManager.StopTimer();
        visitedLevels.Clear();
        loadingOperation = SceneManager.LoadSceneAsync("Start Menu");
        HealthManager.ResetHealth();
    }

    public void Tutorial()
    {
        isTutorial = true;
        loadingSprite.enabled = true;
        aimSet.GetComponent<AimClick>().enabled = false;

        if (PlayerPrefs.GetString("languageChoice") == "")
        {
            PlayerPrefs.SetString("languageChoice", "en");
            PlayerPrefs.SetInt("isRTL", 0);
        }

        loadingOperation = SceneManager.LoadSceneAsync("Tutorial");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.Start();
        
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Game Over Screen" && sceneName != "Win Screen") 
        {
            TimerManager.StartTimer();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        shuffleLevels = false;
        player = GameObject.Find("Player");
        aimSet = GameObject.Find("Aim Set");
        difficulty = PlayerPrefs.GetString("Difficulty");
        loadingSprite = GameObject.Find("Loading_Sprite").GetComponent<SpriteRenderer>();

        // any time we start a new game from the main menu
        if (startGame) 
        {
            // only add once on app launch
            if(launchApp) 
            {
                ResetPref();
                SceneManager.sceneLoaded += OnSceneLoaded;
                itemSpawner = new ItemSpawner();
                puzzleSetter = new PuzzleSetter();
                launchApp = false;
            }
            
            visitedLevels = new Dictionary<string, LevelPuzzleVars>();

            amountOfLevels = SceneManager.sceneCountInBuildSettings - Num_of_menu_screens_in_build;
            sceneOrder = new int[amountOfLevels];

            for (int i = 0; i < amountOfLevels; i++) 
            {
                sceneOrder[i] = i + Num_of_menu_screens_in_build;
            }

            sceneIndex = 0;
            checkpoint = sceneOrder[0];
        }

        DontDestroyOnLoad(this);
    }

    private void updateScore()
    {
        int maxLevelScore = firstLevelScore + ((sceneIndex-1) * levelScoreIncreaseBy);

        // special case for long last level
        if (sceneIndex == amountOfLevels)
        {
            maxLevelScore *= 3;
        }

        int playTime = (int)TimerManager.GetPlaytime();
        int thisLevelTime = playTime - lastLevelTime;
        int thisLevelScore = Mathf.Max(0, (int)(maxLevelScore - (thisLevelTime * timeMultiplier)));

        lastLevelTime = playTime;
        score += thisLevelScore;
    }

    public string GetLevelNameByIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneOrder[index]);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

    public void ShuffleLevels(bool shuffle) 
    {
        shuffleLevels = shuffle;
    }

    public void ZeroHealth()
    {
        if (!isTutorial)
        {
            this.GameOver();
        }
    }
    static void ResetPref()
    {
        PlayerPrefs.DeleteKey("languageChoice");
        PlayerPrefs.DeleteKey("languageIndex");
        PlayerPrefs.DeleteKey("Difficulty");
        PlayerPrefs.DeleteKey("isRTL");

    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.quitting += ResetPref;
    }
}
