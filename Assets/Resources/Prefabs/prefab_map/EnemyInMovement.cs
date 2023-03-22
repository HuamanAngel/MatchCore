using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInMovement : MonoBehaviour
{
    public List<Charac> theCharacters;
    void Start()
    {
        theCharacters = new List<Charac>();

        Charac theNewCharacter = JsonReaderA.SearchCharacterById(1);
        theNewCharacter.lvl = 1;
        theNewCharacter.InitialValuesDerived();


        // Charac theNewCharacter2 = JsonReaderA.SearchCharacterById(1);
        // theNewCharacter2.lvl = 1;
        // theNewCharacter2.InitialValuesDerived();

        theCharacters.Add(theNewCharacter);
        // theCharacters.Add(theNewCharacter2);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
