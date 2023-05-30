using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractuable : ElementInteractuableBase
{
    public List<GameObject> goBlockDoor;
    private int _numberMap;
    private int _positionNumberMap;
    public int NumberMap { get => _numberMap; set => _numberMap = value; }
    public int PositionNumberMap { get => _positionNumberMap; set => _positionNumberMap = value; }
    private void OnMouseDown()
    {

        bool canInteractiveFromHere = false;
        float distanceEnabled = 2.5f;
        if (Vector3.Distance(transform.position, HeroControllerInSelectMap.GetInstance().transform.position) <= 2.5f)
        {
            canInteractiveFromHere = true;
        }
        if (canInteractiveFromHere)
        {
            LogicSelectTale.GetInstance().goConfirmationLvl.SetActive(true);
            LogicSelectTale.GetInstance().GoDoorLvlInConfirmation = this.gameObject;
            LogicSelectTale.GetInstance().InWindow(true);
        }

        // if (canInteractiveFromHere)
        // {
        //     StartCoroutine(ProcessOpen());
        // }
    }

    public IEnumerator ProcessOpen()
    {
        GameObject parentN = transform.parent.gameObject;
        _myAnim = parentN.GetComponent<Animator>();
        _myAnim.SetBool("Open", true);
        while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            yield return null;
        }
        while (_myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }
        UserController.GetInstance().NumberPositionMap = _positionNumberMap;
        UserController.GetInstance().NumberScene = _numberMap;
        // Change scene
        StartCoroutine(LoadingScreen.LoadAsyncScene(_numberMap));
        // SceneController.ToSceneByNumber(_numberMap);
        yield return null;
    }

    public void HideOrShowDoor()
    {
        if (_numberMap == -1)
        {
            this.gameObject.SetActive(false);
            foreach (GameObject goBlock in goBlockDoor)
            {
                goBlock.SetActive(true);
            }
        }
        else
        {
            this.gameObject.SetActive(true);
            foreach (GameObject goBlock in goBlockDoor)
            {
                goBlock.SetActive(false);
            }
        }
    }
}
