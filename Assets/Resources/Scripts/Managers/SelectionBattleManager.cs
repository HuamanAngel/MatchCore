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
    private int[,] _gridTileMap;
    public static SelectionBattleManager _instance;
    public int SizeForTile { get => sizeForTile; set => sizeForTile = value; }
    public int[,] GridTileMap { get => _gridTileMap; }

    // Start is called before the first frame update
    public static SelectionBattleManager GetInstance()
    {
        return _instance;
    }
    enum OptionCreationMap
    {
        POSITION_HERO,
        POSITION_ACTIVE_TILE,
        POSITION_ENEMIE,
        POSITION_ENEMIES_BOSS,
        POSITION_NEXT_LVL
    }

    private void Awake()
    {
        _instance = this;
        _terrainWidth = (int)getTerrain.terrainData.size.x;
        _terrainLength = (int)getTerrain.terrainData.size.z;
        // _gridTileMap = new int[(int)_terrainWidth / sizeForTile, (int)_terrainLength / sizeForTile];
        // _gridTileMap = new int[,]{
        //     {1,1,0,0},
        //     {1,0,0,0},
        //     {1,1,0,0},
        //     {1,0,0,0},
        // };
        _gridTileMap = new int[,]{
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0}
        };

    }
    void Start()
    {
        // CheckQuantityTerrainTiles();
        // VisualizationTerrainTiles();
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
                _gridTileMap[i, j] = 0;
                // Debug.Log(i + " : " + j);
            }
        }
    }

    public void CreateRandomConnections()
    {
        Vector2 toTarget = new Vector2(Random.Range(0, _gridTileMap.GetLength(0)), Random.Range(0, _gridTileMap.GetLength(1)));
        for (int i = 0; i < _gridTileMap.GetLength(0); i++)
        {
            for (int j = 0; j < _gridTileMap.GetLength(1); j++)
            {
                _gridTileMap[i, j] = 0;
                // Debug.Log(i + " : " + j);
            }
        }
    }


    public void VisualizationTerrainTiles()
    {
        string valuesInRow = "";
        for (int i = 0; i < _gridTileMap.GetLength(0); i++)
        {
            for (int j = 0; j < _gridTileMap.GetLength(1); j++)
            {
                // Debug.Log("Element " + i + ":" + j + " is " + _gridTileMap[i, j]);
                valuesInRow = valuesInRow + _gridTileMap[i, j].ToString() + " ";
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
