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
        _dataUserGameObject = UserController.GetInstance();
        RandomSphere(4, 8);
        SetQuantitySpheres();
        User theUser = _dataUserGameObject.user;

        for (int i = 0; i < theUser.CharInCombat.Count; i++)
        {
            // Creation prefab in map
            GameObject goHe = Instantiate(theUser.CharInCombat[i].prefabCharInBattle);
            goHe.tag = "Player";
            goHe.AddComponent<HeroController>();

            goHe.transform.SetParent(GridControlRe.GetInstance().gameObject.transform);
            goHe.transform.localPosition =  new Vector3(0.0f,0.0f,-2.0f) +  new Vector3(0.5f - i, 0.0f, 0.5f);
            
            // Creation Icon in Canvas
            // GameObject goHe = player1Tiers[i];

            goHe.GetComponent<HeroController>().player1Tiers = player1Tiers[i];
            player1Tiers[i].SetActive(true);
            goHe.GetComponent<HeroController>().inBattle = true;
            goHe.GetComponent<HeroController>().objectQuantitySpheresRed = objectQuantitySpheresRed;
            goHe.GetComponent<HeroController>().objectQuantitySpheresBlue = objectQuantitySpheresBlue;
            goHe.GetComponent<HeroController>().objectQuantitySpheresYellow = objectQuantitySpheresYellow;
            goHe.GetComponent<HeroController>().life = theUser.CharInCombat[i].lifeTotal;
            goHe.GetComponent<HeroController>().defense = theUser.CharInCombat[i].armorTotal;
            goHe.GetComponent<HeroController>().textureIcon = theUser.CharInCombat[i].iconChar;
            goHe.GetComponent<HeroController>().costForMovement = theUser.CharInCombat[i].blue;
            // goHe.GetComponent<HeroController>().firstSkill = _dataUserGameObject.user.CharInCombat[i].theSkills[0];
            goHe.GetComponent<HeroController>().moveType = theUser.CharInCombat[i].movementType;
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
            // Debug.Log("Cantidad skiles  : " + _dataUserGameObject.user.CharInCombat[i].theSkills.Count);
            // Debug.Log("skillsCanvas.Count : " + quantity);
            for (int j = 0; j < quantity; j++)
            {
                Dictionary<string, GameObject> dInformation = new Dictionary<string, GameObject>(); 
                GameObject objSkill = UtilitiesClass.FindChildByName(skillsCanvas[j], "ImageSkill");
                GameObject objInformation = UtilitiesClass.FindChildByName(skillsCanvas[j], "Information");
                GameObject objYellow = UtilitiesClass.FindChildByName(objInformation, "yellow");
                GameObject objRed = UtilitiesClass.FindChildByName(objInformation, "red");
                GameObject objBlue = UtilitiesClass.FindChildByName(objInformation, "blue");

                dInformation["yellow"] = objYellow;
                dInformation["red"] = objRed;
                dInformation["blue"] = objBlue;
                // Change
                skillsCanvas[j].GetComponent<ButtonSkill>().CharacterBelong = goHe; 
                skillsCanvas[j].GetComponent<ButtonSkill>().TheSkill = theUser.CharInCombat[i].theSkills[j]; 
                skillsCanvas[j].GetComponent<ButtonSkill>().ElementsInformation = dInformation; 

                objSkill.GetComponent<RawImage>().texture = theUser.CharInCombat[i].theSkills[j].iconSkill;
                objYellow.GetComponent<TMP_Text>().text = "x" + theUser.CharInCombat[i].theSkills[j].yellow;
                objRed.GetComponent<TMP_Text>().text = "x" + theUser.CharInCombat[i].theSkills[j].red;
                objBlue.GetComponent<TMP_Text>().text = "x" + theUser.CharInCombat[i].theSkills[j].blue;

                goHe.GetComponent<HeroController>().allSKill.Add(theUser.CharInCombat[i].theSkills[j]);
                skillsCanvas[j].GetComponent<ButtonSkill>().enabled = true;
                skillsCanvas[j].GetComponent<Button>().enabled = true;

            }
            _allHeroInPlay.Add(goHe);
            _allCharactersAlive[goHe] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
