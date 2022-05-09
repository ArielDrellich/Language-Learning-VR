using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementPuzzlesSolved : MonoBehaviour, IAction
{
    public int incrementBy = 1;

    public void DoAction()
    {
        PuzzleManager.Increment(incrementBy);
    }
}
