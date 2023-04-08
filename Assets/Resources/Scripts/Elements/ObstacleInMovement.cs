using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInMovement : ElementInteractuableBase
{
    public ElementsInteractuable.OptionObstacle typeObstacleOverHere;
    private void OnMouseDown()
    {
        bool isConsumedKey = false;
        bool canInteractiveFromHere = false;
        foreach (GameObject goPointInteractive in _goParent.GetComponent<BrigdeLogic>().pointsInteractuableInCaseExistObstacle)
        {
            if (goPointInteractive.GetComponent<PointInteractive>().HeroIsHere)
            {
                canInteractiveFromHere = true;
                break;
            }
        }
        if (canInteractiveFromHere)
        {
            isConsumedKey = HeroInMovement.GetInstance().ConsumeKey(1, typeObstacleOverHere);
            if (!isConsumedKey)
            {
                Color32 theColor = Color.red;
                Color32 endColor;

                endColor = theColor;
                endColor.a = 0;
                Vector3 positionToFloating = transform.position + new Vector3(0, 2, 0);
                Vector3 theRotation = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, prefabTextFloating.transform.localRotation.eulerAngles.y, prefabTextFloating.transform.localRotation.eulerAngles.z);
                StartCoroutine(EffectText.FloatingTextFadeOut(prefabTextFloating, positionToFloating, "Te faltan mas llaves", theColor, endColor, theRotation));
            }
            else
            {
                _isInteractuable = false;
                StartCoroutine(ProcessDie());
            }
        }
        else
        {
            Color32 theColor = Color.red;
            Color32 endColor;

            endColor = theColor;
            endColor.a = 0;
            Vector3 positionToFloating = transform.position + new Vector3(0, 2, 0);
            Vector3 theRotation = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, prefabTextFloating.transform.localRotation.eulerAngles.y, prefabTextFloating.transform.localRotation.eulerAngles.z);
            StartCoroutine(EffectText.FloatingTextFadeOut(prefabTextFloating, positionToFloating, "Acercate", theColor, endColor, theRotation));

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
