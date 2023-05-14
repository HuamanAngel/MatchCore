using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEditor.Animations;
public class LogicGame : MonoBehaviour
{
    // Start is called before the first frame update

    private int turn;
    private int whatTurn;
    public GameObject canvas;
    [Tooltip("Set GameObject what contain tha text of the turn. Text Mesh Pro component")]
    public GameObject objectTurn;
    [Tooltip("Set Icon select turn")]
    public GameObject iconObjectTurn;
    public GameObject toNextTurnPrefab;
    public Texture sphereRed;
    public Texture sphereBlue;
    public Texture sphereYellow;
    public GameObject objectToShowText4Panels;
    public GameObject objectToShowText3Panels;
    private GameObject infoPanel;
    private int _quantityMatchTotal = 0;
    public int QuantityMatchTotal { get => _quantityMatchTotal; }
    public int Turn { get => turn; }
    public GameObject objectTextFloating;
    public GameObject objectNumberFloating;
    public GameObject objBoxSkillFloating;
    public GameObject objBoxSkillEmptyFloating;
    private List<GameObject> _clonesBoxSkill;
    public int WhatTurn { get => whatTurn; }
    private PlayerBase _thePlayer;
    private Character _theCharacter;
    private int _movementDone = 0;
    public int MovementDone { get => _movementDone; set => _movementDone = value; }
    public PlayerBase ThePlayer { get => _thePlayer; }
    // private List<int> _existItemInPlace;
    // Animator to Hour Glass
    public static LogicGame _instance;
    public Animator animatorHourGlass;
    // Selected options
    private bool isCurrentSelectedSkill = false;
    private bool isCurrentSelectedCharacterToAttack = false;
    private Skill skillSelected;
    private GameObject buttonSkillSelected;
    private GameObject buttonCharacterSelectedToAttack;
    public GameObject objVictory;
    public GameObject objLoss;
    public bool IsCurrentSelectedSkill { get => isCurrentSelectedSkill; set => isCurrentSelectedSkill = value; }
    public bool IsCurrentSelectedCharacterToAttack { get => isCurrentSelectedCharacterToAttack; set => isCurrentSelectedCharacterToAttack = value; }
    public Skill SkillSelected { get => skillSelected; set => skillSelected = value; }
    public GameObject ButtonSkillSelected { get => buttonSkillSelected; set => buttonSkillSelected = value; }
    public GameObject ButtonCharacterSelectedToAttack { get => buttonCharacterSelectedToAttack; set => buttonCharacterSelectedToAttack = value; }
    public Material materialToLineRenderer;
    // public GameObject prefabTextFloating;

    public static LogicGame GetInstance()
    {
        return _instance;
    }
    private void Awake()
    {
        _instance = this;
        turn = 1;
        whatTurn = 1; // 1 for player 1, 2 for player 2
        ChangeTextTurn(turn);
        _clonesBoxSkill = new List<GameObject>();
    }

    public void NextTurn()
    {
        animatorHourGlass.SetBool("IsPressed", true);
        turn = turn + 1;
        if (whatTurn == 1)
        {
            whatTurn = 2;
        }
        else
        {
            whatTurn = 1;
        }
        // if (turn % 9 == 0)
        // {
        //     BoardManager.instance.ResetBoard();
        // }
        _movementDone = 0;
        SetPlayer();
        // _thePlayer.ResetQuantityMovementBoard();
        _thePlayer.SetQuantitySpheres();
        StartCoroutine(animationHourSand());
        ChangeTextTurn(turn);
        CheckTurnPlayer(whatTurn);
    }
    public IEnumerator animationHourSand()
    {
        yield return new WaitForSeconds(1.0f);
        animatorHourGlass.SetBool("IsPressed", false);
        toNextTurnPrefab.SetActive(false);

    }
    public void ChangeTextTurn(int turnValue)
    {
        objectTurn.GetComponent<TMP_Text>().text = "Turno " + turnValue;
    }

