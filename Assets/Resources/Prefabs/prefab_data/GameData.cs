using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public ModelCharacts allCharacters;
    public static GameData _instance;
    public static GameData GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        // Only 1 Game Manager can exist at a time
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        allCharacters = JsonReaderA.GetAllCharacter();
    }
    public List<Charac> GetRandomEnemyByMap(int numberMap, string type, string quality, int quantityEnemies = 1)
    {
        List<Charac> enemyInMap = new List<Charac>();
        List<Charac> enemyCurrentSelected = new List<Charac>();
        enemyInMap = allCharacters.SearchCharacterByTypeAndQuality(type: type, quality: quality);
        switch (numberMap)
        {
            case 1:
                for (int i = 0; i < quantityEnemies; i++)
                {
                    int valueRandom = Random.Range(0, enemyInMap.Count);
                    // enemyInMap[Random.Range(0, enemyInMap.Count)].lvl = 1;
                    // enemyInMap[Random.Range(0, enemyInMap.Count)].InitialValuesDerived();
                    enemyInMap[valueRandom].lvl = 1;
                    enemyInMap[valueRandom].InitialValuesDerived();
                    enemyCurrentSelected.Add(enemyInMap[valueRandom]);
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
        return enemyCurrentSelected;
    }
}
