using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelCharacts
{
    // El nombre de la variable debe ser igual al que esta en el json
    public Charac[] data;
    // Variables del metodo
    public List<Charac> SearchCharacterByTypeAndQuality(string type, string quality)
    {
        List<Charac> theCharactersSelects = new List<Charac>();
        foreach (Charac character in data)
        {
            if (character.quality == quality && character.type == type)
            {
                theCharactersSelects.Add(character);
            }
        }
        return theCharactersSelects;
    }
    public Charac SearchCharacterById(int idCharacter)
    {
        foreach (Charac character in data)
        {
            if (character.id == idCharacter)
            {
                return character;
            }
        }
        Debug.Log("Character not found");
        return null;
    }
}
