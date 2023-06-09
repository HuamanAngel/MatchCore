using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsInteractuable
{
    // Element what interrumped the movement in map
    public enum OptionObstacle
    {
        NOTHING,
        FENCE_BASIC,
        FENCE_MEDIUM,
        FENCE_BIG,
        FENCE_GRAND_TRANSITION
    }
    public enum OptionReward
    {
        NOTHING,
        CHEST_BASIC,
        CHEST_MEDIUM,
        CHEST_BIG,
        KEY_NEXT_LEVEL,
        FOUNTAIN_BASIC,
        BASKET_BASIC,
        CALDERON_BASIC,
        CALDERON_LONG,
        SWORD_ROCK_1,
        SWORD_ROCK_2,
        SWORD_ROCK_3,
        SWORD_ROCK_4,
        CIRCUS_1,
    }
    public enum OptionTale
    {
        NOTHING,
        TALE_NEWSPAPER
    }

    public static Texture GetTextureByElementInteractuable(ElementsInteractuable.OptionReward typeElementInteractuable)
    {
        Texture textt;
        switch (typeElementInteractuable)
        {
            case ElementsInteractuable.OptionReward.CHEST_BASIC :
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_basic");
                break;
            case ElementsInteractuable.OptionReward.CHEST_MEDIUM:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_medium");
                break;
            case ElementsInteractuable.OptionReward.CHEST_BIG:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_big");
                break;
            case ElementsInteractuable.OptionReward.KEY_NEXT_LEVEL:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_nextlevel");
                break;
            case ElementsInteractuable.OptionReward.FOUNTAIN_BASIC:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_fountain");
                break;
            case ElementsInteractuable.OptionReward.BASKET_BASIC:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_fountain");
                break;
            case ElementsInteractuable.OptionReward.CALDERON_BASIC:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_armor");
                break;                
            case ElementsInteractuable.OptionReward.CALDERON_LONG:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_damage");
                break;                                
            case ElementsInteractuable.OptionReward.CIRCUS_1:
                // Add experiencia all heroes in combat 
                textt = Resources.Load<Texture>("Images/RewardIcons/i_damage");
                break;                                

            case ElementsInteractuable.OptionReward.SWORD_ROCK_1:
            case ElementsInteractuable.OptionReward.SWORD_ROCK_2:
            case ElementsInteractuable.OptionReward.SWORD_ROCK_3:
            case ElementsInteractuable.OptionReward.SWORD_ROCK_4:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_damage");
                break;                                

            default:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_basic");
                break;
        }
        return textt;

    }

}
