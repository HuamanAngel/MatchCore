using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentChest
{
    public enum ContentChestType
    {
        REWARD_KEY_BASIC,
        REWARD_KEY_MEDIUM,
        REWARD_KEY_BIG,
        REWARD_MOVEMENT,
    
    }

    public int id;
    public ContentChestType typeReward = ContentChestType.REWARD_KEY_BASIC;
    public string nameItem = "none";
    public int probability = 12;
    public int quantityMin = 0;
    public int quantityMax = 0;
    public static List<ContentChest> typeContentChest;
    public ContentChest(ContentChestType typeReward, int id, string name, int probability, int quantityMin, int quantityMax)
    {
        this.typeReward = typeReward;
        this.id = id;
        this.nameItem = name;
        this.probability = probability;
        this.quantityMin = quantityMin;
        this.quantityMax = quantityMax;
    }
    public int CalculateGiveReward()
    {
        int valueRandom = Random.Range(0, 101);
        if (probability >= valueRandom)
        {
            int randomRewardItem = Random.Range(quantityMin, quantityMax + 1);
            return randomRewardItem;
        }
        else
        {
            return 0;
        }
    }

    public Texture GetTextureFromIcon()
    {
        Texture textt;
        switch (typeReward)
        {
            case ContentChest.ContentChestType.REWARD_KEY_BASIC:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_basic");
                break;
            case ContentChest.ContentChestType.REWARD_KEY_MEDIUM:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_medium");
                break;
            case ContentChest.ContentChestType.REWARD_KEY_BIG:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_big");
                break;
            case ContentChest.ContentChestType.REWARD_MOVEMENT:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_movement");
                break;
            default:
                textt = Resources.Load<Texture>("Images/RewardIcons/i_key_basic");
                break;
        }
        return textt;
    }

}
