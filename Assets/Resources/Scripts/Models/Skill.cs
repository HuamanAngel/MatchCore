using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    // Deben ser exactamente igual a lo que esta en el json
    public int id;
    public string idSpecial;
    public string name;

    public int yellow;
    public int blue;
    public int red;
    public string skillSpecial;
    public string type;
    public double damage_max;
    public double damage_min;
    public int cooldown;
    public string quality;
    public double value1;
    public double value2;
    public double value3;
    public string attackTypeGridString;
    public int idEffect;
    public string pathIcon;
    public int prob_critical_base;
    public int damage_critical_base;
    public int prob_sucess;
    public string pathEffectPrefab;
    // Derivated values generates from the preview values
    public AttackGrid.TypeOfAttack attackType;
    public Texture iconSkill;
    public Sprite iconSkillSprite;
    public GameObject effectPrefab;
    public string GetInformationStatistics()
    {
        string theText = type + "\n" + damage_max + " - " + damage_min + "\n" + quality;
        return theText;
    }

    public string GetInformationOtras()
    {
        string theText = prob_sucess + "\n" + damage_critical_base + "\n" + prob_critical_base;
        return theText;
    }
    public string GetInformationSpheres()
    {
        string theText = "x" + red + "\n \n" + "x" + yellow + "\n \n" + "x" + blue;
        return theText;
    }

    public bool CheckIfNeedAnimation()
    {
        bool exist = false;
        switch (id)
        {
            case 5:
                exist = true;
                break;
        }
        return exist;
    }
    public IEnumerator AnimationToEffect(GameObject meteorPrefab)
    {
        if (id == 5)
        {
            float speed = 1.0f;
            Vector3 targetPos = meteorPrefab.transform.position; 
            float step = speed * Time.deltaTime;
            Vector3 initPosition = new Vector3(targetPos.x + 8, targetPos.y + 8, targetPos.z);
            meteorPrefab.transform.position = initPosition; 
            while (Vector2.Distance(new Vector2(meteorPrefab.transform.position.x, meteorPrefab.transform.position.y), new Vector2(targetPos.x, targetPos.y)) > 0.001f)
            {
                meteorPrefab.transform.position = Vector3.MoveTowards(meteorPrefab.transform.position, new Vector3(targetPos.x,targetPos.y,targetPos.z), step * 3);
                yield return null;
            }

        }

        yield return new WaitForSeconds(0.1f);
    }

}