    public void CheckTurnPlayer(int turnPlayer)
    {
        // GameObject auxPrefabNextTurn = Instantiate(toNextTurnPrefab, canvas.transform);
        toNextTurnPrefab.SetActive(true);
        // List<GameObject> spheresEmpties = UtilitiesClass.FindAllChildWithTag(auxPrefabNextTurn, "EmptySphere");
        // auxPrefabNextTurn.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        if (turnPlayer == 1)
        {
            // GiveSpheres(1, spheresEmpties, 3);
            // UserPlayerController.GetInstance().SetQuantitySpheres();
            // Create text in the center
            Debug.Log("Turn to player 1");
            // Disable all for the player 2
        }
        else
        {
            // GiveSpheres(2, spheresEmpties, 3);
            // EnemyPlayerController.GetInstance().SetQuantitySpheres();
            Debug.Log("Turn to player 2");
            // Disable all for the player 1
        }
    }

    void Start()
    {
        SetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // if (isCurrentSelectedSkill)
        // {
        //     if (Input.GetKeyUp(KeyCode.Mouse1))
        //     {
        //         buttonSkillSelected.GetComponent<ButtonSkill>().DeselectedSkill();
        //     }

        // }
    }

    public void SetPlayer()
    {
        if (whatTurn == 1)
        {
            _thePlayer = UserPlayerController.GetInstance();
        }
        else if (whatTurn == 2)
        {
            _thePlayer = EnemyPlayerController.GetInstance();
        }
    }
    public void PressOption1P1()
    {
    }

    public void PressOption1P1Interrogation(Transform trans)
    {

    }

    public void PressOption1P2()
    {

    }
    public void OnExitFromInterrogation()
    {
        Destroy(infoPanel);
    }
    public void PressOption1P2Interrogation(Transform trans)
    {

    }
    // Only for match
    public void GiveSpheres(int quantityMatch, Spheres.TypeOfSpheres theSphereType, out int totalAdd, out Color32 colorSphere)
    {
        PlayerBase theUserPlayer;
        int quantityAdd = 0;
        if (whatTurn == 1)
        {
            theUserPlayer = UserPlayerController.GetInstance();
        }
        else
        {
            theUserPlayer = EnemyPlayerController.GetInstance();
        }
        quantityAdd = quantityMatch - 1;
        totalAdd = quantityAdd;
        _quantityMatchTotal++;
        switch (theSphereType)
        {
            case Spheres.TypeOfSpheres.SPHERE_RED:
                theUserPlayer.RedSphereG = theUserPlayer.RedSphereG + quantityAdd;
                colorSphere = new Color32(0xC3, 0x6B, 0x68, 0xFF);
                break;
            case Spheres.TypeOfSpheres.SPHERE_BLUE:
                theUserPlayer.BlueSphereG = theUserPlayer.BlueSphereG + quantityAdd;
                colorSphere = new Color32(0x7A, 0x9F, 0xAD, 0xFF);
                break;
            case Spheres.TypeOfSpheres.SPHERE_YELLOW:
                theUserPlayer.YellowSphereG = theUserPlayer.YellowSphereG + quantityAdd;
                colorSphere = new Color32(0xBB, 0x9E, 0x61, 0xFF);
                break;
            default:
                theUserPlayer.RedSphereG = theUserPlayer.RedSphereG + quantityAdd;
                colorSphere = new Color32(0x7B, 0x42, 0x40, 0xFF);
                break;
        }
        theUserPlayer.SetQuantitySpheres();
    }
    public bool CheckIfDoneMovementAvaible()
    {
        return _thePlayer.QuantityMovementBoard <= _movementDone;
    }
    public void StartCoroutineNumberFloating(Vector3 positionToFloating, string text, Color32 startColorA, Color32 endColorA)
    {
        StartCoroutine(EffectText.FloatingTextFadeOut(objectNumberFloating, positionToFloating, text, startColorA, endColorA));
    }
    public void StartCoroutineTextFloating(Vector3 positionToFloating, string text, Color32 startColorA, Color32 endColorA)
    {
        StartCoroutine(EffectText.FloatingTextFadeOut(objectTextFloating, positionToFloating, text, startColorA, endColorA));
    }

