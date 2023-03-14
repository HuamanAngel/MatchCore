using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteligence : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyController _enemyController;
    // private GridControlRe _gridController;
    private AttackGrid _attackGrid;
    private LogicGame _logicGame;
    private float myTime;
    private float _otherMyTime;
    private float timeToMovement = 1.5f;
    private float timeToAttack = 1.5f;
    private float timeToTransicionTurn = 6.0f;
    private float timeBetweenMovements = 0.5f;
    private bool[] _boolStart = new bool[] { false, false, false, false, false, false, false, false, false, false };
    private Vector3 _selectPosition;
    private List<Vector3> _positionOfTheMovementCell;
    private List<int> _pathExistCollision;
    private List<Vector3> _positionSpheresBlue;
    private List<Vector3> _positionSpheresRed;
    private List<Vector3> _positionSpheresYellow;
    private List<Vector3> _positionHero;
    private int numberOrden = -1;
    public int NumberOrden { get => numberOrden; set => numberOrden = value; }
    private bool inRangeToAttack;
    private bool canAttack = false;
    private bool canMovement = false;
    private Skill _skillInRangeAttack;
    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        _attackGrid = new AttackGrid();
        // _positionOfTheMovementCell = new Vector3[10];
        _positionOfTheMovementCell = new List<Vector3>();
        // If value : 
        // 0 : Collision with floor
        // 1 : Sphere blue; 2 : Sphere red; 3 : Sphere yellow
        // 4 : Other Item
        // 5 : Hero
        _pathExistCollision = new List<int>();
        _positionSpheresBlue = new List<Vector3>();
        _positionSpheresRed = new List<Vector3>();
        _positionSpheresYellow = new List<Vector3>();
        _positionHero = new List<Vector3>();
        inRangeToAttack = false;
    }
    void Start()
    {
        myTime = 0.0f;
        _otherMyTime = 0.0f;
        GameObject[] go = GameObject.FindGameObjectsWithTag("GridController");
        GameObject[] go2 = GameObject.FindGameObjectsWithTag("LogicGame");
        // _gridController = go[0].GetComponent<GridControlRe>();
        _logicGame = go2[0].GetComponent<LogicGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOrden == EnemyPlayerController.GetInstance().OrdenToEnemies)
        {
            if (_logicGame.WhatTurn == 2 && !_boolStart[0])
            {
                // Debug.Log("Ejecutando el sigueinte  number orden : " + numberOrden);
                _boolStart[0] = true;
                StartCoroutine(DoMoving());
            }
        }

    }

    // public void DrawThroughGrid(bool isMovement,AttackGrid.TypeOfAttack actionType, GameObject goToDraw)
    // {
    //     // First draw path

    //     Vector3 posCell = transform.position - new Vector3(0.5f, 0, 0.5f);
    //     int[,] movkGrid = _attackGrid.GetTypeGrid(actionType);
    //     _positionOfTheMovementCell.Clear();
    //     _pathExistCollision.Clear();
    //     _gridController.DrawPathGrid(isMovement,_attackGrid, movkGrid, _gridController.PathMap, goToDraw, posCell, true, _positionOfTheMovementCell, _pathExistCollision);
    // }
    public void CheckMyPathWhithoutDraw(Skill theSkill, GameObject goToDraw)
    {
        AttackGrid.TypeOfAttack actionType = theSkill.attackType;
        Vector3 posCell = transform.position - new Vector3(0.5f, 0, 0.5f);
        int[,] movkGrid = _attackGrid.GetTypeGrid(actionType);
        _positionOfTheMovementCell.Clear();
        _pathExistCollision.Clear();
        // _gridController.DrawPathGrid(false,_attackGrid, movkGrid, _gridController.PathMap, goToDraw, posCell, true, _positionOfTheMovementCell, _pathExistCollision, false);
        for (int i = 0; i < _pathExistCollision.Count; i++)
        {
            if (_pathExistCollision[i] == 5)
            {
                inRangeToAttack = true;
                _selectPosition = _positionOfTheMovementCell[i];
                _skillInRangeAttack = theSkill;
            }
        }
    }

    public void InitMovementGrid(int selection)
    {
        // _selectPosition = _gridController.InitMovement(transform.gameObject, _enemyController);
        _selectPosition = MovementToTarget(selection);
        canMovement = true;
    }

    // Types of movement in your range 
    public Vector3 MovementToTarget(int selection)
    {
        Vector3 toTarget = new Vector3();
        bool thereIs = false;
        switch (selection)
        {
            case 0:
                // Random move
                MovementForDefault();
                break;
            case 1:
                // To Sphere blue
                for (int i = 0; i < _pathExistCollision.Count; i++)
                {
                    if (_pathExistCollision[i] == 1)
                    {
                        thereIs = true;
                        toTarget = _positionOfTheMovementCell[i];
                        break;
                        // Debug.DrawRay(_positionOfTheMovementCell[i], Vector3.down, Color.blue, 30, false);
                    }
                    // Debug.Log(" Exist collision : " + _pathExistCollision[i]);
                }
                if (!thereIs)
                {
                    toTarget = MovementToAnySphere();
                    if (toTarget == Vector3.zero)
                    {
                        toTarget = MovementForDefault();
                    }

                }
                break;

            case 2:
                // To Sphere red
                for (int i = 0; i < _pathExistCollision.Count; i++)
                {
                    if (_pathExistCollision[i] == 2)
                    {
                        thereIs = true;
                        toTarget = _positionOfTheMovementCell[i];
                        break;
                        // Debug.DrawRay(_positionOfTheMovementCell[i], Vector3.down, Color.blue, 30, false);
                    }
                    // Debug.Log(" Exist collision : " + _pathExistCollision[i]);
                }
                if (!thereIs)
                {
                    toTarget = MovementToAnySphere();
                    if (toTarget == Vector3.zero)
                    {
                        toTarget = MovementForDefault();
                    }
                }
                break;
            case 3:
                // To Sphere yellow
                for (int i = 0; i < _pathExistCollision.Count; i++)
                {
                    if (_pathExistCollision[i] == 3)
                    {
                        thereIs = true;
                        toTarget = _positionOfTheMovementCell[i];
                        break;
                        // Debug.DrawRay(_positionOfTheMovementCell[i], Vector3.down, Color.blue, 30, false);
                    }
                    // Debug.Log(" Exist collision : " + _pathExistCollision[i]);
                }
                if (!thereIs)
                {
                    toTarget = MovementToAnySphere();
                    if (toTarget == Vector3.zero)
                    {
                        toTarget = MovementForDefault();
                    }

                }

                break;
            case 4:
                toTarget = MovementToAnySphere();
                if (toTarget == Vector3.zero)
                {
                    toTarget = MovementForDefault();
                }
                break;
        }
        return toTarget;
    }
    public Vector3 AttackToTarget(int selection)
    {
        Vector3 toTarget = new Vector3();
        bool thereIs = false;
        // Debug.Log(" OK 1");
        switch (selection)
        {
            case 1:
                // Position to hero
                for (int i = 0; i < _pathExistCollision.Count; i++)
                {
                    // Debug.Log("iteration : " + i);
                    if (_pathExistCollision[i] == 5)
                    {
                        thereIs = true;
                        toTarget = _positionOfTheMovementCell[i];
                        Debug.Log("Okey i have the position from the cellHero");
                        break;
                    }
                }
                if (!thereIs)
                {

                    toTarget = Vector3.zero;
                }
                break;
        }
        // Debug.Log(" OK 3");
        return toTarget;
    }

    public Vector3 MovementForDefault()
    {
        Vector3 toTarget = new Vector3();
        RandomHelper random = new RandomHelper();
        int randomValue = random.RandomInt(0, _positionOfTheMovementCell.Count);
        toTarget = _positionOfTheMovementCell[randomValue];

        return toTarget;
    }

    public Vector3 MovementToAnySphere()
    {
        bool exist = false;
        Vector3 toTarget = new Vector3();
        for (int i = 0; i < _pathExistCollision.Count; i++)
        {
            if (_pathExistCollision[i] == 2)
            {
                toTarget = _positionOfTheMovementCell[i];
                exist = true;
                break;
            }
            if (_pathExistCollision[i] == 1)
            {
                toTarget = _positionOfTheMovementCell[i];
                exist = true;
                break;
            }
            if (_pathExistCollision[i] == 3)
            {
                toTarget = _positionOfTheMovementCell[i];
                exist = true;
                break;
            }
        }
        if (exist)
        {
            return toTarget;

        }
        else
        {
            return Vector3.zero;
        }

    }
    public Vector3 velocity;
    public void ProcessMovementGrid()
    {
        // _gridController.MovementProcess(_enemyController, _selectPosition);
        // float speed = 1.0f;
        // float step = speed * Time.deltaTime;
        // if (Vector2.Distance(new Vector2(c1.transform.position.x, c1.transform.position.z), new Vector2(targetPos.x, targetPos.z)) > 0.1f)
        // {
        //     c1.transform.position = Vector3.MoveTowards(c1.transform.position, new Vector3(targetPos.x, c1.transform.position.y, targetPos.z), step);
        // }
        // else
        // {
        //     c1.SetDataGrid("SelectMovement", 0);
        //     c1.SetDataGrid("IsMoving", 0);
        // }

    }

    public void InitAttack()
    {
        // _selectPosition = _gridController.InitAttack(transform.gameObject, _enemyController);
        _selectPosition = AttackToTarget(1);
        _enemyController.SkillSelectedCurrent = _skillInRangeAttack;
        canAttack = true;
    }
    public void ProcessAttackGrid()
    {
        if (_selectPosition != Vector3.zero)
        {
            // _gridController.AttackProcess(_enemyController, _selectPosition, _gridController.EffectMap, _enemyController.SkillSelectedCurrent.id);
        }
    }
    public void AttackByThroughGrid()
    {

    }
    public void ReturnToOriginalState()
    {
        for (int i = 0; i < _boolStart.Length; i++)
        {
            _boolStart[i] = false;
        }
        myTime = 0.0f;
        _otherMyTime = 0.0f;
        _positionOfTheMovementCell.Clear();
        inRangeToAttack = false;
        canAttack = false;
        canMovement = false;
    }

    public void CheckAllFloor()
    {
        List<GameObject> go;
        // go = UtilitiesClass.FindAllChildByLayer(_gridController.FloorMap.gameObject, "Floor");
        Vector3 pivotValue = new Vector3(0, 0.5f, 0);
        // for (int i = 0; i < go.Count; i++)
        // {
        //     // Debug.DrawRay(go[i].transform.position + pivotValue, Vector3.up * 2, Color.green, 30);
        //     RaycastHit objectHit;
        //     if (Physics.Raycast(go[i].transform.position + pivotValue, Vector3.up * 2, out objectHit))
        //     {
        //         // Debug.Log("Collision mask : " + objectHit.collider.transform.gameObject.layer);
        //         if (objectHit.collider.transform.gameObject.layer != LayerMask.NameToLayer("Items"))
        //         {
        //             _positionHero.Add(objectHit.collider.transform.position);
        //         }
        //         if (objectHit.collider.transform.gameObject.layer != LayerMask.NameToLayer("CellHero"))
        //         {
        //             GameObject g1 = objectHit.collider.transform.gameObject;
        //             // Spheres.TypeOfSpheres type = g1.GetComponent<SphereItem>().typeSphere;
        //             // switch (type)
        //             // {
        //             //     case Spheres.TypeOfSpheres.SPHERE_BLUE:
        //             //         _positionSpheresBlue.Add(objectHit.collider.transform.position);
        //             //         break;
        //             //     case Spheres.TypeOfSpheres.SPHERE_RED:
        //             //         _positionSpheresRed.Add(objectHit.collider.transform.position);
        //             //         break;
        //             //     case Spheres.TypeOfSpheres.SPHERE_YELLOW:
        //             //         _positionSpheresYellow.Add(objectHit.collider.transform.position);
        //             //         break;
        //             // }
        //         }
        //     }
        // }
    }

    IEnumerator DoMoving()
    {
        bool stillMoving = false;
        // _gridController.ClearAllObjectInTilemap(_gridController.PathMap);
        _skillInRangeAttack = null;
        for (int i = 0; i < _enemyController.allSKill.Count; i++)
        {
            if (inRangeToAttack == false)
            {
                // CheckMyPathWhithoutDraw(_enemyController.allSKill[0], _gridController.PathObject);
            }
        }
        yield return new WaitForSeconds(timeToTransicionTurn);
        if (inRangeToAttack)
        {
            if (_enemyController.CheckifCanAttackSpheres(_skillInRangeAttack))
            {
                // Attack
                // DrawThroughGrid(false,_skillInRangeAttack.attackType, _gridController.AttackObject);
                yield return new WaitForSeconds(timeToAttack);

                // _gridController.ClearAllObjectInTilemap(_gridController.PathMap);
                yield return new WaitForSeconds(timeBetweenMovements);

                InitAttack();
                ProcessAttackGrid();
                yield return new WaitForSeconds(timeToAttack);
            }
            if (_enemyController.CheckIfCanMovementSpheres())
            {
                // Movement
                // DrawThroughGrid(true,_enemyController.moveType, _gridController.PathObject);
                yield return new WaitForSeconds(timeToMovement);

                // _gridController.ClearAllObjectInTilemap(_gridController.PathMap);
                yield return new WaitForSeconds(timeBetweenMovements);

                InitMovementGrid(4);
                // yield return StartCoroutine(_gridController.MovementProcessEnumerator(_enemyController, _selectPosition));
                yield return new WaitForSeconds(timeToMovement);

            }

        }
        else
        {
            if (_enemyController.CheckIfCanMovementSpheres())
            {
                // Movement
                // DrawThroughGrid(true,_enemyController.moveType, _gridController.PathObject);
                yield return new WaitForSeconds(timeToMovement);

                // _gridController.ClearAllObjectInTilemap(_gridController.PathMap);
                yield return new WaitForSeconds(timeBetweenMovements);

                InitMovementGrid(4);
                // yield return StartCoroutine(_gridController.MovementProcessEnumerator(_enemyController, _selectPosition));
                yield return new WaitForSeconds(timeToMovement);

            }

            if (_enemyController.CheckifCanAttackSpheres(_enemyController.allSKill[0]))
            {
                for (int i = 0; i < _enemyController.allSKill.Count; i++)
                {
                    if (inRangeToAttack == false)
                    {
                        // CheckMyPathWhithoutDraw(_enemyController.allSKill[i], _gridController.PathObject);
                    }
                }
                if (inRangeToAttack == true)
                {
                    // Attack
                    // DrawThroughGrid(false,_skillInRangeAttack.attackType, _gridController.AttackObject);
                    yield return new WaitForSeconds(timeToAttack);


                    // _gridController.ClearAllObjectInTilemap(_gridController.PathMap);
                    yield return new WaitForSeconds(timeBetweenMovements);

                    InitAttack();
                    ProcessAttackGrid();
                    yield return new WaitForSeconds(timeToAttack);

                }
            }
        }
        ReturnToOriginalState();
        EnemyPlayerController.GetInstance().ChangeControlToOtherEnemy();
        if(EnemyPlayerController.GetInstance().EndTurnAllEnemies)
        {
            _logicGame.NextTurn();
            EnemyPlayerController.GetInstance().EndTurnAllEnemies = false;
        }
    }
}
