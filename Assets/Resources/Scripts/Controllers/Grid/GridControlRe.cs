using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridControlRe : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid grid;
    private AttackGrid _attackGrid;
    [SerializeField] private Tilemap floorMap = null;
    [SerializeField] private Tilemap cursorMap = null;
    [SerializeField] private Tilemap pathMap = null;
    [SerializeField] private Tilemap bonusMap = null;
    [SerializeField] private Tilemap effectMap = null;
    [SerializeField] private GameObject hoverObject = null;
    [SerializeField] private GameObject pathObject = null;
    [SerializeField] private GameObject attackObject = null;
    // private GameObject[][] objectsInMap = new GameObject[5][];
    private GameObject[] objectInMap = new GameObject[2];
    private Vector3Int previousMousePos = new Vector3Int();
    private Vector3 positionTarget = new Vector3();
    private Character _character;
    private bool _pressSelected = false;
    public Tilemap FloorMap { get => floorMap; }
    public Tilemap PathMap { get => pathMap; }
    public Tilemap EffectMap { get => effectMap; }
    public Tilemap BonusMap { get => bonusMap; }
    public GameObject PathObject { get => pathObject; }
    public GameObject AttackObject { get => attackObject; }
    private LogicGame _logicGame;
    private bool _enabledAttackPath = false;
    private Vector3Int mousePos;
    private GameObject buttonSkillSelected;
    public bool PressSelected { get => _pressSelected; set => _pressSelected = value; }

    public bool EnabledAttackPath { get => _enabledAttackPath; set => _enabledAttackPath = value; }
    public GameObject ButtonSkillSelected { get => buttonSkillSelected; set => buttonSkillSelected = value; }
    public static GridControlRe _instance;
    private bool _inAttack = false;
    private bool _inMovement = false;
    public static GridControlRe GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        grid = gameObject.GetComponent<Grid>();
        _attackGrid = new AttackGrid();
    }

    void Start()
    {
        _logicGame = LogicGame.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = GetMousePosition(Input.mousePosition);
        HoverCursor(mousePos);
        if (!_inAttack && !_inMovement)
        {

            // // Control attack button

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "AttackObject")
                        {
                            if (_character.CheckifCanAttackSpheres(_character.SkillSelectedCurrent))
                            {
                                Vector3 positioToFloating = hit.collider.gameObject.transform.position + new Vector3(0, 0, -2);
                                positionTarget = InitAttack(hit.collider.gameObject, _character);
                                AttackProcess(_character, positionTarget, effectMap, _character.SkillSelectedCurrent.id);
                            }
                            else
                            {
                                Vector3 positioToFloating = hit.collider.gameObject.transform.position + new Vector3(0, 0, -2);
                                StartCoroutine(EffectText.FloatingTextFadeOut(_logicGame.objectTextFloating, positioToFloating, "No hay suficientes esferas rojas , amarillas o azules", new Color32(222, 41, 22, 255), new Color32(222, 41, 22, 0)));
                            }
                        }
                        if (hit.collider.tag == "Player1")
                        {
                            _character = hit.collider.gameObject.GetComponent<HeroController>();
                            if (_character.GetDataGrid("SelectMovement") == 1)
                            {
                                // Create instance attack animation
                                _character.SetDataGrid("SelectMovement", 0);
                                _character.SetDataGrid("SelectAttack", 1);
                                ClearAllObjectInTilemap(pathMap);
                                _logicGame.DeleteClonesBox();
                                _logicGame.CreateSkillsButtonsInGame(_character.gameObject.transform.position, _character);

                            }
                            else if (_character.GetDataGrid("SelectAttack") == 0)
                            {
                                _character.SetDataGrid("SelectAttack", 1);
                                ClearAllObjectInTilemap(pathMap);
                                _logicGame.DeleteClonesBox();
                                _logicGame.CreateSkillsButtonsInGame(_character.gameObject.transform.position, _character);
                            }
                            else
                            {
                                _character.SetDataGrid("SelectAttack", 0);
                                ClearAllObjectInTilemap(pathMap);
                                _logicGame.DeleteClonesBox();

                                // Destroy Skill
                            }

                        }
                    }
                }

            }
            // // Control movement
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Debug.Log($"hit collider is {hit.collider.tag}");
                    Debug.Log($"Presiono Mouse0 {hit.collider.tag}");
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "AttackObject")
                        {
                            if (_character.CheckifCanAttackSpheres(_character.SkillSelectedCurrent))
                            {
                                Vector3 positioToFloating = hit.collider.gameObject.transform.position + new Vector3(0, 0, -2);
                                positionTarget = InitAttack(hit.collider.gameObject, _character);
                                AttackProcess(_character, positionTarget, effectMap, _character.SkillSelectedCurrent.id);
                            }
                            else
                            {
                                Vector3 positioToFloating = hit.collider.gameObject.transform.position + new Vector3(0, 0, -2);
                                StartCoroutine(EffectText.FloatingTextFadeOut(_logicGame.objectTextFloating, positioToFloating, "No hay suficientes esferas rojas , amarillas o azules", new Color32(222, 41, 22, 255), new Color32(222, 41, 22, 0)));
                            }
                        }
                        if (hit.collider.tag == "MovementObject")
                        {
                            if (_character.CheckIfCanMovementSpheres())
                            {
                                positionTarget = InitMovement(hit.collider.gameObject, _character);
                                StartCoroutine(MovementProcessEnumerator(_character, positionTarget));
                            }
                            else
                            {
                                Vector3 positioToFloating = hit.point + new Vector3(0, 0, -2);
                                Vector3 theRotation = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, _logicGame.objectTextFloating.transform.localRotation.eulerAngles.y, _logicGame.objectTextFloating.transform.localRotation.eulerAngles.z);
                                StartCoroutine(EffectText.FloatingTextFadeOut(_logicGame.objectTextFloating, positioToFloating, "No hay suficientes esferas azules", new Color32(222, 41, 22, 255), new Color32(222, 41, 22, 0), theRotation));
                            }
                        }
                        if (hit.collider.tag == "Player1")
                        {
                            _character = hit.collider.gameObject.GetComponent<HeroController>();
                            if (_character.GetDataGrid("SelectAttack") == 1)
                            {
                                _character.SetDataGrid("SelectAttack", 0);
                                _character.SetDataGrid("SelectMovement", 1);
                                int[,] movkGrid = _attackGrid.GetTypeGrid(_character.moveType);

                                ClearAllObjectInTilemap(pathMap);
                                _logicGame.DeleteClonesBox();

                                DrawPathGrid(true, _attackGrid, movkGrid, pathMap, pathObject, mousePos);
                                Debug.Log("Quedo SelectetAttack 1");
                            }

                            if (_character.GetDataGrid("SelectMovement") == 0)
                            {
                                _character.SetDataGrid("SelectMovement", 1);
                                int[,] movkGrid = _attackGrid.GetTypeGrid(_character.moveType);
                                Vector3 posCell = _character.gameObject.transform.position;
                                ClearAllObjectInTilemap(pathMap);
                                _logicGame.DeleteClonesBox();
                                DrawPathGrid(true, _attackGrid, movkGrid, pathMap, pathObject, posCell);
                                Debug.Log("Quedo Selectmovement 0");
                            }
                            else
                            {
                                _character.SetDataGrid("SelectMovement", 0);
                                ClearAllObjectInTilemap(pathMap);
                                _logicGame.DeleteClonesBox();
                                Debug.Log("Quedo En otros");
                            }
                        }
                    }
                }
            }
        }
    }
    public void HoverCursor(Vector3Int mousePos)
    {
        if (mousePos == Vector3Int.zero)
        {
            return;
        }
        if (!mousePos.Equals(previousMousePos))
        {
            Vector3 worldPositionNow = cursorMap.CellToWorld(mousePos);
            // GameObject go = Instantiate(hoverObject, worldPositionNow + new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
            GameObject go = Instantiate(hoverObject);
            go.transform.position = worldPositionNow + new Vector3(0.5f, 0.1f, 0.5f);
            go.transform.SetParent(cursorMap.transform);
            objectInMap[1] = objectInMap[0];
            objectInMap[0] = go;
            Destroy(objectInMap[1]);
            previousMousePos = mousePos;
        }
    }
    public Vector3Int GetMousePosition(Vector3 positionMouse)
    {
        Vector3Int pos = Vector3Int.zero;
        Ray ray = Camera.main.ScreenPointToRay(positionMouse);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 7 || hit.collider.tag == "Player1" || hit.collider.tag == "Player2")
            {
                pos = pathMap.WorldToCell(hit.point);
            }
        }
        return pos;

    }
    public void DrawPathGrid(bool isMovement, AttackGrid _attackGrid, int[,] attackGrid, Tilemap pathMap, GameObject pathObject, Vector3 mousePos, bool isIa = false, List<Vector3> positionAllCellPos = null, List<int> existItem = null, bool instaciatePath = true)
    {
        // int valIterationOptional = 0;
        Vector3Int positionPlayerInRange = _attackGrid.GetAttackGridPositionPlayer(attackGrid);
        for (int i = 0; i < attackGrid.GetLength(0); i++)
        {
            for (int j = 0; j < attackGrid.GetLength(1); j++)
            {
                if (attackGrid[i, j] == 1)
                {
                    // Check if exist wall in front of path
                    Vector3 worldPositionNow;
                    // worldPositionNow = new Vector3(mousePos.x + i - positionPlayerInRange.x, 0, mousePos.z + j - positionPlayerInRange.y);
                    worldPositionNow = new Vector3(mousePos.x + i - positionPlayerInRange.x, 0.1f, mousePos.z + j - positionPlayerInRange.y);
                    if (isMovement)
                    {
                        Vector3 positionToRayCast = worldPositionNow - mousePos;
                        RaycastHit objectHit2;
                        float distanceToCheck = Vector3.Distance(mousePos, worldPositionNow);
                        Vector3 originModified = new Vector3(mousePos.x, 0.40f, mousePos.z);
                        Debug.DrawRay(originModified, positionToRayCast, Color.red, 30, false);
                        if (Physics.Raycast(originModified, positionToRayCast, out objectHit2, distanceToCheck))
                        {
                            // Debug.DrawRay(originModified, positionToRayCast * objectHit2.distance, Color.red, 30, false);
                            if (objectHit2.collider.transform.gameObject.layer == LayerMask.NameToLayer("CellHero") || objectHit2.collider.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                            {
                                continue;
                            }
                        }

                    }

                    // Check if has terrain
                    RaycastHit objectHit;
                    Debug.DrawRay(worldPositionNow + new Vector3(0, 2.0f, 0), Vector3.down, Color.red, 30, false);
                    if (Physics.Raycast(worldPositionNow + new Vector3(0, 2.0f, 0), Vector3.down, out objectHit, 6))
                    {
                        // Debug.Log("Golpeo : " + objectHit.collider.transform.gameObject.name);
                        // Debug.Log("Golpeo : " + objectHit.collider.transform.gameObject.layer);
                        string textLayerCell;
                        if (isMovement)
                        {
                            textLayerCell = null;
                        }
                        else
                        {
                            textLayerCell = "CellHero";
                        }
                        if (objectHit.collider.transform.gameObject.layer == LayerMask.NameToLayer("Floor")
                        || objectHit.collider.transform.gameObject.layer == LayerMask.NameToLayer("Items")
                        || objectHit.collider.transform.gameObject.layer == LayerMask.NameToLayer("Wall")
                        || objectHit.collider.transform.gameObject.layer == LayerMask.NameToLayer(textLayerCell))
                        {
                            // Create path
                            // Debug.Log($"valores i y j : {i} {j}");
                            if (instaciatePath)
                            {
                                if (objectHit.collider.transform.gameObject.layer != LayerMask.NameToLayer("Wall"))
                                {
                                    GameObject go = Instantiate(pathObject);
                                    go.transform.position = worldPositionNow;
                                    go.transform.SetParent(pathMap.transform);
                                }
                            }
                            if (isIa)
                            {
                                positionAllCellPos.Add(worldPositionNow);

                                if (objectHit.collider.transform.gameObject.layer == LayerMask.NameToLayer("Floor"))
                                {
                                    existItem.Add(0);
                                }
                                else if (objectHit.collider.transform.gameObject.layer == LayerMask.NameToLayer("Items"))
                                {
                                    GameObject g1 = objectHit.collider.transform.gameObject;
                                    Spheres.TypeOfSpheres type = g1.GetComponent<SphereItem>().typeSphere;
                                    switch (type)
                                    {
                                        case Spheres.TypeOfSpheres.SPHERE_BLUE:
                                            existItem.Add(1);
                                            break;
                                        case Spheres.TypeOfSpheres.SPHERE_RED:
                                            existItem.Add(2);
                                            break;
                                        case Spheres.TypeOfSpheres.SPHERE_YELLOW:
                                            existItem.Add(3);
                                            break;
                                    }
                                }
                                else if (objectHit.collider.transform.gameObject.layer == LayerMask.NameToLayer("CellHero"))
                                {
                                    if (objectHit.collider.transform.gameObject.tag == "Player2")
                                    {
                                        existItem.Add(4);

                                    }
                                    if (objectHit.collider.transform.gameObject.tag == "Player1")
                                    {
                                        existItem.Add(5);
                                    }
                                }
                            }

                        }
                    }

                }
            }
        }
    }

    public void ClearAllObjectInTilemap(Tilemap pathMap)
    {
        for (int i = pathMap.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(pathMap.transform.GetChild(i).gameObject);
        }
    }
    public Vector3 InitMovement(GameObject cellCollider, Character c1)
    {
        c1.SetDataGrid("IsMoving", 1);
        ClearAllObjectInTilemap(pathMap);
        _inMovement = false;
        return cellCollider.transform.position;
    }
    public Vector3 InitAttack(GameObject cellCollider, Character c1)
    {
        c1.SetDataGrid("IsAttacking", 1);
        ClearAllObjectInTilemap(pathMap);
        _inAttack = true;
        return cellCollider.transform.position;
    }

    public void MovementProcess(Character c1, Vector3 targetPos)
    {
        float speed = 1.0f;
        float step = speed * Time.deltaTime;
        if (Vector2.Distance(new Vector2(c1.transform.position.x, c1.transform.position.z), new Vector2(targetPos.x, targetPos.z)) > 0.001f)
        {
            c1.transform.position = Vector3.MoveTowards(c1.transform.position, new Vector3(targetPos.x, c1.transform.position.y, targetPos.z), step);
            c1.UpdateAllStats();
        }
        else
        {
            c1.SetDataGrid("SelectMovement", 0);
            c1.SetDataGrid("IsMoving", 0);
        }
    }

    public IEnumerator MovementProcessEnumerator(Character c1, Vector3 targetPos)
    {
        float speed = 1.0f;
        float step = speed * Time.deltaTime;
        Vector3 diferenceVector = new Vector3(targetPos.x, 0, targetPos.z) - new Vector3(c1.transform.position.x, 0, c1.transform.position.z);
        float distance = diferenceVector.magnitude;
        // distance = Mathf.Abs(distance);
        Vector3 direction = diferenceVector / (-distance);
        float damping = 5;
        // c1.transform.rotation = Quaternion.LookRotation(direction);
        // c1.transform.rotation = c1.transform.LookAt(targetPos);
        // Debug.Log("Nomralized positon : " + direction);
        c1.WalkingAnim();
        Debug.Log("Rotation initial : " + c1.transform.rotation);
        // Debug.Log("LookRotation final : " + Quaternion.LookRotation(direction,direction,Vector3.up));        
        // Debug.Log("LookRotation final : " + direction.LookAt());        
        
        while (Vector2.Distance(new Vector2(c1.transform.position.x, c1.transform.position.z), new Vector2(targetPos.x, targetPos.z)) > 0.001f)
        {
            c1.transform.position = Vector3.MoveTowards(c1.transform.position, new Vector3(targetPos.x, c1.transform.position.y, targetPos.z), step);
            // c1.transform.rotation = Quaternion.Lerp(c1.transform.rotation, Quaternion.LookRotation(direction,Vector3.up), Time.deltaTime * damping);
            c1.transform.rotation = Quaternion.Lerp(c1.transform.rotation, Quaternion.LookRotation(diferenceVector,Vector3.up), Time.deltaTime * damping);
            yield return null;
        }
        // Termino
        c1.SetDataGrid("SelectMovement", 0);
        c1.SetDataGrid("IsMoving", 0);
        c1.ReturnStateOriginalAnim();
        c1.UpdateAllStats();
        _inMovement = false;
    }
    public void AttackProcess(Character c1, Vector3 targetPos, Tilemap mapToEffectAttack, int id)
    {
        GameObject fire = Instantiate(c1.SkillSelectedCurrent.effectPrefab, targetPos, Quaternion.identity);
        fire.transform.SetParent(mapToEffectAttack.transform);
        fire.GetComponent<Fire>().DamageMin = (int)c1.SkillSelectedCurrent.damage_min;
        fire.GetComponent<Fire>().DamageMax = (int)c1.SkillSelectedCurrent.damage_max;
        fire.GetComponent<Fire>().TurnToDestroy = (int)c1.SkillSelectedCurrent.value1;
        if (c1.SkillSelectedCurrent.CheckIfNeedAnimation())
        {
            fire.transform.position = new Vector3(fire.transform.position.x, fire.transform.position.y + 2, fire.transform.position.z);
            // fire.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
            StartCoroutine(c1.SkillSelectedCurrent.AnimationToEffect(fire));
        }
        else
        {
            fire.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
        }
        c1.SetDataGrid("IsAttacking", 0);
        c1.SetDataGrid("SelectAttack", 0);
        c1.UpdateAllStats();
        StartCoroutine(AttackProcessAnimation(c1));
        _inAttack = false;
    }
    public IEnumerator AttackProcessAnimation(Character c1)
    {
        float t = 0;

        int numIteration = 0;
        foreach (Skill theSkillI in c1.allSKill)
        {
            numIteration++;
            if(theSkillI.id == c1.SkillSelectedCurrent.id)
            {
                break;
            }
        }
        c1.AttackAnim(numIteration);
        while (1.0f > t)
        {
            t = t + Time.deltaTime;
            yield return null;
        }
        c1.ReturnStateOriginalAnim();
    }

    public void DrawPathToHover()
    {
        ClearAllObjectInTilemap(pathMap);
        // Vector3 posCell = _character.gameObject.transform.position - new Vector3(0.5f, 0, 0.5f);
        Vector3 posCell = _character.gameObject.transform.position;
        _character.SkillSelectedHover = _character.GetSkillSelected(_character);
        int[,] movkGrid = _attackGrid.GetTypeGrid(_character.SkillSelectedHover.attackType);
        List<Vector3> positionAllCellPos = new List<Vector3>();
        List<int> existItem = new List<int>();
        DrawPathGrid(false, _attackGrid, movkGrid, pathMap, attackObject, posCell, true, positionAllCellPos, existItem);
    }

    public void ClearAllObjectInTilemapInteractive()
    {
        ClearAllObjectInTilemap(pathMap);
    }

    public void DrawPathAttackWhenSelectedSkill()
    {
        _logicGame.DeleteClonesBox();
        ClearAllObjectInTilemap(pathMap);
        Vector3 posCell = _character.gameObject.transform.position;
        _character.SkillSelectedCurrent = _character.GetSkillSelected(_character);
        int[,] movkGrid = _attackGrid.GetTypeGrid(_character.SkillSelectedCurrent.attackType);
        List<Vector3> positionAllCellPos = new List<Vector3>();
        List<int> existItem = new List<int>();
        DrawPathGrid(false, _attackGrid, movkGrid, pathMap, attackObject, posCell, true, positionAllCellPos, existItem);
        _pressSelected = false;
        Debug.Log("Se mantiene en rojo;");
        Debug.Log("El skill actual : " + _character.SkillSelectedCurrent);
    }
}