    public void CreateSkillsButtonsInGame(Vector3 positionFloating, Character c1)
    {
        Debug.Log("cantidad de skiles : " + c1.allSKill.Count);
        Vector3 thePosition = new Vector3(0, 0, 0);
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                thePosition = positionFloating + new Vector3(-0.7f, 1, 0);
            }

            if (i == 1)
            {
                thePosition = positionFloating + new Vector3(-0.7f, 1, -0.8f);
            }
            if (i == 2)
            {
                thePosition = positionFloating + new Vector3(0.7f, 1, 0);
            }
            if (i == 3)
            {
                thePosition = positionFloating + new Vector3(0.7f, 1, -0.8f);
            }
            if (c1.allSKill.Count > i)
            {
                CreateSkillButton(thePosition, true, i, c1);
            }
            else
            {
                CreateSkillButton(thePosition, false, -1, c1);
            }


        }
    }

    public void CreateSkillButton(Vector3 positionFloating, bool existSkill, int numberSkill, Character c1)
    {
        Vector3 vectorTarget = positionFloating + new Vector3(0, 0, 1);
        GameObject go;
        if (existSkill)
        {
            go = Instantiate(objBoxSkillFloating, positionFloating, Quaternion.identity);
            GameObject toIcon = UtilitiesClass.FindChildByName(go, "ImageSkill");

            toIcon.GetComponent<SpriteRenderer>().sprite = c1.allSKill[numberSkill].iconSkillSprite;
            go.GetComponent<ButtonSkillInBattle>().NumberSkill = numberSkill;
            go.GetComponent<ButtonSkillInBattle>().TheSkillSelected = c1.allSKill[numberSkill];
            go.GetComponent<ButtonSkillInBattle>().CharacterGetSet = c1;            
            _clonesBoxSkill.Add(go);
            go.transform.Rotate(Camera.main.transform.localRotation.eulerAngles.x, go.transform.localRotation.eulerAngles.y, go.transform.localRotation.eulerAngles.z);

        }
        // else
        // {
        //     Debug.Log("Dice que nooo exsite el skill : " + numberSkill);
        //     go = Instantiate(objBoxSkillEmptyFloating, positionFloating, Quaternion.identity);
        //     go.GetComponent<ButtonSkillInBattle>().NumberSkill = numberSkill;
        // }
        // _clonesBoxSkill.Add(go);
        // go.transform.Rotate(Camera.main.transform.localRotation.eulerAngles.x, go.transform.localRotation.eulerAngles.y, go.transform.localRotation.eulerAngles.z);

    }

    public void DeleteClonesBox()
    {
        for (int i = 0; i < _clonesBoxSkill.Count; i++)
        {
            Destroy(_clonesBoxSkill[i]);
        }
    }


    // public void ProcessAttackSelected(GameObject targetAttack)
    // {
    //     GameObject originAttack = buttonSkillSelected.GetComponent<ButtonSkillInBattle>().CharacterBelong;
    //     Character characterTmp;
    //     if (originAttack.GetComponent<HeroController>() != null)
    //     {
    //         characterTmp = originAttack.GetComponent<HeroController>();
    //     }
    //     else if (originAttack.GetComponent<EnemyController>() != null)
    //     {
    //         characterTmp = originAttack.GetComponent<EnemyController>();
    //     }
    //     else
    //     {
    //         characterTmp = null;
    //     }


    //     if (targetAttack.GetComponent<HeroController>() != null)
    //     {
    //         if (characterTmp.CheckifCanAttackSpheres(buttonSkillSelected.GetComponent<ButtonSkillInBattle>().TheSkill))
    //         {
    //             int damageMin = (int)buttonSkillSelected.GetComponent<ButtonSkillInBattle>().TheSkill.damage_min;
    //             int damageMax = (int)buttonSkillSelected.GetComponent<ButtonSkillInBattle>().TheSkill.damage_max;
    //             targetAttack.GetComponent<HeroController>().ReceivedDamage(Random.Range(damageMin, damageMax));
    //             buttonSkillSelected.GetComponent<ButtonSkillInBattle>().DeselectedSkill(1);
    //             SoundManager.instance.PlaySFX(SoundManager.ClipItem.Attack);
    //             // bool isAlive = targetAttack.GetComponent<HeroController>().ThePlayer.EvaluateIfAliveCharacter(targetAttack);
    //             // if(targetAttack.GetComponent<HeroController>().life)
    //             bool isAlive = targetAttack.GetComponent<HeroController>().CharacterIsAlive();
    //             if (!isAlive)
    //             {
    //                 targetAttack.GetComponent<HeroController>().ThePlayer.ChangeStateAliveCharacter(targetAttack, false);
    //                 bool stillSomethingAlive = targetAttack.GetComponent<HeroController>().ThePlayer.EvaluateIfExistStillAliveCharacter();
    //                 if (!stillSomethingAlive)
    //                 {
    //                     GameManager.instance.GameOver = true;
    //                     ShowLoss();
    //                 }
    //             }
    //         }
    //     }
    //     else if (targetAttack.GetComponent<EnemyController>() != null)
    //     {
    //         if (characterTmp.CheckifCanAttackSpheres(buttonSkillSelected.GetComponent<ButtonSkillInBattle>().TheSkill))
    //         {
    //             int damageMin = (int)buttonSkillSelected.GetComponent<ButtonSkillInBattle>().TheSkill.damage_min;
    //             int damageMax = (int)buttonSkillSelected.GetComponent<ButtonSkillInBattle>().TheSkill.damage_max;
    //             targetAttack.GetComponent<EnemyController>().ReceivedDamage(Random.Range(damageMin, damageMax));
    //             buttonSkillSelected.GetComponent<ButtonSkillInBattle>().DeselectedSkill(1);
    //             SoundManager.instance.PlaySFX(SoundManager.ClipItem.Attack);
    //             bool isAlive = targetAttack.GetComponent<EnemyController>().CharacterIsAlive();
    //             Debug.Log("is alive : " + isAlive);
    //             if (!isAlive)
    //             {
    //                 targetAttack.GetComponent<EnemyController>().ThePlayer.ChangeStateAliveCharacter(targetAttack, false);
    //                 bool stillSomethingAlive = targetAttack.GetComponent<EnemyController>().ThePlayer.EvaluateIfExistStillAliveCharacter();
    //                 if (!stillSomethingAlive)
    //                 {
    //                     GameManager.instance.GameOver = true;
    //                     UserController.GetInstance().StateInBattle.IsDeadCharacter = true;
    //                     ShowVictory();
    //                 }
    //             }

    //         }
    //     }
    //     else
    //     {
    //     }


    // }

    public void CheckIfEndBattle()
    {
        bool stillSomethingAlivePlayer1 = UserPlayerController.GetInstance().EvaluateIfExistStillAliveCharacter();
        bool stillSomethingAlivePlayer2 = EnemyPlayerController.GetInstance().EvaluateIfExistStillAliveCharacter();
        // bool stillSomethingAlive = targetAttack.GetComponent<HeroController>().ThePlayer.EvaluateIfExistStillAliveCharacter();
        if (!stillSomethingAlivePlayer1)
        {
            GameManager.instance.GameOver = true;
            ShowLoss();
        }
        if (!stillSomethingAlivePlayer2)
        {
            GameManager.instance.GameOver = true;
            ShowVictory();
        }        
    }


    public void IncrementMovement(int increment = 1)
    {
        _movementDone += increment;
        _thePlayer.QuantityMovementBoardCurrent = _thePlayer.QuantityMovementBoardCurrent - 1;
    }

    private void ShowVictory()
    {
        objVictory.SetActive(true);
    }

    private void ShowLoss()
    {
        objLoss.SetActive(true);
    }
}
