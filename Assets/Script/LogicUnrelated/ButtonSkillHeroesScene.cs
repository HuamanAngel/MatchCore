using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonSkillHeroesScene : MonoBehaviour, IPointerClickHandler
{
    private int indexCharacter = -1;
    private int indexSkill = -1;
    private UserController _dataUserGameObject;
    private Charac _characterSelect;
    public TMP_Text special;
    public TMP_Text statistics;
    public TMP_Text requirements;
    public TMP_Text other;
    public string typeAttack;
    public int IndexCharacter { get => indexCharacter; set => indexCharacter = value; }
    public int IndexSkill { get => indexSkill; set => indexSkill = value; }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(indexCharacter != -1)
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("DataUser");
            _dataUserGameObject = go[0].GetComponent<UserController>();
            special.text = _dataUserGameObject.user.CharAll[indexCharacter].theSkills[indexSkill].skillSpecial; 
            statistics.text = _dataUserGameObject.user.CharAll[indexCharacter].theSkills[indexSkill].GetInformationStatistics(); 
            other.text = _dataUserGameObject.user.CharAll[indexCharacter].theSkills[indexSkill].GetInformationOtras(); 
            requirements.text = _dataUserGameObject.user.CharAll[indexCharacter].theSkills[indexSkill].GetInformationSpheres();
            // previewGo.transform.parent = _dataUserGameObject.user.CharAll[indexCharacter].prefabChar.transform;
        }
        // _dataUserGameObject.user.CharAll[indexCharacter]
    }

}
