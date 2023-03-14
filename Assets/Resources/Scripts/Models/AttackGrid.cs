using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AttackGrid
{
    //Nomenclature : 0 <-- None  1 <-- Attack, 9 <-- Player

    public enum TypeOfAttack{
        GRID_CROSS,
        GRID_CROSS_L,

        GRID_DIAGONAL,
        GRID_SCATTER,
        GRID_RING,
        GRID_ROAR,
        GRID_METEOR
    }

    private int[,] _gridCross = new int[,] {
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,1,9,1,0},
        {0,0,1,0,0},
        {0,0,0,0,0},
    };

    private int[,] _gridCrossL = new int[,] {
        {0,0,1,0,0},
        {0,0,1,0,0},
        {1,1,9,1,1},
        {0,0,1,0,0},
        {0,0,1,0,0},
    };
    private int[,] _gridDiagonal = new int[,] {
        {1,0,0,0,1},
        {0,1,0,1,0},
        {0,0,9,0,0},
        {0,1,0,1,0},
        {1,0,0,0,1},
    };
    private int[,] _gridScaterred = new int[,] {
        {1,0,1,0,1},
        {0,0,0,0,0},
        {1,0,9,0,1},
        {0,0,0,0,0},
        {1,0,1,0,1},
    };
    private int[,] _gridRing = new int[,] {
        {0,0,0,0,0},
        {0,1,1,1,0},
        {0,1,9,1,0},
        {0,1,1,1,0},
        {0,0,0,0,0}
    };
    private int[,] _gridRoar = new int[,] {
        {0,1,1,1,0},
        {0,1,1,1,0},
        {0,0,1,0,0},
        {0,0,9,0,0},
        {0,0,0,0,0},
    };
    private int[,] _gridMeteor = new int[,] {
        {1,0,1,0,1},
        {0,0,0,0,0},
        {1,0,1,0,1},
        {0,0,0,0,0},
        {0,0,9,0,0},
    };

    public Vector3Int GetAttackGridPositionPlayer(int[,] attackGrid)
    {
        Vector3Int attackGridPosition = new Vector3Int(0, 0, 0);
        for (int i = 0; i < attackGrid.GetLength(0); i++)
        {
            for (int j = 0; j < attackGrid.GetLength(1); j++)
            {
                if (attackGrid[i, j] == 9)
                {
                    attackGridPosition = new Vector3Int(i, j, 0);
                }
            }
        }
        return attackGridPosition;
    }

    public int[,] GetTypeGrid(TypeOfAttack typeOfAttack)
    {
        switch (typeOfAttack)
        {
            case TypeOfAttack.GRID_CROSS:
                return _gridCross;
            case TypeOfAttack.GRID_CROSS_L:
                return _gridCrossL;
            case TypeOfAttack.GRID_DIAGONAL:
                return _gridDiagonal;
            case TypeOfAttack.GRID_SCATTER:
                return _gridScaterred;
            case TypeOfAttack.GRID_RING:
                return _gridRing;
            case TypeOfAttack.GRID_ROAR:
                return _gridRoar;
            case TypeOfAttack.GRID_METEOR:
                return _gridMeteor;
            default:
                return _gridCross;
        }
    }

}