using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
[System.Serializable]
public class Charac
{
    public int id;
    public string name;
    public string type;
    public int yellow;
    public int blue;
    public int red;
    public string quality;
    public string movementTypeGridString;
    public int skillId1;
    public int skillId2;
    public int skillId3;
    public int skillId4;
    public int idEffect;
    public int lifeBase;
    public int armorBase;
    public string pathImage;
    public string pathPrefab;
    public string pathPrefabInBattle;
    // Derivated values generates from the preview values
    public AttackGrid.TypeOfAttack movementType;
    public List<Skill> theSkills = new List<Skill>();
    public Texture iconChar;
    public GameObject prefabChar;
    public GameObject prefabCharInBattle;
    public int lvl;
    public int lifeTotal;
    public int armorTotal;

    // public Charac CloneThisClass()
    // {

    // }
    public void InitialValuesDerived()
    {
        CalculateArmor();
        CalculateLife();
        LoadMovementType();
        SetSkillToCharacter();
        LoadMyResources();
    }
    public void RecalculateValuesDerived()
    {
        CalculateArmor();
        CalculateLife();
        // SetSkillToCharacter();
    }
    public void CalculateLife()
    {
        lifeTotal = lifeBase + lifeBase * lvl / 4;
    }
    public void CalculateArmor()
    {
        armorTotal = armorBase + armorBase + 1 * lvl;
    }

    public void LvlUp()
    {
        lvl = lvl + 1;
        CalculateLife();
        CalculateArmor();
    }

    public void LoadMovementType()
    {
        movementType = (AttackGrid.TypeOfAttack)System.Enum.Parse(typeof(AttackGrid.TypeOfAttack), movementTypeGridString);
    }


    public void SetSkillToCharacter()
    {
        Texture imageText;
        Sprite imageSpr;
        GameObject goEffect;
        if (skillId1 != 0)
        {
            theSkills.Add(JsonReaderA.SearchSkillById(skillId1));
            theSkills[0].attackType = (AttackGrid.TypeOfAttack)System.Enum.Parse(typeof(AttackGrid.TypeOfAttack), theSkills[0].attackTypeGridString);

            imageText = Resources.Load<Texture>(theSkills[0].pathIcon);
            imageSpr = Resources.Load<Sprite>(theSkills[0].pathIcon);
            goEffect = Resources.Load<GameObject>(theSkills[0].pathEffectPrefab);

            theSkills[0].iconSkill = imageText;
            theSkills[0].iconSkillSprite = imageSpr;
            theSkills[0].effectPrefab = goEffect;
        }
        if (skillId2 != 0)
        {
            theSkills.Add(JsonReaderA.SearchSkillById(skillId2));
            theSkills[1].attackType = (AttackGrid.TypeOfAttack)System.Enum.Parse(typeof(AttackGrid.TypeOfAttack), theSkills[1].attackTypeGridString);
            imageText = Resources.Load<Texture>(theSkills[1].pathIcon);
            imageSpr = Resources.Load<Sprite>(theSkills[1].pathIcon);
            goEffect = Resources.Load<GameObject>(theSkills[1].pathEffectPrefab);

            theSkills[1].iconSkill = imageText;
            theSkills[1].iconSkillSprite = imageSpr;
            theSkills[1].effectPrefab = goEffect;
        }
        if (skillId3 != 0)
        {
            theSkills.Add(JsonReaderA.SearchSkillById(skillId3));
            theSkills[2].attackType = (AttackGrid.TypeOfAttack)System.Enum.Parse(typeof(AttackGrid.TypeOfAttack), theSkills[2].attackTypeGridString);
            imageText = Resources.Load<Texture>(theSkills[2].pathIcon);
            imageSpr = Resources.Load<Sprite>(theSkills[2].pathIcon);
            goEffect = Resources.Load<GameObject>(theSkills[2].pathEffectPrefab);

            theSkills[2].iconSkill = imageText;
            theSkills[2].iconSkillSprite = imageSpr;
            theSkills[2].effectPrefab = goEffect;

        }
        if (skillId4 != 0)
        {
            theSkills.Add(JsonReaderA.SearchSkillById(skillId4));
            theSkills[3].attackType = (AttackGrid.TypeOfAttack)System.Enum.Parse(typeof(AttackGrid.TypeOfAttack), theSkills[3].attackTypeGridString);
            imageText = Resources.Load<Texture>(theSkills[3].pathIcon);
            imageSpr = Resources.Load<Sprite>(theSkills[3].pathIcon);
            goEffect = Resources.Load<GameObject>(theSkills[3].pathEffectPrefab);

            theSkills[3].iconSkill = imageText;
            theSkills[3].iconSkillSprite = imageSpr;
            theSkills[3].effectPrefab = goEffect;
        }
    }
    public void LoadMyResources()
    {
        Texture textt;
        GameObject go;
        GameObject go2;

        textt = Resources.Load<Texture>(pathImage);
        go = Resources.Load<GameObject>(pathPrefab);
        go2 = Resources.Load<GameObject>(pathPrefabInBattle);

        prefabCharInBattle = go2;
        iconChar = textt;
        prefabChar = go;
    }
}
