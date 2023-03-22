using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
public class EnemyController : Character
{
    public Texture textureIcon;
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
    public override void UpdateAllStats()
    {
        objLife.GetComponent<TMP_Text>().text = "" + life;
        objArmor.GetComponent<TMP_Text>().text = "" + defense;
        objectQuantitySpheresRed.GetComponent<TMP_Text>().text = "x" + _thePlayer.RedSphereG;
        objectQuantitySpheresBlue.GetComponent<TMP_Text>().text = "x" + _thePlayer.BlueSphereG;
        objectQuantitySpheresYellow.GetComponent<TMP_Text>().text = "x" + _thePlayer.YellowSphereG;
    }

    public static Rect GetWorldRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        // Get the bottom left corner.
        Vector3 position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        return new Rect(position, size);
    }

    public override void ReceivedDamage(int damageH)
    {
        int lifeBefore = life;
        int totalDamage = ((damageH - defense) * (100 - perAbsortion) / 100);
        // Vector3 originPosition = new Vector3(transform.position.x, transform.position.y, -1);
        Vector3 originPosition = new Vector3(transform.position.x, transform.position.y, -1);
        // GetWorldRect()
        life = life - totalDamage;
        if (countingCoroutine != null)
        {
            StopCoroutine(countingCoroutine);
        }
        countingCoroutine = StartCoroutine(EffectText.CountNumber(((result) => objLife.GetComponent<TMP_Text>().text = result), lifeBefore, life));
        LogicGame.GetInstance().StartCoroutineTextFloating(originPosition, "" + totalDamage, new Color32(0xFF, 0x00, 0x00, 0xFF), new Color32(0xFF, 0x00, 0x00, 0x00));
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
