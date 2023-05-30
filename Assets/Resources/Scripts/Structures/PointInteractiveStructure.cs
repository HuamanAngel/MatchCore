using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInteractiveStructure
{
    public string idElement;
    private List<DirectionMove.OptionMovements> _directionAvaibleMovement;
    private List<DirectionMove.OptionMovements> _sideAvaibles;
    private Dictionary<DirectionMove.OptionMovements,List<Charac>> _positionEnemies;
    private Dictionary<DirectionMove.OptionMovements,Vector3> _positionTreasures;
    public List<DirectionMove.OptionMovements> DirectionAvaibleMovement { get => _directionAvaibleMovement; set => _directionAvaibleMovement = value; }
    public List<DirectionMove.OptionMovements> SideAvaibles { get => _sideAvaibles; set => _sideAvaibles = value; }
    public Dictionary<DirectionMove.OptionMovements,List<Charac>> PositionEnemies { get => _positionEnemies; set => _positionEnemies = value; }
    public Dictionary<DirectionMove.OptionMovements,Vector3> PositionTreasures { get => _positionTreasures; set => _positionTreasures = value; }

    public PointInteractiveStructure()
    {
        _directionAvaibleMovement = new List<DirectionMove.OptionMovements>();
        _sideAvaibles = new List<DirectionMove.OptionMovements>();
        _positionEnemies = new Dictionary<DirectionMove.OptionMovements,List<Charac>>();
        _positionTreasures = new Dictionary<DirectionMove.OptionMovements,Vector3>();
    }


    public void Clear()
    {
        _directionAvaibleMovement.Clear();
        _sideAvaibles.Clear();
        _positionEnemies.Clear();
        _positionTreasures.Clear();
    }
}
