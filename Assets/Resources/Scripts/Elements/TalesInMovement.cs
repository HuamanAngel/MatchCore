using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalesInMovement : ElementInteractuableBase 
{
    public ElementsInteractuable.OptionTale typeTale;
    // Start is called before the first frame update
    public Texture newsPaperImage;
    private void OnMouseDown()
    {
        if (_goParent.GetComponent<PointInteractive>().HeroIsHere)
        {
            StartCoroutine(ProcessShow());
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

    public IEnumerator ProcessShow()
    {
        // Color32 theColor = Color.blue;
        // Color32 endColor;

        // endColor = theColor;
        // endColor.a = 0;
        // Vector3 positionToFloating = transform.position;
        // Vector3 theRotation = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, prefabTextFloating.transform.localRotation.eulerAngles.y, prefabTextFloating.transform.localRotation.eulerAngles.z);
        // _myAnim.SetBool("Open", true);
        // while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        // {
        //     yield return null;
        // }
        // while (_myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        // {
        //     yield return null;
        // }
        LogicGameInMovement.GetInstance().goContainerNewsPaper.SetActive(true);
        LogicGameInMovement.GetInstance().rawImageNewsPaperContainer.texture = newsPaperImage;

        typeTale = ElementsInteractuable.OptionTale.NOTHING;
        yield return null;
        // Destroy(this.gameObject);
    }
}
