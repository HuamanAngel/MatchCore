using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabArrow;
    public static HeroInMovement _instance;
    private List<GameObject> _allArrows;
    private DirectionMove.OptionMovements _directionMovement;
    private float _speedMove = 3.0f;
    private Rigidbody _rb;
    private bool _isMovement = false;
    public DirectionMove.OptionMovements DirectionMovement { get => _directionMovement; set => _directionMovement = value; }
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
                            CreateOneArrowDirection(positionToArrow,DirectionMove.OptionMovements.UP);
                            break;
                        case DirectionMove.OptionMovements.BOTTOM:
                            positionToArrow = new Vector3(transform.position.x, transform.position.y, transform.position.z - pivotTransformArrow);
                            CreateOneArrowDirection(positionToArrow,DirectionMove.OptionMovements.BOTTOM);
                            break;
                        case DirectionMove.OptionMovements.RIGHT:
                            positionToArrow = new Vector3(transform.position.x + pivotTransformArrow, transform.position.y, transform.position.z);
                            CreateOneArrowDirection(positionToArrow,DirectionMove.OptionMovements.RIGHT);
                            break;
                        case DirectionMove.OptionMovements.LEFT:
                            positionToArrow = new Vector3(transform.position.x - pivotTransformArrow, transform.position.y, transform.position.z);
                            CreateOneArrowDirection(positionToArrow,DirectionMove.OptionMovements.LEFT);
                            break;
                    }
                }
            }
        }


        // Vector3 upVector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z + pivotTransformArrow);
        // Vector3 bottomVector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z - pivotTransformArrow);
        // Vector3 rightVector3 = new Vector3(transform.position.x + pivotTransformArrow, transform.position.y, transform.position.z);
        // Vector3 leftVector3 = new Vector3(transform.position.x - pivotTransformArrow, transform.position.y, transform.position.z);

        // GameObject arrowUp = Instantiate(prefabArrow);
        // GameObject arrowBottom = Instantiate(prefabArrow);
        // GameObject arrowRight = Instantiate(prefabArrow);
        // GameObject arrowLeft = Instantiate(prefabArrow);

        // arrowUp.transform.position = upVector3;
        // arrowBottom.transform.position = bottomVector3;
        // arrowRight.transform.position = rightVector3;
        // arrowLeft.transform.position = leftVector3;

        // arrowUp.GetComponent<DirectionMove>().TypeArrow = DirectionMove.OptionMovements.UP;
        // arrowBottom.GetComponent<DirectionMove>().TypeArrow = DirectionMove.OptionMovements.BOTTOM;
        // arrowRight.GetComponent<DirectionMove>().TypeArrow = DirectionMove.OptionMovements.RIGHT;
        // arrowLeft.GetComponent<DirectionMove>().TypeArrow = DirectionMove.OptionMovements.LEFT;

        // arrowUp.GetComponent<DirectionMove>().RotateByTypeArrow();
        // arrowBottom.GetComponent<DirectionMove>().RotateByTypeArrow();
        // arrowRight.GetComponent<DirectionMove>().RotateByTypeArrow();
        // arrowLeft.GetComponent<DirectionMove>().RotateByTypeArrow();

        // _allArrows.Add(arrowUp);
        // _allArrows.Add(arrowBottom);
        // _allArrows.Add(arrowRight);
        // _allArrows.Add(arrowLeft);
    }
    private void CreateOneArrowDirection(Vector3 positionObject,DirectionMove.OptionMovements directionMovement)
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
        StartCoroutine(DoMoving(targetPosition));
    }
    public IEnumerator DoMoving(Vector3 target)

    {
        while (Vector3.Distance(target, transform.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speedMove * Time.deltaTime);
            yield return null;
        }
        transform.position = target;


        CreateArrowDirection();
    }
}
