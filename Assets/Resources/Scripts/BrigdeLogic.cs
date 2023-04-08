using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeLogic : MonoBehaviour
{
    public GameObject gameObjectOverHere;
    // public List<GameObject> pointWhatConnect;
    public List<GameObject> pointsInteractuableInCaseExistObstacle;
    // private void Awake() {
    //     pointWhatConnect = new List<GameObject>();
    // }
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
