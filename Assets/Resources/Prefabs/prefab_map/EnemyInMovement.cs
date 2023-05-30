using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInMovement : MonoBehaviour
{
    [Header("In case not create dynamical")]
    public List<int> setSpecificalIdCharacter;
    public bool randomCharacterSpecific = false;
    private List<Charac> _theCharacters;
    private bool _isALive;
    private bool _isEnemyFinalOfMap;
    private string _idElement;
    private string _idElementPointInteractive;
    private DirectionMove.OptionMovements _directionBelongToPoint;
    public List<Charac> TheCharacters { get => _theCharacters; set => _theCharacters = value; }
    public bool IsALive { get => _isALive; set => _isALive = value; }
    public bool IsEnemyFinalOfMap { get => _isEnemyFinalOfMap; set => _isEnemyFinalOfMap = value; }
    public string IdElement { get => _idElement; set => _idElement = value; }
    public string IdElementPointInteractive { get => _idElementPointInteractive; set => _idElementPointInteractive = value; }
    public DirectionMove.OptionMovements DirectionBelongToPoint { get => _directionBelongToPoint; set => _directionBelongToPoint = value; }
    private void Awake()
    {
        // Debug.Log("Recine me lenvate");
        _theCharacters = new List<Charac>();
        _isALive = true;
        _isEnemyFinalOfMap = false;

    }
    void Start()
    {
        Debug.Log("El valor de thCharacters : " + _theCharacters.Count);
        if (_theCharacters.Count == 0)
        {
            // if (randomCharacterSpecific)
            if(setSpecificalIdCharacter.Count == 0)
            {
                _theCharacters = GameData.GetInstance().GetRandomEnemyByMap(numberMap: 1, type: "Animal", quality: "Muy Comun", Random.Range(0, 4));
            }
            else
            {
                // Debug.Log("Aca lo9s characteres en loop");
                foreach (int idCharacterInLoop in setSpecificalIdCharacter)
                {
                    Charac theCharE = GameData.GetInstance().allCharacters.SearchCharacterById(idCharacterInLoop);
                    theCharE.InitialValuesDerived();
                    _theCharacters.Add(theCharE);
                }
                // Necessary for create SpecificalIdCharacter
                CreatePrefab();
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void DieThisEnemy()
    {
        Debug.Log("Se procedere a matar al enemigo");
        StartCoroutine(ProcessDie());

    }

    IEnumerator ProcessDie()
    {
        Animator myAnim = transform.parent.gameObject.GetComponent<Animator>();
        myAnim.SetBool("IsDie", true);

        // Waiting for start animation
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }

        // myAnim.SetBool("IsDie", false);
        UserController.GetInstance().StateInBattle.IsDeadCharacter = false;
        PointInteractiveStructure goPointInteractive = UserController.GetInstance().StateInBattle.EnemiesInMap[_idElementPointInteractive];
        // Debug.Log("ANtes : " + UserController.GetInstance().StateInBattle.EnemiesInMap[_idElementPointInteractive].PositionEnemies.Count);
        goPointInteractive.PositionEnemies.Remove(_directionBelongToPoint);
        // Debug.Log("Despues : " + UserController.GetInstance().StateInBattle.EnemiesInMap[_idElementPointInteractive].PositionEnemies.Count);
        UserController.GetInstance().StateInBattle.CounterQuantityElements--;
        UserController.GetInstance().StateInBattle.TotalElementsInMap--;
        // UserController.GetInstance().StateInBattle.EnemiesInMap[_idElementPointInteractive]
        // foreach (var enemiesPosition in goPointInteractive.PositionEnemies)
        // {
        //     GameObject enemyObject = Instantiate(SelectionBattleManager.GetInstance().prefabEnemies[0]);
        //     enemyObject.transform.parent = transform;
        //     directionSelectedToCreate = enemiesPosition.Key;
        //     enemyObject.transform.localPosition = PositionAroundThisPoint(directionSelectedToCreate);
        //     _enemiesAround[directionSelectedToCreate] = enemyObject;

        //     GameObject theEnemyBody = UtilitiesClass.FindAllChildWithTag(enemyObject, "Enemy");
        //     theEnemyBody.GetComponent<EnemyInMovement>().IdElement = idElement + "" + directionSelectedToCreate;
        //     theEnemyBody.GetComponent<EnemyInMovement>().IdElementPointInteractive = idElement;
        // }

        Destroy(transform.parent.gameObject);
    }

    public void CreatePrefab()
    {
        // Get parent Interactive point

        GameObject enemyContainer =  this.transform.parent.gameObject;        
        GameObject pointInteractiveLocal =  enemyContainer.transform.parent.gameObject;
        GetComponent<MeshRenderer>().enabled = false;
        Charac theCharacterEnemyToShow = _theCharacters[0];

        GameObject theEnemyRealPrefab = Instantiate(theCharacterEnemyToShow.prefabCharInBattle);
        
        theEnemyRealPrefab.transform.SetParent(this.transform);
        theEnemyRealPrefab.transform.localPosition = new Vector3(0, -0.5f, 0);

        float factorToScale = 3.2f;
        theEnemyRealPrefab.transform.localScale = new Vector3(theEnemyRealPrefab.transform.localScale.x * factorToScale, theEnemyRealPrefab.transform.localScale.y * factorToScale, theEnemyRealPrefab.transform.localScale.z * factorToScale);
        Vector3 diferenceVector = new Vector3(pointInteractiveLocal.transform.position.x, 0, pointInteractiveLocal.transform.position.z) - new Vector3(theEnemyRealPrefab.transform.position.x, 0, theEnemyRealPrefab.transform.position.z);
        theEnemyRealPrefab.transform.rotation = Quaternion.LookRotation(diferenceVector, Vector3.up);

        // Disabled component from enemies prefab
        theEnemyRealPrefab.GetComponent<BoxCollider>().enabled = false;
    }
}
