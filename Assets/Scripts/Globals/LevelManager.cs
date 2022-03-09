﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    const int Num_of_menu_screens_in_build = 2;
    private static AsyncOperation loadingOperation;
    private static GameObject player;
    private static GameObject aimSet;
    private static SpriteRenderer loadingSprite;
    private static int[] sceneOrder;
    private static int sceneIndex;
    private static int amountOfLevels;
    private static bool launchApp = true;
    private static bool startGame = true;
    private static bool shuffleLevels = false;
    public static int checkpoint;

    public static void NextLevel()
    {
        /**********************************************/
        // if (sceneIndex + 1 == amountOfLevels)
            // SceneManager.LoadSceneAsync("Win Screen"); // once we have a win screen
        /**********************************************/

        if (startGame) {
            startGame = false;
            TimerManager.StartTimer();
            /***********/
            //super temporary, will figure out randomization after
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

        loadingOperation = SceneManager.LoadSceneAsync(sceneOrder[sceneIndex++]);
        player.GetComponent<PlayerMovement>().enabled = false;
        aimSet.GetComponent<PickUp>().enabled = false;
        TeleportPad.CanTeleport(false);
    }

    public static void LoadCheckpoint()
    {
        sceneIndex = checkpoint;
        loadingSprite.enabled = true;
        HealthManager.ResetHealth();
        loadingOperation = SceneManager.LoadSceneAsync(sceneOrder[sceneIndex++]);
    }

    public static void GameOver()
    {
        loadingSprite.enabled = true;
        TimerManager.PauseTimer();
        // HealthManager.ResetHealth();
        PuzzleManager.ResetCounters();
        loadingOperation = SceneManager.LoadSceneAsync("Game Over Screen");
    }

    public static void MainMenu()
    {
        startGame = true;
        HealthManager.ResetHealth();
        TimerManager.StopTimer();
        SceneManager.LoadScene("Start Menu");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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

            for (int i = 0; i < amountOfLevels; i++)
                sceneOrder[i] = i + Num_of_menu_screens_in_build;

            sceneIndex = 0;
            checkpoint = sceneOrder[0];
        }
    }

    public static void ShuffleLevels(bool shuffle) 
    {
        shuffleLevels = shuffle;
    }
}
