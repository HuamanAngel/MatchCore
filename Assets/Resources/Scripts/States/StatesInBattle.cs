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
    // In Combat
    public int QuantityMovementAvaible { get => quantityMovementAvaible; set => quantityMovementAvaible = value; }
    public Vector3 CurrentPosition { get => currentPosition; set => currentPosition = value; }
    public List<Charac> AllEnemiesInMapMovement { get => allEnemiesInMapMovement; set => allEnemiesInMapMovement = value; }
    public List<Charac> AllEnemiesForBattleCurrent { get => allEnemiesForBattleCurrent; set => allEnemiesForBattleCurrent = value; }

    public StatesInBattle()
    {
        allEnemiesInMapMovement = new List<Charac>();
        allEnemiesForBattleCurrent = new List<Charac>();
        quantityMovementAvaible = 0;
        currentPosition = Vector3.zero;
    }
    // public StatesInBattle(int quantityMovementAvaible, Vector3 currentPosition)
    // {
    //     quantityMovementAvaible = this.quantityMovementAvaible;
    //     currentPosition = this.currentPosition;
    // }

    public void ResetStateInitial()
    {
        quantityMovementAvaible = 0;
        currentPosition = Vector3.zero;
        allEnemiesInMapMovement.Clear();
        allEnemiesForBattleCurrent.Clear();
    }

}
