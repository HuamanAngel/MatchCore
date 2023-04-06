using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardInMovement : ElementInteractuableBase
{
    public ElementsInteractuable.OptionReward typeReward;
    private void OnMouseDown()
    {
        if (_goParent.GetComponent<PointInteractive>().HeroIsHere)
        {
            StartCoroutine(ProcessDie());
        }
        else
        {
            Color32 theColor = Color.red;
            Color32 endColor;

            endColor = theColor;
            endColor.a = 0;
            Vector3 positionToFloating = transform.position +  new Vector3(0,2,0);
            Vector3 theRotation = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, prefabTextFloating.transform.localRotation.eulerAngles.y, prefabTextFloating.transform.localRotation.eulerAngles.z);
            StartCoroutine(EffectText.FloatingTextFadeOut(prefabTextFloating, positionToFloating, "Acercate", theColor, endColor, theRotation));
        }
    }

    public IEnumerator ProcessDie()
    {
        _myAnim.SetBool("Open", true);
        typeReward = ElementsInteractuable.OptionReward.NOTHING;
        while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            yield return null;
        }
        while (_myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
