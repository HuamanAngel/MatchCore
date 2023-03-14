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
    public int Turn { get => turn; }
    public GameObject objectTextFloating;
    public GameObject objBoxSkillFloating;
    public GameObject objBoxSkillEmptyFloating;
    private List<GameObject> _clonesBoxSkill;
    public int WhatTurn { get => whatTurn; }
    private PlayerBase _thePlayer;
    private Character _theCharacter;
    // private List<int> _existItemInPlace;
    // Animator to Hour Glass
    public Animator animatorHourGlass;
    private void Awake()
    {
        turn = 1;
        whatTurn = 1; // 1 for player 1, 2 for player 2
        ChangeTextTurn(turn);
        _clonesBoxSkill = new List<GameObject>();
    }

    public void NextTurn()
    {
        animatorHourGlass.SetBool("IsPressed",true);
        turn = turn + 1;
        if (whatTurn == 1)
        {
            whatTurn = 2;
        }
        else
        {
            whatTurn = 1;
        }
        StartCoroutine(animationHourSand());
        ChangeTextTurn(turn);
        CheckTurnPlayer(whatTurn);
    }
    public IEnumerator animationHourSand()
    {
        yield return new WaitForSeconds(0.517f);
        animatorHourGlass.SetBool("IsPressed",false);

    }
    public void ChangeTextTurn(int turnValue)
    {
        objectTurn.GetComponent<TMP_Text>().text = "Turno " + turnValue;
    }

    public void CheckTurnPlayer(int turnPlayer)
    {
        GameObject auxPrefabNextTurn = Instantiate(toNextTurnPrefab, canvas.transform);
        List<GameObject> spheresEmpties = UtilitiesClass.FindAllChildWithTag(auxPrefabNextTurn, "EmptySphere");
        auxPrefabNextTurn.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        if (turnPlayer == 1)
        {
            GiveSpheres(1, spheresEmpties, 3);
            UserPlayerController.GetInstance().SetQuantitySpheres();
            // Create text in the center
            Debug.Log("Turn to player 1");
            // Disable all for the player 2
        }
        else
        {
            GiveSpheres(2, spheresEmpties, 3);
            EnemyPlayerController.GetInstance().SetQuantitySpheres();
            Debug.Log("Turn to player 2");
            // Disable all for the player 1
        }
    }

    void Start()
    {
        _thePlayer = UserPlayerController.GetInstance();

    }

    // Update is called once per frame
    void Update()
    {

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

    public void PressOption2()
    {
        Debug.Log("pressed in option 1 from option in battle ");
    }
    public void PressOption3()
    {
        Debug.Log("pressed in option 1 from option in battle ");
    }
    public void PressOption4()
    {
        Debug.Log("pressed in option 1 from option in battle ");
    }


    public void GiveSpheres(int playerNumber, List<GameObject> spheresEmpties, int quantitySpheresForTurn = 3)
    {
        RandomHelper random = new RandomHelper();
        int typeSpheres;
        for (int i = 0; i < quantitySpheresForTurn; i++)
        {
            typeSpheres = 0;
            typeSpheres = random.RandomInt(1, quantitySpheresForTurn + 1);
            switch (typeSpheres)
            {
                case 1:
                    spheresEmpties[i].GetComponent<RawImage>().texture = sphereBlue;
                    if (playerNumber == 1)
                    {
                        UserPlayerController.GetInstance().BlueSphereG = UserPlayerController.GetInstance().BlueSphereG + 1;
                    }
                    else
                    {
                        EnemyPlayerController.GetInstance().BlueSphereG = EnemyPlayerController.GetInstance().BlueSphereG + 1;
                    }
                    // Debug.Log("Give sphere blue");
                    break;
                case 2:
                    spheresEmpties[i].GetComponent<RawImage>().texture = sphereYellow;
                    if (playerNumber == 1)
                    {
                        UserPlayerController.GetInstance().YellowSphereG = UserPlayerController.GetInstance().YellowSphereG + 1;
                    }
                    else
                    {
                        EnemyPlayerController.GetInstance().YellowSphereG = EnemyPlayerController.GetInstance().YellowSphereG + 1;
                    }

                    // Debug.Log("Give sphere yellow");
                    break;
                case 3:
                    spheresEmpties[i].GetComponent<RawImage>().texture = sphereRed;
                    if (playerNumber == 1)
                    {
                        UserPlayerController.GetInstance().RedSphereG = UserPlayerController.GetInstance().RedSphereG + 1;
                    }
                    else
                    {
                        EnemyPlayerController.GetInstance().RedSphereG = EnemyPlayerController.GetInstance().RedSphereG + 1;
                    }

                    // Debug.Log("Give sphere red");
                    break;
                case 4:
                    // Debug.Log("Nothing nothing nothing");
                    break;

            }
        }
    }

    public void StartCoroutineUtil(Vector3 positionToFloating, string text, Color32 startColorA, Color32 endColorA)
    {
        StartCoroutine(FloatingText(positionToFloating, text, startColorA, endColorA));
    }
    public IEnumerator FloatingText(Vector3 positionToFloating, string text, Color32 startColorA, Color32 endColorA)
    {
        // adwa
        Color32 startColor = startColorA;
        Color32 endColor = endColorA;

        GameObject go = Instantiate(objectTextFloating, positionToFloating, Quaternion.identity);
        go.transform.Rotate(Camera.main.transform.localRotation.eulerAngles.x, go.transform.localRotation.eulerAngles.y, go.transform.localRotation.eulerAngles.z);
        go.GetComponent<TMP_Text>().text = text;
        go.GetComponent<TMP_Text>().color = startColor;
        float speed = 1.0f;
        float step = speed * Time.deltaTime;
        float t = 0;
        Vector3 vectorTarget = go.transform.position + new Vector3(0, 0, 1);
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
}
