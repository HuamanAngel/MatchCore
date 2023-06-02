using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class HeroInMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabArrow;
    public static HeroInMovement _instance;
    private List<GameObject> _allArrows;
    private DirectionMove.OptionMovements _directionMovement;
    private float _speedMove = 3.0f;
    private int _quantityKeyBasic = 2;
    private int _quantityKeyMedium = 3;
    private int _quantityKeyBig = 4;
    private int _quantityNextLvl = 0;
    private Rigidbody _rb;
    public TMP_Text textQuantityMoves;
    private bool _isMovement = false;
    private int _quantityMovementsInScene = 12;
    public GameObject screenGameOver;
    public GameObject screenVictory;
    public Coroutine coroutineMovement;
    public PostProcessVolume transitionEffectGameObject;
    private bool _avaibleNextMovement = false;
    public GameObject prefabToBattle;
    private float pivotTransformArrow = 2f;

    public DirectionMove.OptionMovements DirectionMovement { get => _directionMovement; set => _directionMovement = value; }
    public bool IsMovement { get => _isMovement; }
    public TMP_Text tmpKeyBasic;
    public TMP_Text tmpKeyMedium;
    public TMP_Text tmpKeyBig;
    public TMP_Text tmpKeyNextLvl;
    public int QuantityKeyBasic { get => _quantityKeyBasic; set => _quantityKeyBasic = value; }
    public int QuantityKeyMedium { get => _quantityKeyMedium; set => _quantityKeyMedium = value; }
    public int QuantityKeyBig { get => _quantityKeyBig; set => _quantityKeyBig = value; }
    public int QuantityKeyNextLvl { get => _quantityNextLvl; set => _quantityNextLvl = value; }

    public static HeroInMovement GetInstance()
    {
        return _instance;
    }
    private void Awake()
    {
        _instance = this;
        _rb = GetComponent<Rigidbody>();
        _allArrows = new List<GameObject>();
    }

    void Start()
    {
        // Set quantity movement
        if (UserController.GetInstance().StateInBattle.QuantityMovementAvaible != 0)
        {
            _quantityMovementsInScene = UserController.GetInstance().StateInBattle.QuantityMovementAvaible;
        }
        if (UserController.GetInstance().PositionInitialPoint != Vector3.zero)
        {
            // Set position hero and camera the first time
            transform.position = new Vector3(UserController.GetInstance().PositionInitialPoint.x, this.transform.position.y, UserController.GetInstance().PositionInitialPoint.z);
            Camera.main.transform.position = new Vector3(UserController.GetInstance().PositionInitialPoint.x, Camera.main.transform.position.y, UserController.GetInstance().PositionInitialPoint.z);
        }
        SetInformationInCanvas();
        if (UserController.GetInstance().StateInBattle.CurrentPosition != Vector3.zero)
        {
            // Set position hero and camera every time we back to map
            transform.position = UserController.GetInstance().StateInBattle.CurrentPosition;
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        }
        SetQuantityMovements();
        if (UserController.GetInstance().StateInBattle.IsDeadCharacter)
        {
            _avaibleNextMovement = true;
        }
        else
        {
            CreateArrowDirection();
        }
    }
    IEnumerator SendGraveryEnemyAndCreateArrowDirection()
    {
        List<GameObject> goEnemiesCollision = new List<GameObject>();
        // List<Charac> allCharactersEnemiesCollision = new List<Charac>();
        Vector3[] directionRayCast = new Vector3[1];
        // Debug.Log("Direction of enemy : "+ UserController.GetInstance().StateInBattle.DirectionEnemyTakeIt);
        switch (UserController.GetInstance().StateInBattle.DirectionEnemyTakeIt)
        {
            case DirectionMove.OptionMovements.UP:
                directionRayCast[0] = Vector3.forward * 3;
                break;
            case DirectionMove.OptionMovements.BOTTOM:
                directionRayCast[0] = Vector3.back * 3;
                break;
            case DirectionMove.OptionMovements.RIGHT:
                directionRayCast[0] = Vector3.right * 3;
                break;
            case DirectionMove.OptionMovements.LEFT:
                directionRayCast[0] = Vector3.left * 3;
                break;
        }

        yield return new WaitUntil(() => UserController.GetInstance().StateInBattle.EndCreatedEnemiesAndTreasures());

        if (CheckIfEnemieExistInDirection(directionRayCast, out goEnemiesCollision))
        {
            // Debug.Log("La condificion es true");
            goEnemiesCollision[0].GetComponent<EnemyInMovement>().DieThisEnemy();
            goEnemiesCollision[0].GetComponent<EnemyInMovement>().IsALive = false;
            StartCoroutine(WaitForFinishAnimationDeadEnemy(goEnemiesCollision[0].GetComponent<EnemyInMovement>().IsEnemyFinalOfMap));
        }
        else
        {
            // Debug.Log("La condicion es false : ");
        }
    }
    IEnumerator WaitForFinishAnimationDeadEnemy(bool isEnemyFinal)
    {
        // Debug.Log("Waiting for dead enemy");
        while (UserController.GetInstance().StateInBattle.IsDeadCharacter)
        {
            yield return null;
        }
        if (isEnemyFinal)
        {
            screenVictory.SetActive(true);
        }
        else
        {
            if (UserController.GetInstance().StateInBattle.InterrupteMovement)
            {
                // Debug.Log("Retomo el movimiento");
                StartCoroutine(DoMoving(UserController.GetInstance().StateInBattle.TargetPositionInterrupted));
            }
            else
            {
                UserController.GetInstance().StateInBattle.InterrupteMovement = false;
            }

            yield return new WaitUntil(() => UserController.GetInstance().StateInBattle.InterrupteMovement == false);
            // Debug.Log("Ok, already finish the movmenet retake");
            CreateArrowDirection();
            // Debug.Log(" Ok, i was create arrow directions");
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_avaibleNextMovement)
        {
            StartCoroutine(SendGraveryEnemyAndCreateArrowDirection());
            _avaibleNextMovement = false;
        }

        if (UserController.GetInstance().ReturnToPreviousMap)
        {
            GameObject goPoint = GetPointInteractiveOverHere();
            if (goPoint != null)
            {
                goPoint.GetComponent<PointInteractive>().HeroIsHere = true;
            }
            UserController.GetInstance().ReturnToPreviousMap = false;
        }
    }

    public void CreateArrowDirection()
    {
        Vector3 positionToArrow = Vector3.zero;
        // Debug.DrawRay(transform.position, Vector3.down, Color.green, 30, false);
        GameObject goPoint = GetPointInteractiveOverHere();
        if (goPoint != null)
        {

            // Check if is final point
            if (goPoint.GetComponent<PointInteractive>().isFinalPoint)
            {
                SceneController.ToMapSelectionLvl();
            }
            // Recheck Movements in point
            // goPoint.GetComponent<PointInteractive>().RecheckDirectionMovements();

            List<DirectionMove.OptionMovements> movementsAvaibles;
            movementsAvaibles = goPoint.GetComponent<PointInteractive>().DirectionAvaibleMovement;

            foreach (DirectionMove.OptionMovements theMovement in movementsAvaibles)
            {
                switch (theMovement)
                {
                    case DirectionMove.OptionMovements.UP:
                        positionToArrow = new Vector3(transform.position.x, transform.position.y, transform.position.z + pivotTransformArrow);
                        CreateOneArrowDirection(positionToArrow, DirectionMove.OptionMovements.UP);
                        break;
                    case DirectionMove.OptionMovements.BOTTOM:
                        positionToArrow = new Vector3(transform.position.x, transform.position.y, transform.position.z - pivotTransformArrow);
                        CreateOneArrowDirection(positionToArrow, DirectionMove.OptionMovements.BOTTOM);
                        break;
                    case DirectionMove.OptionMovements.RIGHT:
                        positionToArrow = new Vector3(transform.position.x + pivotTransformArrow, transform.position.y, transform.position.z);
                        CreateOneArrowDirection(positionToArrow, DirectionMove.OptionMovements.RIGHT);
                        break;
                    case DirectionMove.OptionMovements.LEFT:
                        positionToArrow = new Vector3(transform.position.x - pivotTransformArrow, transform.position.y, transform.position.z);
                        CreateOneArrowDirection(positionToArrow, DirectionMove.OptionMovements.LEFT);
                        break;
                }
            }

        }
    }
    private void CreateOneArrowDirection(Vector3 positionObject, DirectionMove.OptionMovements directionMovement)
    {
        GameObject arrow = Instantiate(prefabArrow);
        arrow.transform.position = positionObject;

        arrow.GetComponent<DirectionMove>().TypeArrow = directionMovement;

        arrow.GetComponent<DirectionMove>().RotateByTypeArrow();

        _allArrows.Add(arrow);
    }
    public void ClearAllArrows()
    {
        foreach (GameObject arrow in _allArrows)
        {
            Destroy(arrow);
        }
        _allArrows.Clear();
    }

    public void ChangeDirectionMovement(DirectionMove.OptionMovements newMovement)
    {
        _directionMovement = newMovement;
        ClearAllArrows();
        MovementToTarget();
    }

    public void MovementToTarget()
    {
        float stepTarget = 12.0f;
        Vector3 targetPosition = transform.position;
        switch (_directionMovement)
        {
            case DirectionMove.OptionMovements.UP:
                targetPosition = targetPosition + new Vector3(0, 0, stepTarget);
                break;
            case DirectionMove.OptionMovements.BOTTOM:
                targetPosition = targetPosition + new Vector3(0, 0, -stepTarget);
                break;
            case DirectionMove.OptionMovements.RIGHT:
                targetPosition = targetPosition + new Vector3(stepTarget, 0, 0);
                break;
            case DirectionMove.OptionMovements.LEFT:
                targetPosition = targetPosition + new Vector3(-stepTarget, 0, 0);
                break;
        }
        DecrementQuantityMovement();
        SetQuantityMovements();
        UserController.GetInstance().StateInBattle.TargetPositionInterrupted = targetPosition;
        coroutineMovement = StartCoroutine(DoMoving(targetPosition));
    }
    public IEnumerator DoMoving(Vector3 target)
    {
        _isMovement = true;
        SetDataInPointInteractive();
        while (Vector3.Distance(target, transform.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speedMove * Time.deltaTime);
            yield return null;
        }


        // Movement to point's center
        Vector3 aditionalMovement = MovementAditionalToCenterPoint();
        aditionalMovement.y = transform.position.y;
        if (aditionalMovement != Vector3.zero)
        {
            while (Vector3.Distance(aditionalMovement, transform.position) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, aditionalMovement, _speedMove * Time.deltaTime);
                yield return null;
            }
        }
        transform.position = aditionalMovement;
        UserController.GetInstance().StateInBattle.InterrupteMovement = false;
        // StartBattle
        CheckIfCanInitBattle();
    }

    public void CheckIfCanInitBattle()
    {
        _isMovement = false;
        Vector3[] directionRayCast = new Vector3[] { Vector3.forward * 3, Vector3.back * 3, Vector3.right * 3, Vector3.left * 3 };
        List<GameObject> goEnemiesCollision = new List<GameObject>();
        if (!CheckIfEnemieExistInDirection(directionRayCast, out goEnemiesCollision))
        {
            CreateArrowDirection();
            CheckIfGameOver();
        }
        else
        {

            List<Charac> allCharactersEnemiesCollision = new List<Charac>();
            allCharactersEnemiesCollision = goEnemiesCollision[0].GetComponent<EnemyInMovement>().TheCharacters;
            CreateIconAfterBattle(goEnemiesCollision[0].GetComponent<EnemyInMovement>().DirectionBelongToPoint);
            StartCoroutine(TransitionToBattle(allCharactersEnemiesCollision));

        }
    }

    public void SetQuantityMovements()
    {
        // textQuantityMoves.text = "" + _quantityMovementsInScene;
        SetInformationInCanvas();
    }

    public void DecrementQuantityMovement()
    {
        _quantityMovementsInScene -= 1;
    }
    public void CheckIfGameOver()
    {
        if (_quantityMovementsInScene <= 0)
        {
            screenGameOver.SetActive(true);
        }
    }
    public bool CheckIfEnemieExistInDirection(Vector3[] directionRaycast, out List<GameObject> _enemiesCollision)
    {
        RaycastHit objectHit;
        _enemiesCollision = new List<GameObject>();
        for (int i = 0; i < directionRaycast.Length; i++)
        {
            Debug.DrawRay(transform.position, directionRaycast[i], Color.green, 5, false);
            // Debug.Log("Aca antes de checkar la collision :::::::::::::::::::::::::: ");
            // Debug.Log("aca la direccion : "+ directionRaycast[i]);
            // Debug.Log("Reusltado de raycast : " + Physics.Raycast(transform.position, directionRaycast[i], out objectHit, 5.0f));
            if (Physics.Raycast(transform.position, directionRaycast[i], out objectHit, 5.0f))
            {
                // Debug.Log("Collision with : " + objectHit.collider.transform.gameObject.tag);
                if (objectHit.collider.transform.gameObject.tag == "Enemy")
                {
                    // Debug.Log("Detect enemy in front : " + objectHit.collider.transform.gameObject.name);
                    // Debug.Log("Tranformation : " + objectHit.collider.transform.gameObject.transform.position);
                    //  Debug.DrawLine(transform.position, directionRaycast[i], Color.green, 2.5f);
                    // Debug.DrawRay(transform.position, directionRaycast[i], Color.green, 30, false);
                    if (i == 0)
                    {
                        UserController.GetInstance().StateInBattle.DirectionEnemyTakeIt = DirectionMove.OptionMovements.UP;
                    }
                    else if (i == 1)
                    {
                        UserController.GetInstance().StateInBattle.DirectionEnemyTakeIt = DirectionMove.OptionMovements.BOTTOM;
                    }
                    else if (i == 2)
                    {
                        UserController.GetInstance().StateInBattle.DirectionEnemyTakeIt = DirectionMove.OptionMovements.RIGHT;
                    }
                    else if (i == 3)
                    {
                        UserController.GetInstance().StateInBattle.DirectionEnemyTakeIt = DirectionMove.OptionMovements.LEFT;
                    }
                    // DirectionEnemyTakeIt
                    _enemiesCollision.Add(objectHit.collider.transform.gameObject);
                    return true;
                }
            }

        }
        return false;
    }

    public Vector3 MovementAditionalToCenterPoint()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, Vector3.down, out objectHit))
        {
            if (objectHit.collider.transform.gameObject.tag == "Point")
            {
                // Debug.Log("Colocando este como hero is here true");
                objectHit.collider.transform.gameObject.GetComponent<PointInteractive>().HeroIsHere = true;
                return new Vector3(objectHit.collider.transform.position.x, 0, objectHit.collider.transform.position.z);
            }

        }
        return Vector3.zero;
    }

    public IEnumerator TransitionToBattle(List<Charac> allCharactersEnemiesCollision)
    {
        DepthOfField dof;
        Bloom b;
        AutoExposure ae;

        transitionEffectGameObject.profile.TryGetSettings(out b);
        transitionEffectGameObject.profile.TryGetSettings(out dof);
        transitionEffectGameObject.profile.TryGetSettings(out ae);

        float initialValueFocalLength = 100;
        float endValueFocalLength = 300;

        // float initialBloomIntesity = 1;
        float endBloomIntensity = 40.0f;
        b.diffusion.value = 4;

        // float initialValueMinEv = 0;
        float endValueMinEv = 6;

        float initTransitionBloom = endValueFocalLength - endBloomIntensity;
        float initTransitionAutoExposure = endValueFocalLength - endValueMinEv;

        while (dof.focalLength.value < endValueFocalLength)
        {
            dof.focalLength.value = initialValueFocalLength;
            if (initTransitionBloom <= initialValueFocalLength)
            {
                b.intensity.value += 3;
            }
            if (initTransitionAutoExposure <= initialValueFocalLength)
            {
                ae.minLuminance.value += 3;
            }

            initialValueFocalLength += 3;
            yield return new WaitForSeconds(0.0001f);
        }
        // SetDataToSceneBattle();

        // Set data for scene battle
        UserController.GetInstance().StateInBattle.CurrentPosition = transform.position;
        UserController.GetInstance().StateInBattle.AllEnemiesForBattleCurrent = allCharactersEnemiesCollision;
        UserController.GetInstance().userEnemy.CharInCombat = allCharactersEnemiesCollision;

        // UserController.GetInstance().StateInBattle.AllEnemiesInMapMovement
        UserController.GetInstance().StateInBattle.QuantityMovementAvaible = _quantityMovementsInScene;
        // UserController.GetInstance().user.CharInCombat = 
        // UserController.GetInstance().
        UserController.GetInstance().StateInBattle.CounterQuantityElements = 0;
        SceneController.ToBattle();

    }
    public void SetDataInPointInteractive()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, Vector3.down, out objectHit))
        {
            if (objectHit.collider.transform.gameObject.tag == "Point")
            {
                objectHit.collider.transform.gameObject.GetComponent<PointInteractive>().HeroIsHere = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log("exist collision");
        if (other.collider.transform.gameObject.tag == "Enemy")
        {
            // Debug.Log("Collision with enemy");
            if (coroutineMovement != null)
            {
                StopCoroutine(coroutineMovement);
                UserController.GetInstance().StateInBattle.InterrupteMovement = true;
                CheckIfCanInitBattle();
            }
        }
        if (other.collider.transform.gameObject.tag == "ObstacleDestroy")
        {
            // Debug.Log("Collision with sphere conave");
            Destroy(other.collider.transform.gameObject);
        }

    }

    public bool ConsumeKey(int quantityToConsumed, ElementsInteractuable.OptionObstacle typeObstacle)
    {
        switch (typeObstacle)
        {
            case ElementsInteractuable.OptionObstacle.FENCE_BASIC:
                if (_quantityKeyBasic >= quantityToConsumed)
                {
                    _quantityKeyBasic -= quantityToConsumed;
                    SetInformationInCanvas();
                    return true;
                }
                else
                {
                    // Debug.Log("No hay suficiente llaves");
                    return false;
                }
            case ElementsInteractuable.OptionObstacle.FENCE_MEDIUM:
                if (_quantityKeyMedium >= quantityToConsumed)
                {
                    _quantityKeyMedium -= quantityToConsumed;
                    SetInformationInCanvas();
                    return true;
                }
                else
                {
                    // Debug.Log("No hay suficiente llaves");
                    return false;
                }

            case ElementsInteractuable.OptionObstacle.FENCE_BIG:
                if (_quantityKeyBig >= quantityToConsumed)
                {
                    _quantityKeyBig -= quantityToConsumed;
                    SetInformationInCanvas();
                    return true;
                }
                else
                {
                    // Debug.Log("No hay suficiente llaves");
                    return false;
                }
            case ElementsInteractuable.OptionObstacle.FENCE_GRAND_TRANSITION:
                if (_quantityNextLvl >= quantityToConsumed)
                {
                    _quantityNextLvl -= quantityToConsumed;
                    SetInformationInCanvas();
                    return true;
                }
                else
                {
                    // Debug.Log("No hay suficiente llaves");
                    return false;
                }

            default:
                Debug.Log("Tipo de obstaculo no identificado");
                return false;
        }

    }
    public void SetInformationInCanvas()
    {
        tmpKeyBasic.text = "x" + _quantityKeyBasic;
        tmpKeyMedium.text = "x" + _quantityKeyMedium;
        tmpKeyBig.text = "x" + _quantityKeyBig;
        tmpKeyNextLvl.text = "x" + _quantityNextLvl;
        textQuantityMoves.text = "" + _quantityMovementsInScene;
    }

    public void TryCreationArrowDirection()
    {
        // CheckBrigdeAllDirections
        ClearAllArrows();
        // Debug.Log("Termine de limpiar");

        GameObject goPoint = GetPointInteractiveOverHere();
        if (goPoint != null)
        {
            // Recheck Movements in point
            goPoint.GetComponent<PointInteractive>().RecheckDirectionMovements();
        }
        CreateArrowDirection();
        // RecheckDirectionMovements
        UserController.GetInstance().ReturnToPreviousMap = false;
        // Debug.Log("Termine de revisar los brigde");
    }
    public void ModifyValuesFromHero(int addKeyBasic = 0, int addKeyMedium = 0, int addKeyBig = 0, int addKeyNextLvl = 0, int addMovement = 0)
    {
        _quantityKeyBasic += addKeyBasic;
        _quantityKeyMedium += addKeyMedium;
        _quantityKeyBig += addKeyBig;
        _quantityNextLvl += addKeyNextLvl;
        _quantityMovementsInScene += addMovement;
        SetInformationInCanvas();
    }
    public void CreateIconAfterBattle(DirectionMove.OptionMovements directionCreate)
    {
        GameObject twoSword = Instantiate(prefabToBattle);
        Vector3 positionToArrow = Vector3.zero;
        switch (directionCreate)
        {
            case DirectionMove.OptionMovements.UP:
                positionToArrow = new Vector3(transform.position.x, transform.position.y, transform.position.z + pivotTransformArrow / 2);
                twoSword.transform.position = positionToArrow;
                transform.Rotate(0, 0, 0);
                break;
            case DirectionMove.OptionMovements.BOTTOM:
                positionToArrow = new Vector3(transform.position.x, transform.position.y, transform.position.z - pivotTransformArrow / 2);
                twoSword.transform.position = positionToArrow;
                transform.Rotate(0, 0, -180);
                break;
            case DirectionMove.OptionMovements.RIGHT:
                positionToArrow = new Vector3(transform.position.x + pivotTransformArrow / 2, transform.position.y, transform.position.z);
                twoSword.transform.position = positionToArrow;
                transform.Rotate(0, 0, 90);
                break;
            case DirectionMove.OptionMovements.LEFT:
                positionToArrow = new Vector3(transform.position.x - pivotTransformArrow / 2, transform.position.y, transform.position.z);
                twoSword.transform.position = positionToArrow;
                transform.Rotate(0, 0, -90);
                break;
        }
    }

    public GameObject GetPointInteractiveOverHere()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, Vector3.down, out objectHit))
        {
            if (objectHit.collider.transform.gameObject.tag == "Point")
            {
                // objectHit.collider.transform.gameObject.GetComponent<PointInteractive>().HeroIsHere = true;
                return objectHit.collider.transform.gameObject;
            }
            return null;
        }
        else
        {
            return null;
        }
    }
}
