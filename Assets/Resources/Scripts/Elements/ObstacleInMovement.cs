using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInMovement : ElementInteractuableBase
{
    public ElementsInteractuable.OptionObstacle typeObstacleOverHere;
    private void OnMouseDown()
    {
        bool isConsumedKey = false;
        isConsumedKey = HeroInMovement.GetInstance().ConsumeKey(1, typeObstacleOverHere);
        if (!isConsumedKey)
        {
            Debug.Log("Te faltan mas llaves");
        }
        else
        {
            _isInteractuable = false;            
            StartCoroutine(ProcessDie());
        }
    }

    public IEnumerator ProcessDie()
    {
        _myAnim.SetBool("Open", true);
        typeObstacleOverHere = ElementsInteractuable.OptionObstacle.NOTHING;
        while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            yield return null;
        }
        while (_myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }
        HeroInMovement.GetInstance().TryCreationArrowDirection();
        Destroy(this.gameObject);
    }
}
