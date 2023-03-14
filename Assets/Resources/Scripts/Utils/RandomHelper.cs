using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHelper{
    public RandomHelper(){}
    public int RandomInt(int min, int max){
        return Random.Range(min, max);
    }

    public static int RandomIntStatic(int min, int max)
    {
        return Random.Range(min, max);
    }
}

