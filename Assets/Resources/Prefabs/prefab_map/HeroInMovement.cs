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
    private Rigidbody _rb;
    public TMP_Text textQuantityMoves;
    private bool _isMovement = false;
    private int _quantityMovementsInScene = 4;
    public GameObject screenGameOver;
    public DirectionMove.OptionMovements DirectionMovement { get => _directionMovement; set => _directionMovement = value; }
    public PostProcessVolume transitionEffectGameObject;
    public static HeroInMovement GetInstance()
    {
        return _instance;
    }
    private void Awake()
    {
        _instance = this;
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _allArrows = new List<GameObject>();
        CreateArrowDirection();
        SetQuantityMovements();
        // StartCoroutine(TransitionToBattle());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CreateArrowDirection()
    {
        float pivotTransformArrow = 2f;

        RaycastHit objectHit;
        Vector3 positionToArrow = Vector3.zero;
        // Debug.DrawRay(transform.position, Vector3.down, Color.green, 30, false);
        if (Physics.Raycast(transform.position, Vector3.down, out objectHit))
        {
            if (objectHit.collider.transform.gameObject.tag == "Point")
            {
                List<DirectionMove.OptionMovements> movementsAvaibles = objectHit.collider.transform.gameObject.GetComponent<PointInteractive>().DirectionAvaibleMovement;


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
        StartCoroutine(DoMoving(targetPosition));
    }
    public IEnumerator DoMoving(Vector3 target)
    {
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

        Vector3[] directionRayCast = new Vector3[] { Vector3.forward * 3, Vector3.back * 3, Vector3.right * 3, Vector3.left * 3 };
        if (!CheckIfEnemieExistInDirection(directionRayCast))
        {
            CreateArrowDirection();
            CheckIfGameOver();
        }
        else
        {
            Debug.Log("cargando");
            // TransitionToBattle();
            StartCoroutine(TransitionToBattle());

        }
    }

    public void SetQuantityMovements()
    {
        textQuantityMoves.text = "" + _quantityMovementsInScene;
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
    public bool CheckIfEnemieExistInDirection(Vector3[] directionRaycast)
    {
        RaycastHit objectHit;
        // Vector3 positionToArrow = Vector3.zero;
        for (int i = 0; i < directionRaycast.Length; i++)
        {
            Debug.DrawRay(transform.position, directionRaycast[i], Color.green, 30, false);
            if (Physics.Raycast(transform.position, directionRaycast[i], out objectHit))
            {
                if (objectHit.collider.transform.gameObject.tag == "Enemy")
                {
                    // Debug.Log("Collision with enemy");
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
                return new Vector3(objectHit.collider.transform.position.x, 0, objectHit.collider.transform.position.z);
            }

        }
        return Vector3.zero;
    }

    public IEnumerator TransitionToBattle()
    {
        // PostProcessVolume theEffects = transitionEffectGameObject.GetComponent<PostProcessVolume>();
        // Bloom b;
        // theEffects.profile.TryGetSettings(out b);
        // b.intensity.value = 50.0f;



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

        while(dof.focalLength.value < endValueFocalLength)
        {
            dof.focalLength.value = initialValueFocalLength;
            if(initTransitionBloom <= initialValueFocalLength)
            {
                b.intensity.value += 3;
            }
            if(initTransitionAutoExposure <= initialValueFocalLength)
            {   
                ae.minLuminance.value += 3;
            }

            initialValueFocalLength +=3 ;
            yield return new WaitForSeconds(0.0001f);
        }
        SceneController.ToBattle();
    }
}
