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
    // Seeter and Getter
    public int LvlGeneral{get => lvlGeneral; set => lvlGeneral = value;}
    public int Money{get => money; set => money = value;}
    public string Name{get => name; set => name = value;}
    public List<Charac> CharAll{get => charAll; set => charAll = value;}
    public List<Charac> CharInCombat{get => charInCombat; set => charInCombat = value;}

    public User()
    {
        charAll = new List<Charac>();
        charInCombat = new List<Charac>();
    }
}
