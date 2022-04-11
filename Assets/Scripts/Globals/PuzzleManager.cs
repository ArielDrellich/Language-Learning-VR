using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private List<Component> solvedAllPuzzlesActions;
    private static List<IAction> _actions = new List<IAction>();
    private static int totalPuzzles  = 0;
    private static int puzzlesSolved = 0;
    // private static IAction _action;

    // just for possible debugging
    public static void SetTotalPuzzles(int x) {
        totalPuzzles = x;
    }

    // each puzzle should call this at the start
    public static void AddPuzzle(int x = 1) {
        totalPuzzles += x;
    }

    public static int GetTotalPuzzles() {
        return totalPuzzles;
    }

    public static int GetSolvedPuzzles() {
        return puzzlesSolved;
    }

    public static void Increment() {
        puzzlesSolved++;
        
        // Debug.Log("Puzzle solved. You've solved " + puzzlesSolved + " so far.");
        if (puzzlesSolved == totalPuzzles) {
           PuzzleManager.ResetCounters();
            // _action.DoAction();

            foreach (IAction action in _actions)
                action.DoAction();
        }
    }

    // public static void SetAction(IAction act) {
    //     _action = act;
    // }
    public static void SetActions(List<IAction> actions) {
        _actions = actions;
    }
    
    // public static IAction GetAction() {
    //     return _action;
    // }
    public static List<IAction> GetActions() {
        return _actions;
    }

    public static void ResetCounters() {
        totalPuzzles = 0;
        puzzlesSolved = 0;
    }

    void Start()
    {
        // Used for dragging script in the Inspector. If we don't need that in the end, we can remove
        //this and action, and use only _action.
        // if (action is IAction)
        //     _action = (IAction) action;
        // else
        // // if action is either null or not IAction
        //     _action = new LoadNextScene();


        // if (solvedAllPuzzlesActions == null)
        //     solvedAllPuzzlesActions = new List<Component>() {new LoadNextScene()};
        if (solvedAllPuzzlesActions != null) {
            foreach (Component action in solvedAllPuzzlesActions)
                if (action is IAction) {
                    _actions.Add((IAction)action);
                }
        }

        if (_actions.Count == 0)
            _actions.Add(this.gameObject.AddComponent<LoadNextScene>());


        DontDestroyOnLoad(this);
    }
}
