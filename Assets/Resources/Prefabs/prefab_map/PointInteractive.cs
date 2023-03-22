using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInteractive : MonoBehaviour
{
    private int posX;
    private int posY;
    public int PostX { get => posX; set => posX = value; }
    public int PostY { get => posY; set => posY = value; }
    private List<DirectionMove.OptionMovements> _directionAvaibleMovement;
    public List<DirectionMove.OptionMovements> DirectionAvaibleMovement { get => _directionAvaibleMovement; }
    private void Awake() {
        
    }
    private void OnEnable() {
        // Debug.Log("vacaciones");
    }
    void Start()
    {
        Vector3 rayCastVector;
        CheckMapAndSetValuesInArray();
        _directionAvaibleMovement = new List<DirectionMove.OptionMovements>();

        rayCastVector = Vector3.forward * 4 - new Vector3(0, +1.0f, 0);
        if (CheckBrigdeAdjacent(rayCastVector))
        {
            _directionAvaibleMovement.Add(DirectionMove.OptionMovements.UP);
        }

        rayCastVector = Vector3.back * 4 - new Vector3(0, +1.0f, 0);
        if (CheckBrigdeAdjacent(rayCastVector))
        {
            _directionAvaibleMovement.Add(DirectionMove.OptionMovements.BOTTOM);
        }

        rayCastVector = Vector3.right * 4 - new Vector3(0, +1.0f, 0);
        if (CheckBrigdeAdjacent(rayCastVector))
        {
            _directionAvaibleMovement.Add(DirectionMove.OptionMovements.RIGHT);
        }

        rayCastVector = Vector3.left * 4 - new Vector3(0, +1.0f, 0);
        if (CheckBrigdeAdjacent(rayCastVector))
        {
            _directionAvaibleMovement.Add(DirectionMove.OptionMovements.LEFT);
        }
        // Debug.Log("aca direction : " + _directionAvaibleMovement);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CheckMapAndSetValuesInArray()
    {
        float rangeInPosX = transform.position.x / SelectionBattleManager.GetInstance().SizeForTile;
        float rangeInPosY = transform.position.z / SelectionBattleManager.GetInstance().SizeForTile;
        // 6,6 / 12  = 0.5  =>   0 < 0.5 < 1 (0,0)
        // 6,18 / 12 = 0.5,1.5 =>   0 < 0.5 < 1 , 1 < 1.5 < 2  (0,1)
        // 18,18 / 12 = 1.5 =>   1 < 1.5 < 2  (1,1)
        // posX = (int)Mathf.Floor(rangeInPosX);
        // posY = (int)Mathf.Floor(rangeInPosY);

        // Reverse Id
        posX = (int)Mathf.Floor(rangeInPosX);
        // posX = SelectionBattleManager.GetInstance().GridTileMap.GetLength(0) - 1 - (int)Mathf.Floor(rangeInPosX);
        posY = SelectionBattleManager.GetInstance().GridTileMap.GetLength(1) - 1 - (int)Mathf.Floor(rangeInPosY);
    }

    public bool CheckBrigdeAdjacent(Vector3 rayCastDirection)
    {
        RaycastHit objectHit;
        // Debug.DrawRay(transform.position, Vector3.forward * 4 - new Vector3(0, + 1.0f, 0), Color.red, 30, false);
        if (Physics.Raycast(transform.position, rayCastDirection, out objectHit))
        {
            if (objectHit.collider.transform.gameObject.tag == "Brigde")
            {
                return true;
                // Debug.Log(objectHit.collider.transform.gameObject.tag);
            }
        }
        return false;
    }
}
