using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsInteractuable
{
    // Element what interrumped the movement in map
    public enum OptionObstacle
    {
        NOTHING,
        FENCE_BASIC,
        FENCE_MEDIUM,
        FENCE_BIG
    }
    public enum OptionReward
    {
        NOTHING,
        CHEST_BASIC,
        CHEST_MEDIUM,
        CHEST_BIG
    }
}
