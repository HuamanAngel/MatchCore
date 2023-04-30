using System.Collections;
using System.Collections.Generic;
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
    public GameObject GoDoorLvlInConfirmation { get => goDoorLvlInConfirmation; set => goDoorLvlInConfirmation = value; }

    private void Awake()
    {
        _instance = this;
        _hourToShowLvlAvaible = 18;
        _hourToShowLvlAvaibleFinish = 19;
        _whereIsLvl = "Hidden";
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
        dayTmpText.text  = theChronometerObject.DayText;
        if(_hourToShowLvlAvaible == theChronometerObject.HourActual && !_actuallyShowLvl)
        {
            StartDownLvl();    
            _actuallyShowLvl = true;
            _whereIsLvl = "Show";
        }

        if(_hourToShowLvlAvaibleFinish == theChronometerObject.HourActual && _actuallyShowLvl)
        {
            _actuallyShowLvl = false;
            if(_whereIsLvl == "Show")
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
        }
    }

    public IEnumerator ProcessDown(GameObject go)
    {
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



}
