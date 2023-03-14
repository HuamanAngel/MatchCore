using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
public class EnemyController : Character
{
    public Texture textureIcon;

    public int life = 120;
    public int damageMax = 15;
    public int damageMin = 12;
    public int defense = 2;
    // public int armor =60;
    public int perAbsortion = 10;
    public GameObject player2Tier1;
    // Private
    private GameObject objLife;
    private GameObject objArmor;
    private GameObject objIcons;
    public GameObject objectQuantitySpheresBlue;
    public GameObject objectQuantitySpheresYellow;
    public GameObject objectQuantitySpheresRed;
    public GameObject player1Tiers;
    private UserController _dataUserGameObject;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _dataGrid.Add("SelectPlayer", 0);
        _dataGrid.Add("SelectAttack", 0);
        _dataGrid.Add("SelectMovement", 0);
        _dataGrid.Add("IsMoving", 0);
        _dataGrid.Add("IsAttacking", 0);

        _dataGrid.Add("FirstSkillSelected", 0);
        _dataGrid.Add("SecondSkillSelected", 0);
        _dataGrid.Add("ThirdSkillSelected", 0);
        _dataGrid.Add("FourSkillSelected", 0);
    }

    void Start()
    {
        if (inBattle)
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("LogicGame");
            _logicGame = go[0].GetComponent<LogicGame>();
            _thePlayer = EnemyPlayerController.GetInstance();

            player1Tiers.SetActive(true);
            objLife = UtilitiesClass.FindChildWithTag(player1Tiers, "Life");
            objArmor = UtilitiesClass.FindChildWithTag(player1Tiers, "Armor");
            objIcons = UtilitiesClass.FindChildWithTag(player1Tiers, "IconCharacter");

            objLife.GetComponent<TMP_Text>().text = "" + life;
            objArmor.GetComponent<TMP_Text>().text = "" + defense;
            objIcons.GetComponent<RawImage>().texture = textureIcon;

            StartBattle();
        }

    }

    public void SetQuantitySpheres()
    {
        objectQuantitySpheresRed.GetComponent<TMP_Text>().text = "x" + _thePlayer.RedSphereG;
        objectQuantitySpheresBlue.GetComponent<TMP_Text>().text = "x" + _thePlayer.BlueSphereG;
        objectQuantitySpheresYellow.GetComponent<TMP_Text>().text = "x" + _thePlayer.YellowSphereG;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public bool CheckIfSuccessAttack()
    {
        if (RedSphereG >= firstSkill.red && YellowSphereG >= firstSkill.yellow && BlueSphereG >= firstSkill.blue)
        {
            return true;

        }
        else
        {

            Debug.Log("No tienes suficientes esferas");
            Debug.Log("Tienes : yellow - " + YellowSphereG + "red - " + RedSphereG + "blue - " + BlueSphereG);
            Debug.Log("Costo : yellow - " + firstSkill.yellow + "red - " + firstSkill.red + "blue - " + firstSkill.blue);
            return false;
        }
    }
    public override void Attack()
    {
        RedSphereG = RedSphereG - firstSkill.red;
        YellowSphereG = YellowSphereG - firstSkill.yellow;
        BlueSphereG = BlueSphereG - firstSkill.blue;
        SetQuantitySpheres();
    }

    public override int CalculateDmg()
    {
        RandomHelper random = new RandomHelper();
        int randomDmg = random.RandomInt(damageMin, damageMax + 1);
        if (firstSkill.id == 1)
        {

        }
        if (firstSkill.id == 2)
        {
            int toLife = (int)(randomDmg * (firstSkill.value1 / 100));
            Debug.Log("Like life : " + toLife);
            life = life + toLife;
            UpdateAllStats();
        }
        return randomDmg;
    }
    public override void UpdateAllStats()
    {
        objLife.GetComponent<TMP_Text>().text = "" + life;
        objArmor.GetComponent<TMP_Text>().text = "" + defense;
        objectQuantitySpheresRed.GetComponent<TMP_Text>().text = "x" + _thePlayer.RedSphereG;
        objectQuantitySpheresBlue.GetComponent<TMP_Text>().text = "x" + _thePlayer.BlueSphereG;
        objectQuantitySpheresYellow.GetComponent<TMP_Text>().text = "x" + _thePlayer.YellowSphereG;
    }


    public override void ReceivedDamage(int damageH)
    {
        life = life - ((damageH - defense) * (100 - perAbsortion) / 100);
        UpdateAllStats();
    }
    public override void StartBattle()
    {
        base.ResetSphere();
        base.RandomSphere(1, 4);
        SetQuantitySpheres();
        // Debug.Log("For enemy :  yellow : "+ YellowSphereG + " ; red : " + RedSphereG + " ; yellow : " + YellowSphereG);

    }

    public void Movement()
    {
        Debug.Log("Movement is current");
    }

    public void SpecialAttack1()
    {
        Debug.Log("Movement is special effect 1");
    }

    public void SpecialAttack2()
    {
        Debug.Log("Movement is special effect 2");
    }

    public void SpecialAttack3()
    {
        Debug.Log("Movement is special effect 3");
    }

    public void NextTurn()
    {
        Debug.Log("Next turn");
    }
}
