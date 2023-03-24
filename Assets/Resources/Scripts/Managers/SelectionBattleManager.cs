using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Terrain getTerrain;
    private int sizeForTile = 12;
    private int _terrainWidth = 0;
    private int _terrainLength = 0;
    private SelectionBattleManager.OptionCreationMap[,] _gridTileMap;
    private GameObject[,] _allPointInteractive;
    public static SelectionBattleManager _instance;
    public int SizeForTile { get => sizeForTile; set => sizeForTile = value; }
    public SelectionBattleManager.OptionCreationMap[,] GridTileMap { get => _gridTileMap; set => _gridTileMap = value; }

    public List<GameObject> prefabEnemies;
    public List<GameObject> prefabTreasures;
    // Start is called before the first frame update
    public static SelectionBattleManager GetInstance()
    {
        return _instance;
    }
    public enum OptionCreationMap
    {
        POSITION_NOTHING,
        POSITION_HERO,
        POSITION_ACTIVE_TILE,
        POSITION_BRIGDE,
        POSITION_ENEMY,
        POSITION_ENEMY_BOSS,
        POSITION_TREASURE,
        POSITION_NEXT_LVL
    }

    private void Awake()
    {
        _instance = this;
        _terrainWidth = (int)getTerrain.terrainData.size.x;
        _terrainLength = (int)getTerrain.terrainData.size.z;
        sizeForTile = 12;
        int xSize = (int)_terrainWidth / sizeForTile;
        int ySize = (int)_terrainLength / sizeForTile;
        // _gridTileMap = new SelectionBattleManager.OptionCreationMap[xSize, ySize];
        _gridTileMap = new SelectionBattleManager.OptionCreationMap[xSize * 2 - 1, ySize * 2 - 1];
        // _gridTileMap = new int[,]{
        //     {1,1,0,0},
        //     {1,0,0,0},
        //     {1,1,0,0},
        //     {1,0,0,0},
        // };
        // _gridTileMap = new int[,]{
        //     {0,0,0,0,0,0,0},
        //     {0,0,0,0,0,0,0},
        //     {0,0,0,0,0,0,0},
        //     {0,0,0,0,0,0,0},
        //     {0,0,0,0,0,0,0},
        //     {0,0,0,0,0,0,0},
        //     {0,0,0,0,0,0,0}
        // };

    }
    void Start()
    {
        // CheckQuantityTerrainTiles();
        VisualizationTerrainTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckQuantityTerrainTiles()
    {
        // Debug.Log(_terrainWidth/sizeForTile + " : " + _terrainLength/sizeForTile);
        for (int i = 0; i < (int)_terrainWidth / sizeForTile; i++)
        {
            for (int j = 0; j < (int)_terrainLength / sizeForTile; j++)
            {
                _gridTileMap[i, j] = SelectionBattleManager.OptionCreationMap.POSITION_NOTHING;
                // Debug.Log(i + " : " + j);
            }
        }
    }

    // public void CreateRandomConnections()
    // {
    //     Vector2 toTarget = new Vector2(Random.Range(0, _gridTileMap.GetLength(0)), Random.Range(0, _gridTileMap.GetLength(1)));
    //     for (int i = 0; i < _gridTileMap.GetLength(0); i++)
    //     {
    //         for (int j = 0; j < _gridTileMap.GetLength(1); j++)
    //         {
    //             _gridTileMap[i, j] = 0;
    //             // Debug.Log(i + " : " + j);
    //         }
    //     }
    // }


    public void VisualizationTerrainTiles()
    {
        string valuesInRow = "";
        for (int j = 0; j < _gridTileMap.GetLength(1); j++)
        {
            for (int i = 0; i < _gridTileMap.GetLength(0); i++)
            {
                // Debug.Log("Element " + i + ":" + j + " is " + _gridTileMap[i, j]);
                valuesInRow = valuesInRow + (int)_gridTileMap[i, j] + " ";
                // Debug.Log(_gridTileMap[i, j] + " ");
            }
            // Debug.Log(valuesInRow);
            valuesInRow = "";
        }

    }

    public void SetNewValuesInMap(int posX, int posY)
    {
        // _gridTileMap[posX,posY];
    }
    public void CheckPossiblePath(int posX, int posY)
    {

    }
}
