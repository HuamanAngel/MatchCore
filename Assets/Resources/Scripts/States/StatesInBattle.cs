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
    public int QuantityMovementAvaible { get => quantityMovementAvaible; set => quantityMovementAvaible = value; }
    public Vector3 CurrentPosition { get => currentPosition; set => currentPosition = value; }
    public List<Charac> AllEnemiesInMapMovement { get => allEnemiesInMapMovement; set => allEnemiesInMapMovement = value; }
    public List<Charac> AllEnemiesForBattleCurrent { get => allEnemiesForBattleCurrent; set => allEnemiesForBattleCurrent = value; }
    public bool IsDeadCharacter { get => _isDeadCharacter; set => _isDeadCharacter = value; }
    public DirectionMove.OptionMovements DirectionEnemyTakeIt { get => _directionEnemyTakeIt; set => _directionEnemyTakeIt = value; }
    // In Combat

    public StatesInBattle()
    {
        _directionEnemyTakeIt = DirectionMove.OptionMovements.UP;
        allEnemiesInMapMovement = new List<Charac>();
        allEnemiesForBattleCurrent = new List<Charac>();
        quantityMovementAvaible = 0;
        currentPosition = Vector3.zero;
        IsDeadCharacter = false;
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
    }

}
