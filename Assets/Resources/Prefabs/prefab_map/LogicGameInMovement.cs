using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogicGameInMovement : MonoBehaviour
{
    public static LogicGameInMovement _instance;
    public GameObject containerCard;
    public GameObject panelCard;
    public GameObject cardRewardBasic;
    public GameObject cardRewardMedium;
    public GameObject cardRewardBig;
    public GameObject onlyOneReward;
    public GameObject goContainerNewsPaper;
    public RawImage rawImageNewsPaperContainer;
    public Dictionary<ElementsInteractuable.OptionReward, Dictionary<ContentChest.ContentChestType, ContentChest>> allContentFromChest;
    // Start is called before the first frame update
    public static LogicGameInMovement GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        allContentFromChest = new Dictionary<ElementsInteractuable.OptionReward, Dictionary<ContentChest.ContentChestType, ContentChest>>();
        CreateTypeContentInChest();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateTypeContentInChest()
    {
        // ChestBasic


        Dictionary<ContentChest.ContentChestType, ContentChest> itemInChest;
        itemInChest = new Dictionary<ContentChest.ContentChestType, ContentChest>();
        ContentChest keyBasic = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_BASIC, 1, "KeyBasic", 100, 1, 2);
        ContentChest keyMedium = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_MEDIUM, 2, "KeyMedium", 5, 1, 1);
        ContentChest keyBig = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_BIG, 3, "KeyBig", 0, 1, 2);
        ContentChest movements = new ContentChest(ContentChest.ContentChestType.REWARD_MOVEMENT, 4, "Movements", 100, 2, 4);
        itemInChest[ContentChest.ContentChestType.REWARD_KEY_BASIC] = keyBasic;
        itemInChest[ContentChest.ContentChestType.REWARD_KEY_MEDIUM] = keyMedium;
        itemInChest[ContentChest.ContentChestType.REWARD_KEY_BIG] = keyBig;
        itemInChest[ContentChest.ContentChestType.REWARD_MOVEMENT] = movements;


        Dictionary<ContentChest.ContentChestType, ContentChest> itemInChest2;
        itemInChest2 = new Dictionary<ContentChest.ContentChestType, ContentChest>();
        ContentChest keyBasic2 = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_BASIC, 1, "KeyBasic", 100, 1, 2);
        ContentChest keyMedium2 = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_MEDIUM, 2, "KeyMedium", 5, 1, 1);
        ContentChest keyBig2 = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_BIG, 3, "KeyBig", 0, 1, 2);
        ContentChest movements2 = new ContentChest(ContentChest.ContentChestType.REWARD_MOVEMENT, 4, "Movements", 100, 2, 4);
        itemInChest2[ContentChest.ContentChestType.REWARD_KEY_BASIC] = keyBasic2;
        itemInChest2[ContentChest.ContentChestType.REWARD_KEY_MEDIUM] = keyMedium2;
        itemInChest2[ContentChest.ContentChestType.REWARD_KEY_BIG] = keyBig2;
        itemInChest2[ContentChest.ContentChestType.REWARD_MOVEMENT] = movements2;


        Dictionary<ContentChest.ContentChestType, ContentChest> itemInChest3;
        itemInChest3 = new Dictionary<ContentChest.ContentChestType, ContentChest>();
        ContentChest keyBasic3 = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_BASIC, 1, "KeyBasic", 100, 1, 2);
        ContentChest keyMedium3 = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_MEDIUM, 2, "KeyMedium", 5, 1, 1);
        ContentChest keyBig3 = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_BIG, 3, "KeyBig", 0, 1, 2);
        ContentChest movements3 = new ContentChest(ContentChest.ContentChestType.REWARD_MOVEMENT, 4, "Movements", 100, 2, 4);
        itemInChest3[ContentChest.ContentChestType.REWARD_KEY_BASIC] = keyBasic3;
        itemInChest3[ContentChest.ContentChestType.REWARD_KEY_MEDIUM] = keyMedium3;
        itemInChest3[ContentChest.ContentChestType.REWARD_KEY_BIG] = keyBig3;
        itemInChest3[ContentChest.ContentChestType.REWARD_MOVEMENT] = movements3;


        // Dictionary<ContentChest.ContentChestType, ContentChest> itemOnly;
        // itemOnly = new Dictionary<ContentChest.ContentChestType, ContentChest>();
        // ContentChest keyBasic3 = new ContentChest(ContentChest.ContentChestType.REWARD_KEY_BAS, 1, "KeyBasic", 100, 1, 2);
        // itemInChest3[ContentChest.ContentChestType.REWARD_KEY_BASIC] = keyBasic3;



        allContentFromChest[ElementsInteractuable.OptionReward.CHEST_BASIC] = itemInChest;
        allContentFromChest[ElementsInteractuable.OptionReward.CHEST_MEDIUM] = itemInChest2;
        allContentFromChest[ElementsInteractuable.OptionReward.CHEST_BIG] = itemInChest3;
        // allContentFromChest[ElementsInteractuable.OptionReward.KEY_NEXT_LEVEL] = itemInChest3;
    }

    
}
