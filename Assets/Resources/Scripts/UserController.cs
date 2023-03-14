using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserController : MonoBehaviour
{
    private static UserController _instance;
    public User user;
    public User userEnemy;
    public static UserController GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);        
        user = new User();
        userEnemy = new User();
        _instance = this;


        Charac oneCharacter = JsonReaderA.SearchCharacterById(1);
        Charac secondCharacter = JsonReaderA.SearchCharacterById(3);

        oneCharacter.lvl = 1;
        oneCharacter.InitialValuesDerived();
        secondCharacter.lvl = 4;
        secondCharacter.InitialValuesDerived();
        // RecalculateValuesDerived
        // Add to user data
        user.CharInCombat.Add(oneCharacter);
        user.CharInCombat.Add(secondCharacter);
        user.CharAll.Add(oneCharacter);
        user.CharAll.Add(secondCharacter);

        user.LvlGeneral = 1;
        user.Name = "Oxipusio";
        user.Money = 0;


        // Enemies




        Charac oneCharacterE = JsonReaderA.SearchCharacterById(1);
        Charac secondCharacterE = JsonReaderA.SearchCharacterById(3);
        Charac thirdCharacterE = JsonReaderA.SearchCharacterById(1);

        oneCharacterE.lvl = 1;
        oneCharacterE.InitialValuesDerived();
        secondCharacterE.lvl = 4;
        secondCharacterE.InitialValuesDerived();
        secondCharacterE.lvl = 10;
        thirdCharacterE.InitialValuesDerived();
        // RecalculateValuesDerived
        // Add to user data
        userEnemy.CharInCombat.Add(oneCharacterE);
        userEnemy.CharInCombat.Add(secondCharacterE);
        userEnemy.CharInCombat.Add(thirdCharacterE);
        userEnemy.CharAll.Add(oneCharacterE);
        userEnemy.CharAll.Add(secondCharacterE);
        userEnemy.CharAll.Add(thirdCharacterE);

        userEnemy.LvlGeneral = 1;
        userEnemy.Name = "The enemies";
        userEnemy.Money = 0;


    }
    // Start is called before the first frame update
    void Start()
    {


        // Debug.Log("aca el armor total + " + user.CharInCombat[0].armorTotal);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
