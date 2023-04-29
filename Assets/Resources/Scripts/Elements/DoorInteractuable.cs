using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractuable : ElementInteractuableBase
{
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
        // SceneController.ToTale1();
        yield return null;
    }

}
