using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBuff : MonoBehaviour
{
    // Start is called before the first frame update
    public enum BuffOption
    {
        BUFF_INCREMENT_LIFE,
        BUFF_INCREMENT_ARMOR,
        BUFF_INCREMENT_DAMAGE,
        BUFF_DECREMENT_LIFE,
        BUFF_DECREMENT_ARMOR,
        BUFF_DECREMENT_DAMAGE,
    }    
    public static Texture GetTextureByElementBuff(ElementBuff.BuffOption typeElementBuff)
    {
        Texture textt;
        switch (typeElementBuff)
        {
            case ElementBuff.BuffOption.BUFF_INCREMENT_LIFE :
                textt = Resources.Load<Texture>("Images/Buff/Buff_Life_up");
                break;
            case ElementBuff.BuffOption.BUFF_INCREMENT_ARMOR:
                textt = Resources.Load<Texture>("Images/Buff/Buff_Armor_up");
                break;
            case ElementBuff.BuffOption.BUFF_INCREMENT_DAMAGE:
                textt = Resources.Load<Texture>("Images/Buff/Buff_Life_up");
                break;
            case ElementBuff.BuffOption.BUFF_DECREMENT_LIFE:
                textt = Resources.Load<Texture>("Images/Buff/Buff_Armor_down");
                break;
            case ElementBuff.BuffOption.BUFF_DECREMENT_ARMOR:
                textt = Resources.Load<Texture>("Images/Buff/Buff_Life_down");
                break;
            case ElementBuff.BuffOption.BUFF_DECREMENT_DAMAGE:
                textt = Resources.Load<Texture>("Images/Buff/Buff_Life_up");
                break;
            default:
                textt = Resources.Load<Texture>("Images/Buff/Buff_Life_up");
                break;
        }
        return textt;

    }
}
