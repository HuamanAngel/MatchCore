using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeLogic : MonoBehaviour
{
    public GameObject gameObjectOverHere;
    void Start()
    {

    }

    void Update()
    {

    }

    public bool TheHeroCanMovementOverHere()
    {
        if(gameObjectOverHere != null)
        {
            return gameObjectOverHere.GetComponent<ObstacleInMovement>().typeObstacleOverHere == ElementsInteractuable.OptionObstacle.NOTHING;
        }else{
            return true;
        }
    }
}
