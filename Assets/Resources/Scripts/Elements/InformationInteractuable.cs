using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationInteractuable : ElementInteractuableBase
{
    private void OnMouseDown()
    {

        // bool isConsumedKey = false;
        bool canInteractiveFromHere = false;
        float distanceEnabled = 2.5f;
        if (Vector3.Distance(transform.position,HeroControllerInSelectMap.GetInstance().transform.position) <= 2.5f)
        {
            canInteractiveFromHere = true;
        }
        if (canInteractiveFromHere)
        {
            LogicSelectTale.GetInstance().goInformationMap.SetActive(true);          
            LogicSelectTale.GetInstance().InWindow(true);
        }
    }
}
