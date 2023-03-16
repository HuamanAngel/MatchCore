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
    public bool IsCurrentSelectedSkill { get => isCurrentSelectedSkill; set => isCurrentSelectedSkill = value; }
    public bool IsCurrentSelectedCharacterToAttack { get => isCurrentSelectedCharacterToAttack; set => isCurrentSelectedCharacterToAttack = value; }
    public Skill SkillSelected { get => skillSelected; set => skillSelected = value; }
    public GameObject ButtonSkillSelected { get => buttonSkillSelected; set => buttonSkillSelected = value; }
    public GameObject ButtonCharacterSelectedToAttack { get => buttonCharacterSelectedToAttack; set => buttonCharacterSelectedToAttack = value; }


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
        _movementDone = 0;
        SetPlayer();
        _thePlayer.ResetQuantityMovementBoard();
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
        if (isCurrentSelectedSkill)
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                buttonSkillSelected.GetComponent<ButtonSkill>().DeselectedSkill();
            }

        }
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
    public void GiveSpheres(int quantityMatch, Spheres.TypeOfSpheres theSphereType)
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
        _quantityMatchTotal++;
        switch (theSphereType)
        {
            case Spheres.TypeOfSpheres.SPHERE_RED:
                theUserPlayer.RedSphereG = theUserPlayer.RedSphereG + quantityAdd;
                break;
            case Spheres.TypeOfSpheres.SPHERE_BLUE:
                theUserPlayer.BlueSphereG = theUserPlayer.BlueSphereG + quantityAdd;
                break;
            case Spheres.TypeOfSpheres.SPHERE_YELLOW:
                theUserPlayer.YellowSphereG = theUserPlayer.YellowSphereG + quantityAdd;
                break;
            default:
                theUserPlayer.RedSphereG = theUserPlayer.RedSphereG + quantityAdd;
                break;
        }
        theUserPlayer.SetQuantitySpheres();
    }
    public bool CheckIfDoneMovementAvaible()
    {
        return _thePlayer.QuantityMovementBoard <= _movementDone;
    }
    public void StartCoroutineTextFloating(Vector3 positionToFloating, string text, Color32 startColorA, Color32 endColorA)
    {
        StartCoroutine(FloatingText(objectTextFloating, positionToFloating, text, startColorA, endColorA));
    }

    public IEnumerator FloatingText(GameObject objectToFloating, Vector3 positionToFloating, string text, Color32 startColorA, Color32 endColorA)
    {
        // adwa
        Color32 startColor = startColorA;
        Color32 endColor = endColorA;

        GameObject go = Instantiate(objectToFloating, positionToFloating, Quaternion.identity);
        // go.transform.Rotate(Camera.main.transform.localRotation.eulerAngles.x, go.transform.localRotation.eulerAngles.y, go.transform.localRotation.eulerAngles.z);
        go.GetComponent<TMP_Text>().text = text;
        go.GetComponent<TMP_Text>().color = startColor;
        float speed = 1.0f;
        float step = speed * Time.deltaTime;
        float t = 0;
        Vector3 vectorTarget = go.transform.position + new Vector3(0, 1, 1);
        while (t < 1)
        {
            go.GetComponent<TMP_Text>().color = Color32.Lerp(startColor, endColor, t);
            go.transform.position = Vector3.MoveTowards(go.transform.position, vectorTarget, step);
            t += Time.deltaTime / 2f;
            yield return null;
        }
        Destroy(go);
    }

    public void CreateSkillsButtonsInGame(Vector3 positionFloating, Character c1)
    {

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
            // go.GetComponent<ButtonSkill>().NumberSkill = numberSkill;
            // go.GetComponent<ButtonSkill>().TheSkillSelected = c1.allSKill[numberSkill];
            // go.GetComponent<ButtonSkill>().CharacterGetSet = c1;            
        }
        else
        {
            Debug.Log("Dice que nooo exsite el skill : " + numberSkill);
            go = Instantiate(objBoxSkillEmptyFloating, positionFloating, Quaternion.identity);
            // go.GetComponent<ButtonSkill>().NumberSkill = numberSkill;
        }
        _clonesBoxSkill.Add(go);
        go.transform.Rotate(Camera.main.transform.localRotation.eulerAngles.x, go.transform.localRotation.eulerAngles.y, go.transform.localRotation.eulerAngles.z);

    }

    public void DeleteClonesBox()
    {
        for (int i = 0; i < _clonesBoxSkill.Count; i++)
        {
            Destroy(_clonesBoxSkill[i]);
        }
    }


    public void ProcessAttackSelected(GameObject targetAttack)
    {
        GameObject originAttack = buttonSkillSelected.GetComponent<ButtonSkill>().CharacterBelong;
        Character characterTmp;
        if (originAttack.GetComponent<HeroController>() != null)
        {
            characterTmp = originAttack.GetComponent<HeroController>();
        }
        else if (originAttack.GetComponent<EnemyController>() != null)
        {
            characterTmp = originAttack.GetComponent<EnemyController>();
        }
        else
        {
            characterTmp = null;
        }


        if (targetAttack.GetComponent<HeroController>() != null)
        {
            if (characterTmp.CheckifCanAttackSpheres(buttonSkillSelected.GetComponent<ButtonSkill>().TheSkill))
            {
                int damageMin = (int)buttonSkillSelected.GetComponent<ButtonSkill>().TheSkill.damage_min;
                int damageMax = (int)buttonSkillSelected.GetComponent<ButtonSkill>().TheSkill.damage_max;
                targetAttack.GetComponent<HeroController>().ReceivedDamage(Random.Range(damageMin, damageMax));
                buttonSkillSelected.GetComponent<ButtonSkill>().DeselectedSkill(1);
                SoundManager.instance.PlaySFX(SoundManager.ClipItem.Attack);
            }
        }
        else if (targetAttack.GetComponent<EnemyController>() != null)
        {
            if (characterTmp.CheckifCanAttackSpheres(buttonSkillSelected.GetComponent<ButtonSkill>().TheSkill))
            {
                int damageMin = (int)buttonSkillSelected.GetComponent<ButtonSkill>().TheSkill.damage_min;
                int damageMax = (int)buttonSkillSelected.GetComponent<ButtonSkill>().TheSkill.damage_max;
                targetAttack.GetComponent<EnemyController>().ReceivedDamage(Random.Range(damageMin, damageMax));
                buttonSkillSelected.GetComponent<ButtonSkill>().DeselectedSkill(1);
                SoundManager.instance.PlaySFX(SoundManager.ClipItem.Attack);
            }
        }
        else
        {
        }


    }

    public void IncrementMovement(int increment = 1)
    {
        _movementDone += increment;
        _thePlayer.QuantityMovementBoardCurrent = _thePlayer.QuantityMovementBoardCurrent - 1;
    }

}
