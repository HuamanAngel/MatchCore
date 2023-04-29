using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicSelectTale : MonoBehaviour
{
    private static LogicSelectTale _instance;
    public GameObject goInformationMap;
    public GameObject goConfirmationLvl;
    public List<GameObject> goDoorLvls;
    private GameObject goDoorLvlInConfirmation;
    public GameObject GoDoorLvlInConfirmation { get => goDoorLvlInConfirmation; set => goDoorLvlInConfirmation = value; }
    private void Awake()
    {
        _instance = this;
    }
    public static LogicSelectTale GetInstance()
    {
        return _instance;
    }
    void Start()
    {
        StartDownLvl();
    }

    // Update is called once per frame
    void Update()
    {

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
        while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("DownLevel"))
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
