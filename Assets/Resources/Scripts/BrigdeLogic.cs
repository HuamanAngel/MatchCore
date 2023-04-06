using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeLogic : MonoBehaviour
{
    public enum optionEmiesObstacle
    {
        NOTHING,
        FENCE_BASIC,
        FENCE_MEDIUM,
        FENCE_BIG
    }
    public optionEmiesObstacle typeObstacleOverHere;
    void Start()
    {

    }

    void Update()
    {

    }

    public bool TheHeroCanMovementOverHere()
    {
        return typeObstacleOverHere == optionEmiesObstacle.NOTHING;
    }

    public void SetNothingOverHere()
    {
        typeObstacleOverHere = optionEmiesObstacle.NOTHING;
    }
}
