using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHelper
{
    public RandomHelper() { }
    public int RandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }

    public static int RandomIntStatic(int min, int max)
    {
        return Random.Range(min, max);
    }

    public static int GetRandomValueByProbabilityAndValues(Dictionary<int, float> theProbabilities)
    {
        float weightTotal = 0.0f;
        foreach (var item in theProbabilities)
        {
            weightTotal += item.Value;
        }
        if (weightTotal != 100)
        {
            Debug.Log("The sum of the prob.  must be 100");
            return -1;
        }
        int createRandomValue = Random.Range(1, (int)weightTotal + 1);
        float rangeMin = 1;
        float rangeMax = 0;
        int valueSelected = -2;
        foreach (var item in theProbabilities)
        {
            rangeMax += item.Value;
            // Debug.Log("Values valid between " + rangeMin + " and " + rangeMax + " : Value give -> " + createRandomValue);
            if( createRandomValue > rangeMin && createRandomValue <= rangeMax )
            {
                valueSelected = item.Key;
                break;
            }
            rangeMin = rangeMax;
        }
        if (valueSelected == -2)
        {
            Debug.Log("Error in calculate value");
        }
        // Debug.Log("Value Selected " + valueSelected);
        return valueSelected;

    }
}

