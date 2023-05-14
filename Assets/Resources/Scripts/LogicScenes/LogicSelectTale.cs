using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class LogicSelectTale : MonoBehaviour
{
    private static LogicSelectTale _instance;
    public GameObject goInformationMap;
    public GameObject goConfirmationLvl;
    public List<GameObject> goDoorLvls;
    private GameObject goDoorLvlInConfirmation;
    private Chronometer theChronometerObject;
    private int _hourToShowLvlAvaible;
    private int _hourToShowLvlAvaibleFinish;
    private string _whereIsLvl;
    private bool _actuallyShowLvl = false;
    public TMP_Text chronometerTmpText;
    public TMP_Text dayTmpText;
    public GameObject parentWhatContainAllLight;
    public GameObject GoDoorLvlInConfirmation { get => goDoorLvlInConfirmation; set => goDoorLvlInConfirmation = value; }
    private Color32 _colorAlertDownLevel;
    private List<Color32> _colorOld;
    private int _numberStage;
    private void Awake()
    {
        _instance = this;
        _hourToShowLvlAvaible = 18;
        _hourToShowLvlAvaibleFinish = 19;
        _whereIsLvl = "Hidden";
        _colorOld = new List<Color32>();
        _colorAlertDownLevel = new Color32(0xE5, 0x29, 0x1B, 0xFF);
        foreach (Transform child in parentWhatContainAllLight.transform)
        {
            _colorOld.Add(child.GetComponent<Light>().color);
        }
        _numberStage = 1;
    }
    public static LogicSelectTale GetInstance()
    {
        return _instance;
    }
    void Start()
    {
        theChronometerObject = Chronometer.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        chronometerTmpText.text = theChronometerObject.GetInformationHour();
        dayTmpText.text = theChronometerObject.DayText;
        if (_hourToShowLvlAvaible == theChronometerObject.HourActual && !_actuallyShowLvl)
        {
            StartDownLvl();
            _actuallyShowLvl = true;
            _whereIsLvl = "Show";
        }

        if (_hourToShowLvlAvaibleFinish == theChronometerObject.HourActual && _actuallyShowLvl)
        {
            _actuallyShowLvl = false;
            if (_whereIsLvl == "Show")
            {
                StartHiddenLvl();
                _whereIsLvl = "Hidden";
            }
        }
    }
    private void StartDownLvl()
    {
        foreach (GameObject goDoorLvl in goDoorLvls)
        {
            StartCoroutine(ProcessDown(goDoorLvl));
            GameObject levelGo = UtilitiesClass.FindChildByName(goDoorLvl, "Level");
            GameObject entryPointGo = UtilitiesClass.FindChildWithTag(levelGo, "EntryPointLevel");
            GameObject doorGo = UtilitiesClass.FindChildByName(entryPointGo, "door");

            int quantityPositionNumberByMap = 5;
            List<int> numberSceneList = new List<int>();
            // numberSceneList.Add(5);
            // numberSceneList.Add(6);
            numberSceneList = SceneController.ToMaps(1);
            int randomValue = Random.Range(0, numberSceneList.Count + 1);
            if (randomValue == numberSceneList.Count)
            {
                doorGo.GetComponent<DoorInteractuable>().NumberMap = -1;
                doorGo.GetComponent<DoorInteractuable>().PositionNumberMap = -1;
            }
            else
            {
                doorGo.GetComponent<DoorInteractuable>().NumberMap = numberSceneList[randomValue];
                doorGo.GetComponent<DoorInteractuable>().PositionNumberMap = Random.Range(0, quantityPositionNumberByMap + 1);
            }
            doorGo.GetComponent<DoorInteractuable>().HideOrShowDoor();
        }
    }

    public IEnumerator ProcessDown(GameObject go)
    {
        foreach (Transform child in parentWhatContainAllLight.transform)
        {
            child.GetComponent<Light>().color = _colorAlertDownLevel;
        }

        Animator _myAnim = go.GetComponent<Animator>();
        _myAnim.SetBool("DownLvl", true);
        _myAnim.SetBool("ReverseDownLvl", false);

        while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("DownLevel"))
        {
            yield return null;
        }
        while (_myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }
    }

    private void StartHiddenLvl()
    {
        foreach (GameObject goDoorLvl in goDoorLvls)
        {
            StartCoroutine(ProcessHidden(goDoorLvl));
        }
    }

    public IEnumerator ProcessHidden(GameObject go)
    {
        int i = 0;
        foreach (Transform child in parentWhatContainAllLight.transform)
        {
            child.GetComponent<Light>().color = _colorOld[i];
            i++;
        }
        Animator _myAnim = go.GetComponent<Animator>();
        _myAnim.SetBool("DownLvl", false);
        _myAnim.SetBool("ReverseDownLvl", true);

        while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("ReveseDownLevel"))
        {
            yield return null;
        }
        while (_myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }
    }


    public void ContinueOpenDoor()
    {
        goConfirmationLvl.SetActive(false);
        StartCoroutine(goDoorLvlInConfirmation.GetComponent<DoorInteractuable>().ProcessOpen());
    }

    // public void StagesGame()
    // {
    //     switch()
    //     {

    //     }
    //     // Stage 1
    //     // _numberStage
    //     // Stage 2
    //     // Stage 3
    //     // Stage 4
    //     // Stage 5
    //     // Stage Final
    // }

    public void MapByStages(int numberStage)
    {
        int valueRandom = 0;
        int quantityMapRandom = 0;
        int quantityTotalDoorInMap = 3;
        int quantityMinTotalDoorInMap = 1;
        // Scene snow : 5 map, 0 big
        // Scene lava : 5 map, 0 big
        switch (numberStage)
        {
            // SceneManager.LoadScene(4,LoadSceneMode.Single);
            case 1:
                valueRandom = Random.Range(quantityMinTotalDoorInMap, quantityTotalDoorInMap + 1);

                // SceneManager.LoadScene(4,LoadSceneMode.Single);
                break;
            case 2:
                // SceneManager.LoadScene(4,LoadSceneMode.Single);
                break;
            case 3:
                // SceneManager.LoadScene(4,LoadSceneMode.Single);
                break;
            case 4:
                // SceneManager.LoadScene(4,LoadSceneMode.Single);
                break;
            case 5:
                // SceneManager.LoadScene(4,LoadSceneMode.Single);
                break;
            case 6:
                // SceneManager.LoadScene(4,LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }

    public void InWindow(bool state)
    {
        if (state)
        {
            theChronometerObject.Pause();
        }
        else
        {
            theChronometerObject.Continue();
        }
    }

    public void HiddenAllWindowInThisFile()
    {
        goConfirmationLvl.SetActive(false);
        goInformationMap.SetActive(false);
        InWindow(false);
    }

}
