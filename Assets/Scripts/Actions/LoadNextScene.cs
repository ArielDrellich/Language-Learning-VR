using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour, IAction
{
    public void DoAction()
    {
        LevelManager.NextLevel();
    }
}