using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : MonoBehaviour
{
    public bool _isHolding = false;//change to private
    
    public bool IsHolding() {
            return _isHolding;
    }

    public void flip(bool value) {
        _isHolding = value;
    }
}
