using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesInBattle
{
    // In Movement
    private int quantityMovementAvaible;
    private Vector3 currentPosition;
    private List<Charac> allEnemiesInMapMovement;
    private List<Charac> allEnemiesForBattleCurrent;
    private bool _isDeadCharacter;
    private DirectionMove.OptionMovements _directionEnemyTakeIt;
    // private List<GameObject> _enemiesInMap;
    private Dictionary<string, PointInteractiveStructure> _enemiesInMap;
    private bool _alreadySaveEnemies = false;
    public int QuantityMovementAvaible { get => quantityMovementAvaible; set => quantityMovementAvaible = value; }
    public Vector3 CurrentPosition { get => currentPosition; set => currentPosition = value; }
    public List<Charac> AllEnemiesInMapMovement { get => allEnemiesInMapMovement; set => allEnemiesInMapMovement = value; }
    public List<Charac> AllEnemiesForBattleCurrent { get => allEnemiesForBattleCurrent; set => allEnemiesForBattleCurrent = value; }
    public bool IsDeadCharacter { get => _isDeadCharacter; set => _isDeadCharacter = value; }
    public DirectionMove.OptionMovements DirectionEnemyTakeIt { get => _directionEnemyTakeIt; set => _directionEnemyTakeIt = value; }
    public Dictionary<string, PointInteractiveStructure> EnemiesInMap { get => _enemiesInMap; set => _enemiesInMap = value; }
    public bool AlreadySaveEnemies { get => _alreadySaveEnemies; set => _alreadySaveEnemies = value; }

    public PointInteractiveStructure pointStructure;

    // In Combat

    public StatesInBattle()
    {
        _directionEnemyTakeIt = DirectionMove.OptionMovements.UP;
        allEnemiesInMapMovement = new List<Charac>();
        allEnemiesForBattleCurrent = new List<Charac>();
        quantityMovementAvaible = 0;
        currentPosition = Vector3.zero;
        IsDeadCharacter = false;
        _enemiesInMap = new Dictionary<string, PointInteractiveStructure>();
        pointStructure = new PointInteractiveStructure();
    }
    // public StatesInBattle(int quantityMovementAvaible, Vector3 currentPosition)
    // {
    //     quantityMovementAvaible = this.quantityMovementAvaible;
    //     currentPosition = this.currentPosition;
    // }

    public void ResetStateInitial()
    {
        _directionEnemyTakeIt = DirectionMove.OptionMovements.UP;
        quantityMovementAvaible = 0;
        currentPosition = Vector3.zero;
        allEnemiesInMapMovement.Clear();
        allEnemiesForBattleCurrent.Clear();
        IsDeadCharacter = false;
        _enemiesInMap.Clear();
        pointStructure.Clear();
    }

}
