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
    private int _quantityMovementsInScene = 4;
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
        SetInformationInCanvas();
        if (UserController.GetInstance().StateInBattle.CurrentPosition != Vector3.zero)
        {
            transform.position = UserController.GetInstance().StateInBattle.CurrentPosition;
        }
        SetQuantityMovements();
        // UserController.GetInstance().StateInBattle.IsDeadCharacter = true;
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

    }

    public void CreateArrowDirection()
    {

        RaycastHit objectHit;
        Vector3 positionToArrow = Vector3.zero;
        // Debug.DrawRay(transform.position, Vector3.down, Color.green, 30, false);
        if (Physics.Raycast(transform.position, Vector3.down, out objectHit))
        {
            if (objectHit.collider.transform.gameObject.tag == "Point")
            {
                List<DirectionMove.OptionMovements> movementsAvaibles;
                objectHit.collider.transform.gameObject.GetComponent<PointInteractive>().CheckBrigdeAllDirections();
                movementsAvaibles = objectHit.collider.transform.gameObject.GetComponent<PointInteractive>().DirectionAvaibleMovement;
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
            GameObject twoSword = Instantiate(prefabToBattle);
            List<Charac> allCharactersEnemiesCollision = new List<Charac>();
            Vector3 positionToArrow = Vector3.zero;
            allCharactersEnemiesCollision = goEnemiesCollision[0].GetComponent<EnemyInMovement>().TheCharacters;
            switch (goEnemiesCollision[0].GetComponent<EnemyInMovement>().DirectionBelongToPoint)
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

        float initialBloomIntesity = 1;
        float endBloomIntensity = 40.0f;
        b.diffusion.value = 4;

        float initialValueMinEv = 0;
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
                break;
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
                break;

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
                break;
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
                break;
                
            default:
                Debug.Log("Tipo de obstaculo no identificado");
                return false;
                break;
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
        Debug.Log("Termine de limpiar");
        CreateArrowDirection();
        Debug.Log("Termine de revisar los brigde");
    }
}
