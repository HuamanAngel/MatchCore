using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReaderA
{
    public static Skills GetAllSkills()
    {
        string jsonString = Resources.Load<TextAsset>("Files/SkillJson").ToString();
        Skills allSkills = JsonUtility.FromJson<Skills>(jsonString);
        return allSkills;
    }

    public static Skill SearchSkillById(int idSkill)
    {
        string jsonString = Resources.Load<TextAsset>("Files/SkillJson").ToString();
        Skills allSkills = JsonUtility.FromJson<Skills>(jsonString);
        foreach(Skill skill  in allSkills.allSkill)
        {
            if(skill.id == idSkill)
            {
                return skill;
            }
        }    
        Debug.Log("Skill not found");    
        return null;
    }

    public static ModelCharacts GetAllCharacter()
    {
        string jsonString = Resources.Load<TextAsset>("Files/CharacterJson").ToString();
        ModelCharacts allData = JsonUtility.FromJson<ModelCharacts>(jsonString);
        return allData;
    }

    public static Charac SearchCharacterById(int idCharacter)
    {
        string jsonString = Resources.Load<TextAsset>("Files/CharacterJson").ToString();
        ModelCharacts allData = JsonUtility.FromJson<ModelCharacts>(jsonString);
        foreach(Charac character  in allData.data)
        {
            if(character.id == idCharacter)
            {
                return character;
            }
        }    
        Debug.Log("Character not found");    
        return null;
    }

}
