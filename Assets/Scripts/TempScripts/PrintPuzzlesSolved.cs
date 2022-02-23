﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintPuzzlesSolved : MonoBehaviour, IClickable
{
    public void LookedAt(RaycastHit hit) {
        print("Solved "+PuzzlesSolvedCounter.GetSolvedPuzzles()+"/"+PuzzlesSolvedCounter.GetTotalPuzzles());
    }
}
