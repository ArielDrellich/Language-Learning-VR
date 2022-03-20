using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    const int Num_of_menu_screens_in_build = 2;
    private AsyncOperation loadingOperation;
    private GameObject player;
    private GameObject aimSet;
    private SpriteRenderer loadingSprite;
    private int[] sceneOrder;
    private int sceneIndex;
    private int amountOfLevels;
    private bool launchApp = true;
    private bool startGame = true;
    private bool shuffleLevels = false;
    public int checkpoint;

    public void NextLevel()
    {
        /**********************************************/
        // if (sceneIndex + 1 == amountOfLevels)
            // SceneManager.LoadSceneAsync("Win Screen"); // once we have a win screen
        /**********************************************/

        if (startGame) {
            startGame = false;
            TimerManager.StartTimer();
            /***********/
            //temporary, will figure out better randomization after
            if (shuffleLevels) {
                System.Random rand = new System.Random();
                sceneOrder = sceneOrder.OrderBy(x => rand.Next()).ToArray();
            }
            /***********/
        }

        TimerManager.PauseTimer(); 
        loadingSprite.enabled = true;

        // temporarily set checkpoint every 2 levels. Will change later
        if (sceneIndex % 2 == 0)
            checkpoint = sceneIndex;

        player.GetComponent<PlayerMovement>().enabled = false;
        aimSet.GetComponent<PickUp>().enabled = false;
        TeleportPad.CanTeleport(false);

        // loadingOperation = SceneManager.LoadSceneAsync(sceneOrder[sceneIndex++]);
        StartCoroutine(LoadNextSceneAsync());
        
    }

    private IEnumerator LoadNextSceneAsync()
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneOrder[sceneIndex++]);
        while (!loadingOperation.isDone)
        {
            yield return null;
        }
        //call item spawner
    }

    public void LoadCheckpoint()
    {
        sceneIndex = checkpoint;
        loadingSprite.enabled = true;
        HealthManager.ResetHealth();
        loadingOperation = SceneManager.LoadSceneAsync(sceneOrder[sceneIndex++]);
    }

    public void GameOver()
    {
        loadingSprite.enabled = true;
        TimerManager.PauseTimer();
        PuzzleManager.ResetCounters();
        loadingOperation = SceneManager.LoadSceneAsync("Game Over Screen");
    }

    public void MainMenu()
    {
        startGame = true;
        HealthManager.ResetHealth();
        TimerManager.StopTimer();
        this.Start();
        SceneManager.LoadScene("Start Menu");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
        aimSet = GameObject.Find("Aim Set");
        loadingSprite = GameObject.Find("Loading_Sprite").GetComponent<SpriteRenderer>();

        if (SceneManager.GetActiveScene().name != "Game Over Screen")
            TimerManager.StartTimer();
    }

    // Start is called before the first frame update
    void Start()
    {
        shuffleLevels = false;
        player = GameObject.Find("Player");
        aimSet = GameObject.Find("Aim Set");
        loadingSprite = GameObject.Find("Loading_Sprite").GetComponent<SpriteRenderer>();
        if (startGame) {
            if(launchApp) {
                SceneManager.sceneLoaded += OnSceneLoaded;
                launchApp = false;
            }

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
}
