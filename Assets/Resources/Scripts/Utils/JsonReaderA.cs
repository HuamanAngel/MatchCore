using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReaderA
{
    private static string locationOfFileSkillJson = "Files/SkillJson";
    private static string locationOfFileCharacterJson = "Files/CharacterJson";
    public List<Charac> allCharacters = new List<Charac>();
    public static Skills GetAllSkills()
    {
        string jsonString = Resources.Load<TextAsset>(locationOfFileSkillJson).ToString();
        Skills allSkills = JsonUtility.FromJson<Skills>(jsonString);
        return allSkills;
    }

    public static Skill SearchSkillById(int idSkill)
    {
        string jsonString = Resources.Load<TextAsset>(locationOfFileSkillJson).ToString();
        Skills allSkills = JsonUtility.FromJson<Skills>(jsonString);
        foreach (Skill skill in allSkills.allSkill)
        {
            if (skill.id == idSkill)
            {
                return skill;
            }
        }
        Debug.Log("Skill not found");
        return null;
    }

    public static ModelCharacts GetAllCharacter()
    {
        string jsonString = Resources.Load<TextAsset>(locationOfFileCharacterJson).ToString();
        ModelCharacts allData = JsonUtility.FromJson<ModelCharacts>(jsonString);
        return allData;
    }
}
