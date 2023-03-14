using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameObject goHe = Instantiate(_dataUserGameObject.user.CharInCombat[i].prefabCharInBattle);
            goHe.tag = "Player";
            goHe.AddComponent<HeroController>() ;
            goHe.transform.position = new Vector3(5.5f - i, 0.53f, -31.682f);
            goHe.GetComponent<HeroController>().player1Tiers = player1Tiers[i];
            goHe.GetComponent<HeroController>().inBattle = true;
            goHe.GetComponent<HeroController>().objectQuantitySpheresRed = objectQuantitySpheresRed;
            goHe.GetComponent<HeroController>().objectQuantitySpheresBlue = objectQuantitySpheresBlue;
            goHe.GetComponent<HeroController>().objectQuantitySpheresYellow = objectQuantitySpheresYellow;
            goHe.GetComponent<HeroController>().life = _dataUserGameObject.user.CharInCombat[i].lifeTotal;
            goHe.GetComponent<HeroController>().defense = _dataUserGameObject.user.CharInCombat[i].armorTotal;
            goHe.GetComponent<HeroController>().textureIcon = _dataUserGameObject.user.CharInCombat[i].iconChar;
            goHe.GetComponent<HeroController>().costForMovement = _dataUserGameObject.user.CharInCombat[i].blue;
            goHe.GetComponent<HeroController>().firstSkill = _dataUserGameObject.user.CharInCombat[i].theSkills[0];
            // goHe.GetComponent<HeroController>().moveType = _dataUserGameObject.user.CharInCombat[i].movementType;

            for (int j = 0; j < _dataUserGameObject.user.CharInCombat[i].theSkills.Count; j++)
            {
                // Debug.Log("aca el iterador : " + j);
                // Debug.Log("Aca o9s poderes : " + _dataUserGameObject.user.CharInCombat[i].theSkills[j]);
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
