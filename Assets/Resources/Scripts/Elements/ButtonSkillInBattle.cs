using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonSkillInBattle : MonoBehaviour
{
    private bool _isPressed = false;
    private Character _character;
    private GridControlRe _gridController;
    private int _numberSkill;
    private GameObject _characterBelong;
    private Skill _theSkillSelected;
    public Skill TheSkillSelected { get => _theSkillSelected; set => _theSkillSelected = value; }

    private Skill theSkill;

    public Skill TheSkill { get => theSkill; set => theSkill = value; }

    private Dictionary<string, GameObject> _elementsInformation;
    public Dictionary<string, GameObject> ElementsInformation { get => _elementsInformation; set => _elementsInformation = value; }

    public Character CharacterGetSet { get => _character; set => _character = value; }
    public int NumberSkill { get => _numberSkill; set => _numberSkill = value; }
    public GameObject CharacterBelong { get => _characterBelong; set => _characterBelong = value; }
    private int _prevRed;
    private int _prevBlue;
    private int _prevYellow;
    private GameObject objIconSkill;
    private int turnActivateSkill;
    private int turnTemp;
    private Color c1 = Color.yellow;
    private Color c2 = Color.red;
    private Color32 colorDisabled = new Color32(0x6F, 0x6F, 0x6F, 0xFF);
    private Color32 colorEnabled = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    private Color32 colorInsufficient = new Color32(0xFF, 0x23, 0x23, 0xFF);

    void Start()
    {
        _gridController = GridControlRe.GetInstance();
        if(_numberSkill != -1)
        {
            objIconSkill = UtilitiesClass.FindChildByName(this.gameObject, "ImageSkill");
        }
        turnActivateSkill = -1;
        turnTemp = LogicGame.GetInstance().Turn;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseEnter()
    {
        if (_numberSkill != -1)
        {

            _character.SetDataGrid("FirstSkillSelected", 0);
            _character.SetDataGrid("SecondSkillSelected", 0);
            _character.SetDataGrid("ThirdSkillSelected", 0);
            _character.SetDataGrid("FourSkillSelected", 0);
            switch (_numberSkill)
            {
                case -1:
                    break;
                case 0:
                    _character.SetDataGrid("FirstSkillSelected", 1);
                    _gridController.DrawPathToHover();
                    break;
                case 1:
                    _character.SetDataGrid("SecondSkillSelected", 1);
                    _gridController.DrawPathToHover();
                    break;
                case 2:
                    _character.SetDataGrid("ThirdSkillSelected", 1);
                    _gridController.DrawPathToHover();
                    break;
                case 3:
                    _character.SetDataGrid("FourSkillSelected", 1);
                    _gridController.DrawPathToHover();
                    break;
            }
        }

    }
    private void OnMouseExit()
    {
        if (_numberSkill != -1)
        {
            _character.SetDataGrid("FirstSkillSelected", 0);
            _character.SetDataGrid("SecondSkillSelected", 0);
            _character.SetDataGrid("ThirdSkillSelected", 0);
            _character.SetDataGrid("FourSkillSelected", 0);
            _gridController.ClearAllObjectInTilemapInteractive();

        }
    }

    private void OnMouseUp()
    {
        if (_numberSkill != -1)
        {
            _character.SetDataGrid("FirstSkillSelected", 0);
            _character.SetDataGrid("SecondSkillSelected", 0);
            _character.SetDataGrid("ThirdSkillSelected", 0);
            _character.SetDataGrid("FourSkillSelected", 0);

            switch (_numberSkill)
            {
                case -1:
                    break;
                case 0:
                    _character.SetDataGrid("FirstSkillSelected", 1);
                    _gridController.DrawPathAttackWhenSelectedSkill();
                    break;
                case 1:
                    _character.SetDataGrid("SecondSkillSelected", 1);
                    _gridController.DrawPathAttackWhenSelectedSkill();
                    break;
                case 2:
                    _character.SetDataGrid("ThirdSkillSelected", 1);
                    _gridController.DrawPathAttackWhenSelectedSkill();
                    break;
                case 3:
                    _character.SetDataGrid("FourSkillSelected", 1);
                    _gridController.DrawPathAttackWhenSelectedSkill();
                    break;
            }
            _gridController.EnabledAttackPath = true;
            _gridController.PressSelected = true;
        }
    }
    public void DeselectedSkill(int mode = 0)
    {
        if (mode == 0)
        {
            LogicGame.GetInstance().IsCurrentSelectedSkill = false;
            _isPressed = false;

        }
        else if (mode == 1)
        {

            LogicGame.GetInstance().IsCurrentSelectedSkill = false;
            _isPressed = false;
            turnActivateSkill = LogicGame.GetInstance().Turn;
            if(_numberSkill  != -1)
            {

                objIconSkill.GetComponent<RawImage>().color = colorDisabled;
            }
        }
    }

}
