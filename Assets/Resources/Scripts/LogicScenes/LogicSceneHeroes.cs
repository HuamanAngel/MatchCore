using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LogicSceneHeroes : MonoBehaviour
{
    private UserController _dataUserGameObject;
    public RawImage[] spriteHeroesAll;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("DataUser");
        _dataUserGameObject = go[0].GetComponent<UserController>();

        for(int i = 0; i < spriteHeroesAll.Length;i++)
        {
            if (_dataUserGameObject.user.CharAll.Count > i)
            {
                spriteHeroesAll[i].enabled = true;
                spriteHeroesAll[i].texture = _dataUserGameObject.user.CharAll[i].iconChar;
                OnRawImage auxRawImage = spriteHeroesAll[i].gameObject.GetComponent<OnRawImage>();
                auxRawImage.enabled = true;
                auxRawImage.IndexCharacter = i;
            }
            else
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
