using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    // Start is called before the first frame update
    private int lvlGeneral;
    private int money;
    private string name;
    private List<Charac> charAll;
    private List<Charac> charInCombat;
    private List<List<BuffStruct>> buffCurrent;
    // Seeter and Getter
    public int LvlGeneral { get => lvlGeneral; set => lvlGeneral = value; }
    public int Money { get => money; set => money = value; }
    public string Name { get => name; set => name = value; }
    public List<Charac> CharAll { get => charAll; set => charAll = value; }
    public List<Charac> CharInCombat { get => charInCombat; set => charInCombat = value; }
    public List<List<BuffStruct>> BuffCurrent { get => buffCurrent; set => buffCurrent = value; }
    public User()
    {
        charAll = new List<Charac>();
        charInCombat = new List<Charac>();
        buffCurrent = new List<List<BuffStruct>>();
        // 5 characters
        for (int i = 0; i < 5; i++)
        {
            List<BuffStruct> listBuffStruct = new List<BuffStruct>();
            // 20 buff max for character
            for (int j = 0; i < 20; i++)
            {
                listBuffStruct.Add(new BuffStruct());
            }
            buffCurrent.Add(listBuffStruct);
        }
    }
}
