using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class OnRawImage : MonoBehaviour, IPointerClickHandler
{
    private UserController _dataUserGameObject;
    private Charac _characterSelect;
    // private int idCharacter;
    // public int IdCharacter { get => idCharacter; set => idCharacter = value; }
    private int indexCharacter;
    public TMP_Text vida;
    public TMP_Text defense;
    public TMP_Text nameAndLvl;
    [Tooltip("Set in order desc, max. set 4 skill")]
    public GameObject[] skills;
    public Sprite skillDisabled;
    public Sprite skillEnabled;
    public RawImage[] allSkill;
    public Image[] allSkillButtons;

    private GameObject _cloneChar;
    public GameObject previewHero;

    public int IndexCharacter { get => indexCharacter; set => indexCharacter = value; }
    // public int CharacterSelect { get => _characterSelect; set => _characterSelect = value; }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("DataUser");
        _dataUserGameObject = go[0].GetComponent<UserController>();
        vida.text = "" + _dataUserGameObject.user.CharAll[indexCharacter].lifeBase;
        defense.text = "" + _dataUserGameObject.user.CharAll[indexCharacter].armorBase;
        nameAndLvl.text = "" + _dataUserGameObject.user.CharAll[indexCharacter].name + "\n" + "LvL. " + _dataUserGameObject.user.CharAll[indexCharacter].lvl;

        // remove all information from the last choose
        
        foreach (Transform theTransform in previewHero.transform)
        {
            Destroy(theTransform.gameObject);
        }

        for (int i = 0; i < allSkill.Length; i++)
        {
            if (_dataUserGameObject.user.CharAll[indexCharacter].theSkills.Count > i)
            {
                allSkill[i].texture = _dataUserGameObject.user.CharAll[indexCharacter].theSkills[i].iconSkill;
                allSkill[i].enabled = true;
                allSkillButtons[i].sprite = skillEnabled; 
                allSkillButtons[i].gameObject.GetComponent<ButtonSkillHeroesScene>().IndexCharacter = indexCharacter;
                allSkillButtons[i].gameObject.GetComponent<ButtonSkillHeroesScene>().IndexSkill = i;

                _cloneChar = Instantiate(_dataUserGameObject.user.CharAll[indexCharacter].prefabChar);
                _cloneChar.transform.SetParent(previewHero.transform);

            }else{
                allSkill[i].enabled = false;
                allSkillButtons[i].sprite = skillDisabled; 
            }
        }
        // Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

}
