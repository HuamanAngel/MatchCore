using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CharacterUiOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // private bool mouse_over = false;
    private bool isActive = false;
    public bool IsActive { get => isActive; set => isActive = value; }
    private Color colorDefault = Color.white;
    void Update()
    {
        if (!GameManager.instance.GameOver)
        {
            if (LogicGame.GetInstance().IsCurrentSelectedSkill)
            {
                isActive = true;
            }
            else
            {
                GetComponent<Image>().color = colorDefault;
                isActive = false;
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActive)
        {
            GetComponent<Image>().color = Color.red;
            // mouse_over = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isActive)
        {
            GetComponent<Image>().color = colorDefault;
            // mouse_over = false;
        }
    }
    public void OnPointerClick(PointerEventData data)
    {
        if (isActive)
        {
            LogicGame.GetInstance().IsCurrentSelectedCharacterToAttack = true;
            // LogicGame.GetInstance().ProcessAttackSelected(data.rawPointerPress.transform.parent.gameObject);
            // LogicGame.GetInstance().ButtonCharacterSelectedToAttack = data.rawPointerPress.transform.parent.gameObject;
            Debug.Log("OnPointerClick parent. + " + data.rawPointerPress.transform.parent.gameObject);
        }
    }
}
