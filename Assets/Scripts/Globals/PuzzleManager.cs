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

    public static void Increment(int x = 1) {
        puzzlesSolved += x;
        
        if (puzzlesSolved == totalPuzzles) {
           PuzzleManager.ResetCounters();

            foreach (IAction action in _actions)
                action.DoAction();
        }
    }


    public static void SetActions(List<IAction> actions) {
        _actions = actions;
    }
    
    public static List<IAction> GetActions() {
        return _actions;
    }

    public static void ResetCounters() {
        totalPuzzles = 0;
        puzzlesSolved = 0;
    }

    void Start()
    {
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
