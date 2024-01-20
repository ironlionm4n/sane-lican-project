using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Utilities 
{
        public static bool CoinFlip()
        {
            return Random.Range(0, 2) == 0;
        }
}
