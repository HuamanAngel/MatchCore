using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardInMovement : ElementInteractuableBase
{
    public DirectionMove.OptionMovements _theDirection;
    public DirectionMove.OptionMovements TheDirection { get => _theDirection; set => value = _theDirection; }
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
            Vector3 positionToFloating = transform.position + new Vector3(0, 2, 0);
            Vector3 theRotation = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, prefabTextFloating.transform.localRotation.eulerAngles.y, prefabTextFloating.transform.localRotation.eulerAngles.z);
            StartCoroutine(EffectText.FloatingTextFadeOut(prefabTextFloating, positionToFloating, "Acercate", theColor, endColor, theRotation));
        }
    }

    public IEnumerator ProcessDie()
    {
        HeroInMovement.GetInstance().ClearAllArrows();

        Color32 theColor = Color.blue;
        Color32 endColor;

        endColor = theColor;
        endColor.a = 0;
        Vector3 positionToFloating = transform.position;
        Vector3 theRotation = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, prefabTextFloating.transform.localRotation.eulerAngles.y, prefabTextFloating.transform.localRotation.eulerAngles.z);
        _myAnim.SetBool("Open", true);
        while (!_myAnim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            yield return null;
        }
        while (_myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }
        LogicGameInMovement.GetInstance().containerCard.SetActive(true);
        LogicGameInMovement.GetInstance().panelCard.SetActive(true);
        if (typeReward != ElementsInteractuable.OptionReward.CHEST_BASIC && typeReward != ElementsInteractuable.OptionReward.CHEST_MEDIUM && typeReward != ElementsInteractuable.OptionReward.CHEST_BIG)
        {
            if(typeReward == ElementsInteractuable.OptionReward.FOUNTAIN_BASIC )
            {
                GameObject goR = CreateCardReward(ElementsInteractuable.GetTextureByElementInteractuable(typeReward), "1", new Vector3(0, 0, 0));
                // Restore Life 20%
                // 
            }else
            {
                GameObject goR = CreateCardReward(ElementsInteractuable.GetTextureByElementInteractuable(typeReward), "1", new Vector3(0, 0, 0));
            }

            // behaviar ass element only in the map
        }
        else
        {
            // behaviar ass chest

            Dictionary<ContentChest.ContentChestType, ContentChest> theReward = LogicGameInMovement.GetInstance().allContentFromChest[typeReward];
            Dictionary<ContentChest.ContentChestType, int> allRewardShowInCanvas = new Dictionary<ContentChest.ContentChestType, int>();

            int quantityAddKeyBasic = theReward[ContentChest.ContentChestType.REWARD_KEY_BASIC].CalculateGiveReward();
            int quantityAddKeyMedium = theReward[ContentChest.ContentChestType.REWARD_KEY_MEDIUM].CalculateGiveReward();
            int quantityAddKeyBig = theReward[ContentChest.ContentChestType.REWARD_KEY_BIG].CalculateGiveReward();
            int quantityAddMovements = theReward[ContentChest.ContentChestType.REWARD_MOVEMENT].CalculateGiveReward();

            int quantityCard = 0;


            if (quantityAddKeyBasic != 0)
            {
                allRewardShowInCanvas[ContentChest.ContentChestType.REWARD_KEY_BASIC] = quantityAddKeyBasic;
                GameObject goR = CreateCardReward(theReward[ContentChest.ContentChestType.REWARD_KEY_BASIC].GetTextureFromIcon(), "" + quantityAddKeyBasic, GetAditionalPosition(quantityCard));
                quantityCard++;
            }
            if (quantityAddKeyMedium != 0)
            {
                allRewardShowInCanvas[ContentChest.ContentChestType.REWARD_KEY_MEDIUM] = quantityAddKeyMedium;
                GameObject goR = CreateCardReward(theReward[ContentChest.ContentChestType.REWARD_KEY_MEDIUM].GetTextureFromIcon(), "" + quantityAddKeyMedium, GetAditionalPosition(quantityCard));
                quantityCard++;
            }
            if (quantityAddKeyBig != 0)
            {

                allRewardShowInCanvas[ContentChest.ContentChestType.REWARD_KEY_BIG] = quantityAddKeyBig;
                GameObject goR = CreateCardReward(theReward[ContentChest.ContentChestType.REWARD_KEY_BIG].GetTextureFromIcon(), "" + quantityAddKeyBig, GetAditionalPosition(quantityCard));
                quantityCard++;
            }
            if (quantityAddMovements != 0)
            {
                allRewardShowInCanvas[ContentChest.ContentChestType.REWARD_MOVEMENT] = quantityAddMovements;
                GameObject goR = CreateCardReward(theReward[ContentChest.ContentChestType.REWARD_MOVEMENT].GetTextureFromIcon(), "" + quantityAddMovements, GetAditionalPosition(quantityCard));
                quantityCard++;
            }
            HeroInMovement.GetInstance().ModifyValuesFromHero(addKeyBasic: quantityAddKeyBasic, addKeyMedium: quantityAddKeyMedium, addKeyBig: quantityAddKeyBig, addMovement: quantityAddMovements);
        }


        switch (typeReward)
        {
            case ElementsInteractuable.OptionReward.CHEST_BASIC:
                positionToFloating += new Vector3(0, 0, 0);
                yield return StartCoroutine(EffectText.FloatingTextFadeOut(prefabTextFloating, positionToFloating, "Abierto 1", theColor, endColor, theRotation));
                break;
            case ElementsInteractuable.OptionReward.CHEST_MEDIUM:
                positionToFloating += new Vector3(0, 0, 0);
                yield return StartCoroutine(EffectText.FloatingTextFadeOut(prefabTextFloating, positionToFloating, "Abierto 2", theColor, endColor, theRotation));
                break;
            case ElementsInteractuable.OptionReward.CHEST_BIG:
                positionToFloating += new Vector3(0, 0, 0);
                yield return StartCoroutine(EffectText.FloatingTextFadeOut(prefabTextFloating, positionToFloating, "Abierto 3", theColor, endColor, theRotation));
                break;


            case ElementsInteractuable.OptionReward.KEY_NEXT_LEVEL:
                HeroInMovement.GetInstance().ModifyValuesFromHero(addKeyNextLvl: 1);
                positionToFloating += new Vector3(0, 0, 0);
                yield return StartCoroutine(EffectText.FloatingTextFadeOut(prefabTextFloating, positionToFloating, "Llave obtenida", theColor, endColor, theRotation));
                break;
        }
        typeReward = ElementsInteractuable.OptionReward.NOTHING;
        // Aditional action
        this.gameObject.SetActive(false);
        Debug.Log("/////Remove init");
        foreach(var a in _goParent.GetComponent<PointInteractive>().TreasuresAround)
        {
            Debug.Log(a.Key);
        } 
        Debug.Log("/////Remove init");
        Debug.Log("Elemento to remove : " + _theDirection);

        // _goParent.GetComponent<PointInteractive>().TreasuresAround = _goParent.GetComponent<PointInteractive>().TreasuresAround.Remove(_theDirection);
        _goParent.GetComponent<PointInteractive>().RemoveTreasureByKey(_theDirection);
        Debug.Log("/////Remove end");
        foreach(var a in _goParent.GetComponent<PointInteractive>().TreasuresAround)
        {
            Debug.Log(a.Key);
        } 
        Debug.Log("/////Remove end");
        HeroInMovement.GetInstance().TryCreationArrowDirection();
        Destroy(this.gameObject);
    }

    public GameObject CreateCardReward(Texture theTexture, string quantityText, Vector3 aditionalPos)
    {
        GameObject goInstanceCard = Instantiate(LogicGameInMovement.GetInstance().cardRewardBasic);
        goInstanceCard.transform.SetParent(LogicGameInMovement.GetInstance().panelCard.transform);
        goInstanceCard.transform.localScale = new Vector3(1, 1, 1);
        goInstanceCard.transform.localPosition = new Vector3(0, 0, 0) + aditionalPos;
        GameObject goRewardCard = UtilitiesClass.FindChildByName(goInstanceCard, "Marco");
        GameObject goBackgroundCard = UtilitiesClass.FindChildByName(goRewardCard, "Background");
        GameObject goImageReward = UtilitiesClass.FindChildByName(goBackgroundCard, "ImageReward");
        GameObject goQuantityReward = UtilitiesClass.FindChildByName(goBackgroundCard, "Quantity");


        goImageReward.GetComponent<RawImage>().texture = theTexture;
        goQuantityReward.GetComponent<TMP_Text>().text = "x" + quantityText;
        return goInstanceCard;
    }
    public Vector3 GetAditionalPosition(int numCard)
    {
        if (numCard == 0)
        {
            return new Vector3(0, 0, 0);
        }
        else if (numCard == 1)
        {
            return new Vector3(150, 0, 0);

        }
        else if (numCard == 2)
        {
            return new Vector3(-150, 0, 0);

        }
        else if (numCard == 3)
        {
            return new Vector3(300, 0, 0);

        }
        else if (numCard == 4)
        {
            return new Vector3(-300, 0, 0);

        }
        else if (numCard == 5)
        {
            return new Vector3(450, 0, 0);

        }
        else if (numCard == 6)
        {
            return new Vector3(-450, 0, 0);

        }
        return Vector3.zero;
    }
}
