using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerController : PlayerBase
{
    // Start is called before the first frame update
    private static EnemyPlayerController _instance;
    private List<GameObject> _allHeroInPlay;
    private int ordenToEnemies;
    public bool endTurnAllEnemies;
    public bool EndTurnAllEnemies { get => endTurnAllEnemies; set => endTurnAllEnemies = value; }

    public int OrdenToEnemies { get => ordenToEnemies; }

    public static EnemyPlayerController GetInstance()
    {
        return _instance;
    }
    private void Awake()
    {
        _instance = this;
        _allHeroInPlay = new List<GameObject>();
        ordenToEnemies = 0;
        endTurnAllEnemies = false;
    }

    void Start()
    {
        GameObject[] goCh = GameObject.FindGameObjectsWithTag("DataUser");
        _dataUserGameObject = goCh[0].GetComponent<UserController>();
        RandomSphere(4, 8);
        User theUser = _dataUserGameObject.userEnemy;
        for (int i = 0; i < theUser.CharInCombat.Count; i++)
        {
            GameObject goHe = Instantiate(theUser.CharInCombat[i].prefabCharInBattle);
            goHe.AddComponent<EnemyController>();
            goHe.tag = "Enemies";
            goHe.transform.position = new Vector3(5.5f - i, 0.53f, -21.682f);
            goHe.GetComponent<EnemyController>().player1Tiers = player1Tiers[i];
            goHe.GetComponent<EnemyController>().inBattle = true;
            goHe.GetComponent<EnemyController>().objectQuantitySpheresRed = objectQuantitySpheresRed;
            goHe.GetComponent<EnemyController>().objectQuantitySpheresBlue = objectQuantitySpheresBlue;
            goHe.GetComponent<EnemyController>().objectQuantitySpheresYellow = objectQuantitySpheresYellow;
            goHe.GetComponent<EnemyController>().life = theUser.CharInCombat[i].lifeTotal;
            goHe.GetComponent<EnemyController>().defense = theUser.CharInCombat[i].armorTotal;
            goHe.GetComponent<EnemyController>().textureIcon = theUser.CharInCombat[i].iconChar;
            goHe.GetComponent<EnemyController>().costForMovement = theUser.CharInCombat[i].blue;
            // goHe.GetComponent<EnemyController>().moveType = theUser.CharInCombat[i].movementType;

            for (int j = 0; j < theUser.CharInCombat[i].theSkills.Count; j++)
            {
                goHe.GetComponent<EnemyController>().allSKill.Add(theUser.CharInCombat[i].theSkills[j]);
            }
            _allHeroInPlay.Add(goHe);
            goHe.AddComponent<EnemyInteligence>();
            goHe.AddComponent<EnemyInteligence>().NumberOrden = i;
        }
    }
    public void ChangeControlToOtherEnemy()
    {
        ordenToEnemies += 1;
        if (_dataUserGameObject.userEnemy.CharInCombat.Count <= ordenToEnemies)
        {
            Debug.Log("nada");
            ordenToEnemies = 0;
            endTurnAllEnemies = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
