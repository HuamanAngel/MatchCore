using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Fire : MonoBehaviour
{
    private int damageMin;
    private int damageMax;
    private int turnToDestroy;
    private LogicGame _logicGame;
    private int _turnCreate;
    public int DamageMin { get => damageMin; set => damageMin = value; }
    public int DamageMax { get => damageMax; set => damageMax = value; }
    public int TurnToDestroy { get => turnToDestroy; set => turnToDestroy = value; }
    public bool destroyInmediate = false;
    public bool noDestroy = false;
    public float timeToDestroy = 2.0f;
    private void Awake()
    {
        _logicGame = LogicGame.GetInstance();
        _turnCreate = _logicGame.Turn;
    }
    void Start()
    {
        if (destroyInmediate == true && noDestroy == false)
        {
            Invoke("DestroyThisObject", timeToDestroy);
        }        
    }

    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyInmediate == false)
        {
            if ((_turnCreate + turnToDestroy) < _logicGame.Turn)
            {
                Destroy(this.gameObject);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HeroController>() != null)
        {
            HeroController h1 = other.gameObject.GetComponent<HeroController>();
            RandomHelper random = new RandomHelper();
            int randomDmg = random.RandomInt(damageMin, damageMax + 1);
            h1.ReceivedDamage(randomDmg);
            h1.UpdateAllStats();
            Vector3 positioToFloating = other.gameObject.transform.position + new Vector3(0, 0, -2);
            StartCoroutine(EffectText.FloatingTextFadeOut(_logicGame.objectTextFloating,positioToFloating, "" + randomDmg, new Color32(222, 41, 22, 255), new Color32(222, 41, 22, 0)));
        }
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            EnemyController h1 = other.gameObject.GetComponent<EnemyController>();
            RandomHelper random = new RandomHelper();
            int randomDmg = random.RandomInt(damageMin, damageMax + 1);
            h1.ReceivedDamage(randomDmg);
            h1.UpdateAllStats();
            Vector3 positioToFloating = other.gameObject.transform.position + new Vector3(0, 0, -2);
            StartCoroutine(EffectText.FloatingTextFadeOut(_logicGame.objectTextFloating,positioToFloating, "" + randomDmg, new Color32(222, 41, 22, 255), new Color32(222, 41, 22, 0)));

        }
    }
}