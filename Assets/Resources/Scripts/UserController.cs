using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserController : MonoBehaviour
{
    private static UserController _instance;
    private StatesInBattle _stateInBattle;
    public User user;
    public User userEnemy;
    public StatesInBattle StateInBattle { get => _stateInBattle; set => _stateInBattle = value; }
    public static UserController GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (UserController.GetInstance() != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        user = new User();
        userEnemy = new User();
        _stateInBattle = new StatesInBattle();

        Charac oneCharacter = CreateNewCharacterInScene(3, 1);
        // Add to user data
        // Debug.Log("IN HERO : " + oneCharacter );
        user.CharInCombat.Add(oneCharacter);
        user.CharAll.Add(oneCharacter);

        user.LvlGeneral = 1;
        user.Name = "Oxipusio";
        user.Money = 0;
        // Debug.Log("Aca user poquito antes de controller  : " + user.CharInCombat[0].theSkills.Count);

        // Enemies


        Charac oneCharacterE = CreateNewCharacterInScene(1, 1);
        // Charac secondCharacterE = CreateNewCharacterInScene(3,4);
        // Charac thirdCharacterE = CreateNewCharacterInScene(1,10);

        // RecalculateValuesDerived
        // Add to user data
        userEnemy.CharInCombat.Add(oneCharacterE);
        // userEnemy.CharInCombat.Add(secondCharacterE);
        // userEnemy.CharInCombat.Add(thirdCharacterE);
        userEnemy.CharAll.Add(oneCharacterE);
        // userEnemy.CharAll.Add(secondCharacterE);
        // userEnemy.CharAll.Add(thirdCharacterE);

        userEnemy.LvlGeneral = 1;
        userEnemy.Name = "The enemies";
        userEnemy.Money = 0;

        // Debug.Log("Aca user controller  : " + user.CharInCombat[0].theSkills.Count);

    }

    public Charac CreateNewCharacterInScene(int idCharacter, int lvl)
    {
        Charac theNewCharacter = GameData.GetInstance().allCharacters.SearchCharacterById(idCharacter);
        theNewCharacter.lvl = lvl;
        theNewCharacter.InitialValuesDerived();
        return theNewCharacter;
    }

    public void ResetValuesStateInBattleMovement()
    {
        _stateInBattle.ResetStateInitial();
    }
}
