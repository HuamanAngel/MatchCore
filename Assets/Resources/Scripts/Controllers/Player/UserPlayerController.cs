using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UserPlayerController : PlayerBase
{
    // Start is called before the first frame update
    private static UserPlayerController _instance;
    private List<GameObject> _allHeroInPlay;
    public static UserPlayerController GetInstance()
    {
        return _instance;
    }
    private void Awake()
    {
        _instance = this;
        _allHeroInPlay = new List<GameObject>();
    }

    void Start()
    {
        GameObject[] goCh = GameObject.FindGameObjectsWithTag("DataUser");
        _dataUserGameObject = goCh[0].GetComponent<UserController>();
        RandomSphere(4, 8);

        for (int i = 0; i < _dataUserGameObject.user.CharInCombat.Count; i++)
        {
            GameObject goHe = player1Tiers[i];
            // goHe.tag = "Player";
            goHe.AddComponent<HeroController>();
            goHe.GetComponent<HeroController>().player1Tiers = player1Tiers[i];
            player1Tiers[i].SetActive(true);
            goHe.GetComponent<HeroController>().inBattle = true;
            goHe.GetComponent<HeroController>().objectQuantitySpheresRed = objectQuantitySpheresRed;
            goHe.GetComponent<HeroController>().objectQuantitySpheresBlue = objectQuantitySpheresBlue;
            goHe.GetComponent<HeroController>().objectQuantitySpheresYellow = objectQuantitySpheresYellow;
            goHe.GetComponent<HeroController>().life = _dataUserGameObject.user.CharInCombat[i].lifeTotal;
            goHe.GetComponent<HeroController>().defense = _dataUserGameObject.user.CharInCombat[i].armorTotal;
            goHe.GetComponent<HeroController>().textureIcon = _dataUserGameObject.user.CharInCombat[i].iconChar;
            goHe.GetComponent<HeroController>().costForMovement = _dataUserGameObject.user.CharInCombat[i].blue;
            // goHe.GetComponent<HeroController>().firstSkill = _dataUserGameObject.user.CharInCombat[i].theSkills[0];
            // goHe.GetComponent<HeroController>().moveType = _dataUserGameObject.user.CharInCombat[i].movementType;
            goHe.GetComponent<HeroController>().allSKill = new List<Skill>();
            GameObject toIcon = UtilitiesClass.FindChildByName(player1Tiers[i], "PanelSkills");
            List<GameObject> skillsCanvas = UtilitiesClass.FindAllChildWithTag(toIcon, "SkillCanvas");

            // get less value

            int quantity = 0;
            if(_dataUserGameObject.user.CharInCombat[i].theSkills.Count > skillsCanvas.Count)
            {
                quantity = skillsCanvas.Count;
            }else{
                quantity = _dataUserGameObject.user.CharInCombat[i].theSkills.Count;
            }
            // for (int j = 0; j < _dataUserGameObject.user.CharInCombat[i].theSkills.Count; j++)
            for (int j = 0; j < quantity; j++)
            {
                GameObject objSkill = UtilitiesClass.FindChildByName(skillsCanvas[j], "ImageSkill");
                GameObject objInformation = UtilitiesClass.FindChildByName(skillsCanvas[j], "Information");
                GameObject objYellow = UtilitiesClass.FindChildByName(objInformation, "yellow");
                GameObject objRed = UtilitiesClass.FindChildByName(objInformation, "red");
                GameObject objBlue = UtilitiesClass.FindChildByName(objInformation, "blue");

                objSkill.GetComponent<RawImage>().texture = _dataUserGameObject.user.CharInCombat[i].theSkills[j].iconSkill;
                objYellow.GetComponent<TMP_Text>().text = "x" + _dataUserGameObject.user.CharInCombat[i].theSkills[j].yellow;
                objRed.GetComponent<TMP_Text>().text = "x" + _dataUserGameObject.user.CharInCombat[i].theSkills[j].red;
                objBlue.GetComponent<TMP_Text>().text = "x" + _dataUserGameObject.user.CharInCombat[i].theSkills[j].blue;

                goHe.GetComponent<HeroController>().allSKill.Add(_dataUserGameObject.user.CharInCombat[i].theSkills[j]);
            }
            _allHeroInPlay.Add(goHe);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
