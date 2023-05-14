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
    public Light lightDirectional;
    public GameObject GoDoorLvlInConfirmation { get => goDoorLvlInConfirmation; set => goDoorLvlInConfirmation = value; }
    private Color32 _colorAlertDownLevel;
    private List<Color32> _colorOld;
    private int _numberStage;

    // _typeDay = 0 -> Day
    // _typeDay = 1 -> MiddleDay
    // _typeDay = 2 -> Night
    // _typeDay = 3 -> MiddleNight

    private int _typeDay = -1;

    // Colors days 
    public Color32 dayNormal = new Color32(254, 241, 222, 255);
    public Color32 dayBoom = new Color32(252, 209, 144, 255);
    public Color32 nightNormal = new Color32(147, 192, 219, 255);
    public Color32 nightBoom = new Color32(60, 136, 182, 255);
    private float _timeActualHour;
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
        _timeActualHour = theChronometerObject.HourActual;
        lightDirectional.color = CheckColorInTime(_timeActualHour);

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

        // Light
        if (_timeActualHour != theChronometerObject.HourActual)
        {
            _timeActualHour = theChronometerObject.HourActual;
            lightDirectional.color = CheckColorInTime(theChronometerObject.HourActual);
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
    public Color32 CheckColorInTime(float timeHour)
    {
        int hour = (int)timeHour;
        int addHourLight = 0;
        int addHourNight = 0;
        Calendar calendar = theChronometerObject.calendar;
        // Add time for season
        // Se lee de atras para adelante
        // 21 de marzo - 21 de diciembre , 1 hora mas de noche
        // Meses : 12,1,2,3
        // 23 de septiembre - 21 de junio , 1 hora mas de dia
        // Meses : 6,7,8,9
        if (calendar.GetMonth() >= 6 && calendar.GetMonth() <= 9)
        {
            if (calendar.GetMonth() == 6)
            {
                if (calendar.GetDay() >= 21)
                {
                    addHourLight = 1;
                }
            }
            else if (calendar.GetMonth() == 9)
            {
                if (calendar.GetDay() <= 23)
                {
                    addHourLight = 1;
                }
            }
            else
            {
                addHourLight = 1;
            }
        }
        else if (calendar.GetMonth() == 12 || calendar.GetMonth() <= 3)
        {
            if (calendar.GetMonth() == 12)
            {
                if (calendar.GetDay() >= 21)
                {
                    addHourNight = 1;
                }
            }
            else if (calendar.GetMonth() == 3)
            {
                if (calendar.GetDay() <= 21)
                {
                    addHourNight = 1;
                }
            }
            else
            {
                addHourNight = 1;
            }
        }


        // Check special date 
        // Solsticio de verano	21 de junio	La noche mas corta	3 horas menos antes de acabar la noche
        // Solsticio de invierno	21 de diciembre	La noche mas larga	3 horas mas antes de acabar la noche

        if (calendar.GetMonth() == 6 && calendar.GetDay() == 21)
        {
            addHourLight = 3;
        }
        else if (calendar.GetMonth() == 12 && calendar.GetDay() == 21)
        {
            addHourNight = 3;
        }

        // Tiempo
        // Horas de luz normal
        if ((hour >= 8 && hour < 11) || (hour >= 13 && hour < 18 + addHourLight - addHourNight))
        {
            if (_typeDay != 0)
            {
                _typeDay = 0;
                // soundManager.GetComponent<SoundManager>().SetAudioStartDay(true, false);
            }
            return dayNormal;
            // Hora de luz al maximo
        }
        else if (hour >= 11 && hour < 13)
        {
            if (_typeDay != 1)
            {
                _typeDay = 1;
                // soundManager.GetComponent<SoundManager>().SetAudioStartDay(true, false);
            }
            return dayBoom;
            // Hora de noche nornal
        }
        else if ((hour >= 18 + addHourLight - addHourNight && hour < 23) || (hour >= 1 && hour < 8))
        {
            if (_typeDay != 2)
            {
                _typeDay = 2;
                // soundManager.GetComponent<SoundManager>().SetAudioStartNight(true, false);
            }

            return nightNormal;
            // Hora de noche al maximo 
        }
        else if ((hour >= 23 && hour < 24) || (hour >= 0 && hour < 1))
        {
            if (_typeDay != 3)
            {
                _typeDay = 3;
                // soundManager.GetComponent<SoundManager>().SetAudioMiddleNight(true, false);
            }
            return nightBoom;
        }
        else
        {
            if (_typeDay != 3)
            {
                _typeDay = 3;
                // soundManager.GetComponent<SoundManager>().SetAudioMiddleNight(true, false);
            }
            return nightBoom;
        }
    }

}
