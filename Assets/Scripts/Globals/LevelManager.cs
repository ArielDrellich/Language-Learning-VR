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
    /*-------------------------------------------------*/
                    /** KEEP UPDATED **/
    const int Num_of_menu_screens_in_build = 4;
    /*-------------------------------------------------*/

    private Dictionary<string, LevelPuzzleVars> levelsSinceLastCheckpoint;
    private AsyncOperation loadingOperation;
    private ItemSpawner    itemSpawner;
    private PuzzleSetter   puzzleSetter;
    private GameObject     player;
    private GameObject     aimSet;
    private SpriteRenderer loadingSprite;
    private int[]  sceneOrder;
    private int    sceneIndex;
    private int    amountOfLevels;
    private bool   launchApp     = true;
    private bool   startGame     = true;
    private bool   shuffleLevels = false;
    private bool   isTutorial    = false;
    private string difficulty;
    public  int    checkpoint;



    public void NextLevel()
    {
        if (startGame) {
            startGame = false;
            TimerManager.StartTimer();
            /***********/
            //Might figure out better level order randomization after
            if (shuffleLevels) {
                System.Random rand = new System.Random();
                sceneOrder = sceneOrder.OrderBy(x => rand.Next()).ToArray();
            }
            /***********/
        }

        TimerManager.PauseTimer(); 
        loadingSprite.enabled = true;

        // temporarily set checkpoint every 2 levels. Will change later depending on how many levels we add
        if (sceneIndex % 2 == 0) {
            checkpoint = sceneIndex;
            levelsSinceLastCheckpoint.Clear();
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

    private void SetLevelPuzzleVars()
    {
        string levelName = GetLevelNameByIndex(sceneIndex);
        
        /* "Difficulty scaling" */
        int numOfItems;
        int numOfMatchObjects;
        int numOfWords;

        switch (difficulty)
        {
            case "easy":
                numOfItems = 8 + (sceneIndex * 1);
                numOfMatchObjects = 3 + sceneIndex;
                numOfWords = 2 + sceneIndex;
                break;
            case "medium":
                numOfItems = 15 + (sceneIndex * 2);
                numOfMatchObjects = 5 + sceneIndex;
                numOfWords = 6 + sceneIndex;
                break;
            case "hard":
                numOfItems = 20 + (sceneIndex * 3);
                numOfMatchObjects = 7 + sceneIndex;
                numOfWords = 10 + sceneIndex;
                break;
            default:
                numOfItems = 0;
                numOfMatchObjects = 0;
                numOfWords = 0;
                break;

        }
        // Spawn level items and MatchObject puzzles
        if (itemSpawner.LevelItems.ContainsKey(levelName)) {
            this.SpawnItems(levelName, numOfItems);
            puzzleSetter.SetMatchObject(levelsSinceLastCheckpoint[levelName].items, numOfMatchObjects);
        }

        // Set WordScramble words
        this.SetWordScramble(levelName, numOfWords);
    }

    private void SetWordScramble(string levelName, int numOfWords)
    {
        List<string> wordsInScramble;

        // if we haven't already been to this level since last checkpoint
        if (!levelsSinceLastCheckpoint.ContainsKey(levelName)
                        || levelsSinceLastCheckpoint[levelName].words == null) {

            wordsInScramble = puzzleSetter.RandomizeWordScramble(levelName, numOfWords);

            // save words for if we return to checkpoint
            if (!levelsSinceLastCheckpoint.ContainsKey(levelName)) {
                levelsSinceLastCheckpoint[levelName] = new LevelPuzzleVars(words: wordsInScramble);
            }
            // if levelsSinceLastCheckpoint contains the levelname but not yet words
            else {
                levelsSinceLastCheckpoint[levelName].words = wordsInScramble;
            }

        // if we have been to this level since last checkpoint, use the same words
        } else {
            puzzleSetter.SetWordScramble(levelsSinceLastCheckpoint[levelName].words);
        }

    }

    private void SpawnItems(string levelName, int numOfItems)
    {
        List<Vector3> positions;
        List<string>  itemList;

        // if we haven't already been to this level since last checkpoint
        if (!levelsSinceLastCheckpoint.ContainsKey(levelName)
                        || levelsSinceLastCheckpoint[levelName].items == null) {

            positions = itemSpawner.GetPossiblePositions();

            itemList = itemSpawner.ChooseSpawnItems(levelName, numOfItems);

            itemSpawner.RandomizeItemAndPositionOrder(itemList, positions);
            
            // save items and positions for if we return to checkpoint
            if (!levelsSinceLastCheckpoint.ContainsKey(levelName)) {
                levelsSinceLastCheckpoint[levelName] = new LevelPuzzleVars(items: itemList, positions: positions);
            }
            // if levelsSinceLastCheckpoint contains the levelname but not yet items
            else {
                levelsSinceLastCheckpoint[levelName].items = itemList;
                levelsSinceLastCheckpoint[levelName].itemPositions = positions;
            }

        // if we have been to this level since last checkpoint, use the same items and positions
        } else {
            itemList = levelsSinceLastCheckpoint[levelName].items;
            positions = levelsSinceLastCheckpoint[levelName].itemPositions;
            
            //just to delete ItemSpawners
            itemSpawner.GetPossiblePositions();
        }

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
        levelsSinceLastCheckpoint.Clear();
        loadingOperation = SceneManager.LoadSceneAsync("Start Menu");
        HealthManager.ResetHealth();
    }

    public void Tutorial()
    {
        isTutorial = true;
        loadingSprite.enabled = true;
        aimSet.GetComponent<AimClick>().enabled = false;
        loadingOperation = SceneManager.LoadSceneAsync("Tutorial");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.Start();
        
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Game Over Screen" && sceneName != "Win Screen") 
            TimerManager.StartTimer();
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
        if (startGame) {
            // only add once on app launch
            if(launchApp) {
                SceneManager.sceneLoaded += OnSceneLoaded;
                launchApp = false;
            }
            
            itemSpawner = new ItemSpawner();
            puzzleSetter = new PuzzleSetter();
            levelsSinceLastCheckpoint = new Dictionary<string, LevelPuzzleVars>();

            amountOfLevels = SceneManager.sceneCountInBuildSettings - Num_of_menu_screens_in_build;
            sceneOrder = new int[amountOfLevels];

            for (int i = 0; i < amountOfLevels; i++) {
                sceneOrder[i] = i + Num_of_menu_screens_in_build;
            }

            sceneIndex = 0;
            checkpoint = sceneOrder[0];
        }

        DontDestroyOnLoad(this);
    }

    public string GetLevelNameByIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index + Num_of_menu_screens_in_build);
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

    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.quitting += ResetPref;
    }
}
