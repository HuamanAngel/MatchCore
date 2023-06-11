using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInteractive : MonoBehaviour
{
    public string idElement;
    public bool isInitialPoint = false;
    public bool isFinalPoint = false;
    public int belongToNumberPositionMap = -1;
    [Header("Select directions no interactuables")]
    public List<DirectionMove.OptionMovements> directionsNotInteractuable;
    private int posX;
    private int posY;
    private Dictionary<DirectionMove.OptionMovements, GameObject> _enemiesAround;
    private Dictionary<DirectionMove.OptionMovements, GameObject> _treasuresAround;
    private bool _heroIsHere = false;
    private Dictionary<int, float> theListProbTreasure;
    private Dictionary<int, float> theListProbEnemies;
    public int PosX { get => posX; set => posX = value; }
    public int PosY { get => posY; set => posY = value; }
    private List<DirectionMove.OptionMovements> _directionAvaibleMovement;
    private List<DirectionMove.OptionMovements> _sideAvaibles;
    public List<DirectionMove.OptionMovements> DirectionAvaibleMovement { get => _directionAvaibleMovement; }
    public Dictionary<DirectionMove.OptionMovements, GameObject> TreasuresAround { get => _treasuresAround; set => _treasuresAround = value; }
    public bool HeroIsHere { get => _heroIsHere; set => _heroIsHere = value; }
    private void Awake()
    {
        // Treasure Probability
        theListProbTreasure = new Dictionary<int, float>();
        theListProbTreasure[1] = 90.0f;
        theListProbTreasure[2] = 8.0f;
        theListProbTreasure[3] = 1.5f;
        theListProbTreasure[4] = 0.5f;

        // Enemie Probability

        theListProbEnemies = new Dictionary<int, float>();
        theListProbEnemies[1] = 70.0f;
        theListProbEnemies[2] = 20.0f;
        theListProbEnemies[3] = 8.0f;
        theListProbEnemies[4] = 2.0f;

        _enemiesAround = new Dictionary<DirectionMove.OptionMovements, GameObject>();
        _treasuresAround = new Dictionary<DirectionMove.OptionMovements, GameObject>();
        // Directions movements avaible
        _directionAvaibleMovement = new List<DirectionMove.OptionMovements>();
        // Sides from this point interactive
        _sideAvaibles = new List<DirectionMove.OptionMovements>() { DirectionMove.OptionMovements.BOTTOM, DirectionMove.OptionMovements.UP, DirectionMove.OptionMovements.RIGHT, DirectionMove.OptionMovements.LEFT };

        CheckMapAndSetValuesInArray();
        if (UserController.GetInstance().NumberPositionMap == belongToNumberPositionMap)
        {

            // First time
            if (!UserController.GetInstance().StateInBattle.EnemiesInMap.ContainsKey(idElement))
            {
                UserController.GetInstance().StateInBattle.EnemiesInMap[idElement] = new PointInteractiveStructure();
                // Directions Movements Avaible Path
                CheckBrigdeAllDirections();

                // _sideAvaibles.AddRange(_directionAvaibleMovement);
                foreach (DirectionMove.OptionMovements theDirection in directionsNotInteractuable)
                {
                    switch (theDirection)
                    {
                        case DirectionMove.OptionMovements.UP:
                            _sideAvaibles.Remove(DirectionMove.OptionMovements.UP);
                            // _sideAvaibles.Find(DirectionMove.OptionMovements.UP);
                            break;
                        case DirectionMove.OptionMovements.BOTTOM:
                            _sideAvaibles.Remove(DirectionMove.OptionMovements.BOTTOM);
                            break;
                        case DirectionMove.OptionMovements.RIGHT:
                            _sideAvaibles.Remove(DirectionMove.OptionMovements.RIGHT);
                            break;
                        case DirectionMove.OptionMovements.LEFT:
                            _sideAvaibles.Remove(DirectionMove.OptionMovements.LEFT);
                            break;

                    }
                }
                if (!UserController.GetInstance().StartMap && isInitialPoint && UserController.GetInstance().NumberPositionMap == belongToNumberPositionMap)
                {
                    UserController.GetInstance().StartMap = true;
                    UserController.GetInstance().PositionInitialPoint = this.transform.position;
                    _heroIsHere = true;
                }
                else
                {
                    _heroIsHere = CheckIfHereIsHere();
                }

                if (!_heroIsHere)
                {
                    if (UserController.GetInstance().NumberPositionMap == belongToNumberPositionMap)
                    {
                        // CreateEnemieInMap();
                        // CreateTreasureInMap();
                    }
                }
                // Directiones Movement block way, just treasures 
                CheckIfElementsBlockedTheWay();
            }
            else
            {
                // To return from battle

                PointInteractiveStructure goPointInteractive = UserController.GetInstance().StateInBattle.EnemiesInMap[idElement];
                _directionAvaibleMovement = goPointInteractive.DirectionAvaibleMovement;
                _sideAvaibles = goPointInteractive.SideAvaibles;

                foreach (var enemiesPosition in goPointInteractive.PositionEnemies)
                {
                    CreateEnemyAround(isRecreate: true, enemiesPosition.Key, enemiesPosition.Value);
                }

                foreach (var treasurePosition in goPointInteractive.PositionTreasures)
                {
                    CreateTreasureAround(isRecreate: true, treasurePosition.Key);
                }

            }
            UserController.GetInstance().StateInBattle.EnemiesInMap[idElement].PositionEnemies.Clear();
            UserController.GetInstance().StateInBattle.EnemiesInMap[idElement].DirectionAvaibleMovement = _directionAvaibleMovement;
            UserController.GetInstance().StateInBattle.EnemiesInMap[idElement].SideAvaibles = _sideAvaibles;
            foreach (var enemyDirection in _enemiesAround)
            {
                // Debug.Log("/////////////Inicio/////////////");
                GameObject theEnemyBody = UtilitiesClass.FindAllChildWithTag(enemyDirection.Value, "Enemy")[0];
                UserController.GetInstance().StateInBattle.EnemiesInMap[idElement].PositionEnemies[enemyDirection.Key] = theEnemyBody.GetComponent<EnemyInMovement>().TheCharacters;
                // Debug.Log("/////////////Fin/////////////");
                // UserController.GetInstance().StateInBattle.EnemiesInMap[idElement].PositionEnemies[enemyDirection.Key] = enemyDirection.Value.transform.position;
            }

            foreach (var treasureDirection in _treasuresAround)
            {
                UserController.GetInstance().StateInBattle.EnemiesInMap[idElement].PositionTreasures[treasureDirection.Key] = treasureDirection.Value.transform.position;
            }
        }
    }

    private void Start()
    {

    }

    public bool CheckIfHereIsHere()
    {
        RaycastHit objectHit;
        Debug.DrawRay(transform.position, Vector3.up, Color.red, 30, false);
        if (Physics.Raycast(transform.position, Vector3.up, out objectHit))
        {
            if (objectHit.collider.transform.gameObject.tag == "Player")
            {
                // Debug.Log("Exist hero over here");
                return true;
            }
        }
        return false;
    }
    public void CreateEnemyAround(bool isRecreate = false, DirectionMove.OptionMovements directionSelectedToCreate = DirectionMove.OptionMovements.LEFT, List<Charac> charactList = default(List<Charac>))
    {

        GameObject enemyObject = Instantiate(SelectionBattleManager.GetInstance().prefabEnemies[0]);

        enemyObject.transform.parent = transform;
        if (!isRecreate)
        {
            directionSelectedToCreate = _sideAvaibles[Random.Range(0, _sideAvaibles.Count)];
            _sideAvaibles.Remove(directionSelectedToCreate);
        }
        enemyObject.transform.localPosition = PositionAroundThisPoint(directionSelectedToCreate);
        _enemiesAround[directionSelectedToCreate] = enemyObject;

        GameObject theEnemyBody = UtilitiesClass.FindAllChildWithTag(enemyObject, "Enemy")[0];
        theEnemyBody.GetComponent<EnemyInMovement>().IdElement = idElement + "" + directionSelectedToCreate;
        theEnemyBody.GetComponent<EnemyInMovement>().IdElementPointInteractive = idElement;
        theEnemyBody.GetComponent<EnemyInMovement>().DirectionBelongToPoint = directionSelectedToCreate;

        // Remove Mesh rendered for default
        theEnemyBody.GetComponent<MeshRenderer>().enabled = false;
        List<Charac> allEnemies = new List<Charac>();
        if (!isRecreate)
        {
            allEnemies = GameData.GetInstance().GetRandomEnemyByMap(1, "Animal", "Muy Comun", 1);
        }
        else
        {
            // Ojo
            allEnemies = charactList;
        }
        Charac theCharacterEnemyToShow = allEnemies[0];

        theEnemyBody.GetComponent<EnemyInMovement>().TheCharacters = allEnemies;

        GameObject theEnemyRealPrefab = Instantiate(theCharacterEnemyToShow.prefabCharInBattle);
        // Debug.Log("El preagab : " + theCharacterEnemyToShow.prefabCharInBattle);
        theEnemyRealPrefab.transform.SetParent(theEnemyBody.transform);
        theEnemyRealPrefab.transform.localPosition = new Vector3(0, -0.5f, 0);

        float factorToScale = 3.2f;
        theEnemyRealPrefab.transform.localScale = new Vector3(theEnemyRealPrefab.transform.localScale.x * factorToScale, theEnemyRealPrefab.transform.localScale.y * factorToScale, theEnemyRealPrefab.transform.localScale.z * factorToScale);
        Vector3 diferenceVector = new Vector3(this.transform.position.x, 0, this.transform.position.z) - new Vector3(theEnemyRealPrefab.transform.position.x, 0, theEnemyRealPrefab.transform.position.z);
        theEnemyRealPrefab.transform.rotation = Quaternion.LookRotation(diferenceVector, Vector3.up);

        // Disabled component from enemies prefab
        theEnemyRealPrefab.GetComponent<BoxCollider>().enabled = false;

    }
    public void CheckMapAndSetValuesInArray()
    {
        float rangeInPosX = transform.position.x / SelectionBattleManager.GetInstance().SizeForTile;
        float rangeInPosY = transform.position.z / SelectionBattleManager.GetInstance().SizeForTile;
        // 6,6 / 12  = 0.5  =>   0 < 0.5 < 1 (0,0)
        // 6,18 / 12 = 0.5,1.5 =>   0 < 0.5 < 1 , 1 < 1.5 < 2  (0,1)
        // 18,18 / 12 = 1.5 =>   1 < 1.5 < 2  (1,1)

        // Reverse Id
        posX = (int)Mathf.Floor(rangeInPosX);

        // posY = SelectionBattleManager.GetInstance().GridTileMap.GetLength(1) - 1 - (int)Mathf.Floor(rangeInPosY);
        // SelectionBattleManager.GetInstance().GridTileMap[posX, posY] = SelectionBattleManager.OptionCreationMap.POSITION_ACTIVE_TILE;
        // (SelectionBattleManager.GetInstance().GridTileMap.GetLength(1)+1)/2
        posY = (SelectionBattleManager.GetInstance().GridTileMap.GetLength(1) + 1) / 2 - 1 - (int)Mathf.Floor(rangeInPosY);
        // Send information to ManagerSelection
        SelectionBattleManager.GetInstance().GridTileMap[posX * 2, posY * 2] = SelectionBattleManager.OptionCreationMap.POSITION_ACTIVE_TILE;
        for (int i = 0; i < _directionAvaibleMovement.Count; i++)
        {
            switch (_directionAvaibleMovement[i])
            {
                case DirectionMove.OptionMovements.UP:
                    SelectionBattleManager.GetInstance().GridTileMap[posX * 2, posY * 2 - 1] = SelectionBattleManager.OptionCreationMap.POSITION_BRIGDE;
                    break;
                case DirectionMove.OptionMovements.BOTTOM:
                    SelectionBattleManager.GetInstance().GridTileMap[posX * 2, posY * 2 + 1] = SelectionBattleManager.OptionCreationMap.POSITION_BRIGDE;
                    break;
                case DirectionMove.OptionMovements.RIGHT:
                    SelectionBattleManager.GetInstance().GridTileMap[posX * 2 + 1, posY * 2] = SelectionBattleManager.OptionCreationMap.POSITION_BRIGDE;
                    break;
                case DirectionMove.OptionMovements.LEFT:
                    SelectionBattleManager.GetInstance().GridTileMap[posX * 2 - 1, posY * 2] = SelectionBattleManager.OptionCreationMap.POSITION_BRIGDE;
                    break;

            }
        }
        if (_directionAvaibleMovement.Count == 0)
        {
            SelectionBattleManager.GetInstance().GridTileMap[posX * 2, posY * 2] = SelectionBattleManager.OptionCreationMap.POSITION_NOTHING;
        }
        idElement = "" + posX + posY;
    }

    public bool CheckBrigdeAdjacent(Vector3 rayCastDirection)
    {
        RaycastHit objectHit;
        // Debug.DrawRay(transform.position, Vector3.forward * 4 - new Vector3(0, + 1.0f, 0), Color.red, 30, false);
        if (Physics.Raycast(transform.position, rayCastDirection, out objectHit))
        {
            if (objectHit.collider.transform.gameObject.tag == "Brigde")
            {
                // Check if brigde exist some fence over him
                // return false;
                // objectHit.collider.transform.gameObject.GetComponent<BrigdeLogic>().pointWhatConnect.Add(this.gameObject);                
                if (objectHit.collider.transform.gameObject.GetComponent<BrigdeLogic>().TheHeroCanMovementOverHere())
                {
                    return true;
                }
                else
                {
                    return false;
                }
                // Debug.Log(objectHit.collider.transform.gameObject.tag);
            }
        }
        return false;
    }

    public void CreateTreasureAround(bool isRecreate = false, DirectionMove.OptionMovements directionSelectedToCreate = DirectionMove.OptionMovements.LEFT)
    {
        GameObject enemyObject = Instantiate(SelectionBattleManager.GetInstance().prefabTreasures[0]);
        enemyObject.transform.parent = transform;
        if (!isRecreate)
        {
            directionSelectedToCreate = _sideAvaibles[Random.Range(0, _sideAvaibles.Count)];
            _sideAvaibles.Remove(directionSelectedToCreate);
            // Debug.Log("I create treasure in this direction : " + directionSelectedToCreate);

        }
        enemyObject.transform.localPosition = PositionAroundThisPoint(directionSelectedToCreate);
        enemyObject.transform.localPosition = new Vector3(enemyObject.transform.localPosition.x, 0.5f, enemyObject.transform.localPosition.z);
        // float factorToScale = 3.2f;
        // enemyObject.transform.localScale = new Vector3(enemyObject.transform.localScale.x * factorToScale, enemyObject.transform.localScale.y * factorToScale, enemyObject.transform.localScale.z * factorToScale);
        Vector3 diferenceVector = new Vector3(this.transform.position.x, 0, this.transform.position.z) - new Vector3(enemyObject.transform.position.x, 0, enemyObject.transform.position.z);
        enemyObject.transform.rotation = Quaternion.LookRotation(diferenceVector, Vector3.up);

        // Aca el codigo puede fallar, revisarlo
        // Debug.Log("Before the assign direction : " + directionSelectedToCreate);
        // Debug.Log("Check default assign direction : " + enemyObject.GetComponent<RewardInMovement>()._theDirection);
        enemyObject.GetComponent<RewardInMovement>()._theDirection = directionSelectedToCreate;
        // Debug.Log("Check the assign direction : " + enemyObject.GetComponent<RewardInMovement>()._theDirection);

        // UserController.GetInstance().StateInBattle.TotalElementsInMap++;
        // UserController.GetInstance().StateInBattle.CounterQuantityElements++;
        _treasuresAround[directionSelectedToCreate] = enemyObject;
    }

    public Vector3 PositionAroundThisPoint(DirectionMove.OptionMovements directionSelectedToCreate)
    {
        Vector3 thePosition = Vector3.zero;
        switch (directionSelectedToCreate)
        {
            case DirectionMove.OptionMovements.UP:
                thePosition = Vector3.zero + new Vector3(0.0f, 10.0f, 0.65f);
                break;
            case DirectionMove.OptionMovements.BOTTOM:
                thePosition = Vector3.zero + new Vector3(0.0f, 10.0f, -0.65f);
                break;
            case DirectionMove.OptionMovements.RIGHT:
                thePosition = Vector3.zero + new Vector3(0.6f, 10.0f, 0.0f);
                break;
            case DirectionMove.OptionMovements.LEFT:
                thePosition = Vector3.zero + new Vector3(-0.6f, 10.0f, 0.0f);
                break;

        }
        return thePosition;
    }
    public void CheckBrigdeAllDirections()
    {
        Vector3 rayCastVector;
        rayCastVector = Vector3.forward * 4 - new Vector3(0, +1.0f, 0);
        _directionAvaibleMovement.Clear();
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
    }
    public void CheckIfElementsBlockedTheWay()
    {

        foreach (var theDirection in _treasuresAround)
        {
            switch (theDirection.Key)
            {
                case DirectionMove.OptionMovements.UP:
                    _directionAvaibleMovement.Remove(DirectionMove.OptionMovements.UP);
                    break;
                case DirectionMove.OptionMovements.BOTTOM:
                    _directionAvaibleMovement.Remove(DirectionMove.OptionMovements.BOTTOM);
                    break;
                case DirectionMove.OptionMovements.RIGHT:
                    _directionAvaibleMovement.Remove(DirectionMove.OptionMovements.RIGHT);
                    break;
                case DirectionMove.OptionMovements.LEFT:
                    _directionAvaibleMovement.Remove(DirectionMove.OptionMovements.LEFT);
                    break;

            }
        }
    }

    public void RecheckDirectionMovements()
    {
        // Directions Movements Avaible Path
        CheckBrigdeAllDirections();
        // Debug.Log(" ///////// before check elementos blquesd //////");
        // foreach (DirectionMove.OptionMovements dir in _directionAvaibleMovement)
        // {
        //     Debug.Log("Direction : " + dir);

        // }
        // Debug.Log(" ///////// before end //////");
        // Directiones Movement block way, just treasures 
        CheckIfElementsBlockedTheWay();
        // Debug.Log("I going to recheck brigde");

        // Debug.Log(" ///////// After //////");
        // foreach (DirectionMove.OptionMovements dir in _directionAvaibleMovement)
        // {
        //     Debug.Log("Direction : " + dir);

        // }
        // Debug.Log(" ///////// After end //////");

    }

    public void RemoveTreasureByKey(DirectionMove.OptionMovements direction)
    {
        _treasuresAround.Remove(direction);
    }

    public void CreateTreasureInMap()
    {
        if (_directionAvaibleMovement.Count != 0)
        {
            int createEnemy = Random.Range(1, 101);

            // 10%
            if (createEnemy <= 15)
            {
                int quantityTreasureAroundThisPoint = RandomHelper.GetRandomValueByProbabilityAndValues(theListProbTreasure);
                for (int i = 0; i < quantityTreasureAroundThisPoint; i++)
                {
                    if (_sideAvaibles.Count > 0)
                    {
                        CreateTreasureAround();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

    }

    public void CreateEnemieInMap()
    {
        if (_directionAvaibleMovement.Count != 0)
        {
            int createEnemy = Random.Range(1, 101);

            // 10%
            if (createEnemy <= 70)
            {
                int quantityEnemiesAroundThisPoint = RandomHelper.GetRandomValueByProbabilityAndValues(theListProbEnemies);
                // Not working whith 2 or more enemies in same point
                // int quantityEnemiesAroundThisPoint = 1;
                for (int i = 0; i < quantityEnemiesAroundThisPoint; i++)
                {
                    if (_sideAvaibles.Count > 0)
                    {
                        CreateEnemyAround();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

    }

}
