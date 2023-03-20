using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LogicSceneHomeUser : MonoBehaviour
{
    // Start is called before the first frame update
    private UserController _dataUserGameObject;
    [Tooltip("3 images")]
    public RawImage[] spriteHeroesCombat;
    [Tooltip("3 text")]
    public TMP_Text[] lvlHeroesCombat;
    public TMP_Text txtMoney;
    void Start()
    {
        // Scene logic to map HomeUser
        _dataUserGameObject = UserController.GetInstance() ;
        for (int i = 0; i < spriteHeroesCombat.Length; i++)
        {
            if (_dataUserGameObject.user.CharInCombat.Count > i)
            {
                spriteHeroesCombat[i].texture = _dataUserGameObject.user.CharInCombat[i].iconChar;
                lvlHeroesCombat[i].text = "LVL." + _dataUserGameObject.user.CharInCombat[i].lvl;
            }
            else
            {
                spriteHeroesCombat[i].enabled = false;
                lvlHeroesCombat[i].enabled = false;
            }
        }
        txtMoney.text = "$ " + _dataUserGameObject.user.Money + ".00";
    }

    // Update is called once per frame
    void Update()
    {
        // _dataUserGameObject.user.LvlGeneral
    }
}
