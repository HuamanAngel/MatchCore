using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        GameObject bonusMap = GridControlRe.GetInstance().BonusMap.gameObject;
        List<GameObject> listPosition = UtilitiesClass.FindAllChildWithTag(bonusMap, "PositionPlayer2");
        _dataUserGameObject = UserController.GetInstance();
        RandomSphere(4, 8);
        SetQuantitySpheres();
        // StateInBattle
        User theUser = _dataUserGameObject.userEnemy;
        
        for (int i = 0; i < theUser.CharInCombat.Count; i++)
        {
            GameObject goHe = Instantiate(theUser.CharInCombat[i].prefabCharInBattle);
            goHe.tag = "Player2";
            goHe.AddComponent<EnemyController>();

            goHe.transform.SetParent(GridControlRe.GetInstance().gameObject.transform);

            int random = Random.Range(0, listPosition.Count);
            Vector3 positionFinal = new Vector3(listPosition[random].transform.position.x, 0.0f, listPosition[random].transform.position.z);
            // Aditional Position
            goHe.transform.position = positionFinal;
            listPosition[random].SetActive(false);
            listPosition.RemoveAt(random);

            // goHe.transform.localPosition =  new Vector3(1.0f,0.0f,-4.0f) +  new Vector3(0.5f - i, 0.0f, 0.5f);

            // goHe.tag = "Player2";
            goHe.GetComponent<EnemyController>().player1Tiers = player1Tiers[i];
            player1Tiers[i].SetActive(true);
            goHe.GetComponent<EnemyController>().inBattle = true;
            goHe.GetComponent<EnemyController>().objectQuantitySpheresRed = objectQuantitySpheresRed;
            goHe.GetComponent<EnemyController>().objectQuantitySpheresBlue = objectQuantitySpheresBlue;
            goHe.GetComponent<EnemyController>().objectQuantitySpheresYellow = objectQuantitySpheresYellow;
            goHe.GetComponent<EnemyController>().life = theUser.CharInCombat[i].lifeTotal;
            goHe.GetComponent<EnemyController>().defense = theUser.CharInCombat[i].armorTotal;
            goHe.GetComponent<EnemyController>().textureIcon = theUser.CharInCombat[i].iconChar;
            goHe.GetComponent<EnemyController>().costForMovement = theUser.CharInCombat[i].blue;
            // goHe.GetComponent<EnemyController>().moveType = theUser.CharInCombat[i].movementType;
            goHe.GetComponent<EnemyController>().allSKill = new List<Skill>();
            GameObject toIcon = UtilitiesClass.FindChildByName(player1Tiers[i], "PanelSkills");
            List<GameObject> skillsCanvas = UtilitiesClass.FindAllChildWithTag(toIcon, "SkillCanvas");
            int quantity = 0;
            quantity = theUser.CharInCombat[i].theSkills.Count;
            for (int j = 0; j < quantity; j++)
            {

                Dictionary<string, GameObject> dInformation = new Dictionary<string, GameObject>(); 
                GameObject objSkill = UtilitiesClass.FindChildByName(skillsCanvas[j], "ImageSkill");
                GameObject objInformation = UtilitiesClass.FindChildByName(skillsCanvas[j], "Information");
                GameObject objYellow = UtilitiesClass.FindChildByName(objInformation, "yellow");
                GameObject objRed = UtilitiesClass.FindChildByName(objInformation, "red");
                GameObject objBlue = UtilitiesClass.FindChildByName(objInformation, "blue");

                dInformation["yellow"] = objYellow;
                dInformation["red"] = objRed;
                dInformation["blue"] = objBlue;

                skillsCanvas[j].GetComponent<ButtonSkill>().CharacterBelong = goHe; 
                skillsCanvas[j].GetComponent<ButtonSkill>().TheSkill = theUser.CharInCombat[i].theSkills[j]; 
                skillsCanvas[j].GetComponent<ButtonSkill>().ElementsInformation = dInformation; 

                objSkill.GetComponent<RawImage>().texture = theUser.CharInCombat[i].theSkills[j].iconSkill;
                objYellow.GetComponent<TMP_Text>().text = "x" + theUser.CharInCombat[i].theSkills[j].yellow;
                objRed.GetComponent<TMP_Text>().text = "x" + theUser.CharInCombat[i].theSkills[j].red;
                objBlue.GetComponent<TMP_Text>().text = "x" + theUser.CharInCombat[i].theSkills[j].blue;

                goHe.GetComponent<EnemyController>().allSKill.Add(theUser.CharInCombat[i].theSkills[j]);
                skillsCanvas[j].GetComponent<ButtonSkill>().enabled = true;
                skillsCanvas[j].GetComponent<Button>().enabled = true;
            }
            _allHeroInPlay.Add(goHe);
            _allCharactersAlive[goHe] = true;

            goHe.AddComponent<EnemyInteligence>();
            goHe.AddComponent<EnemyInteligence>().NumberOrden = i;
        }
        
        foreach (GameObject goPositions in listPosition)
        {
            goPositions.SetActive(false);
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
