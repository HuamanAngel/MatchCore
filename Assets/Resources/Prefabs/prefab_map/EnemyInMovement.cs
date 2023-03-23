using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInMovement : MonoBehaviour
{
    private List<Charac> _theCharacters;
    private bool _isALive;
    private bool _isEnemyFinalOfMap;
    public List<Charac> TheCharacters { get => _theCharacters; set => _theCharacters = value; }
    public bool IsALive { get => _isALive; set => _isALive = value; }
    public bool IsEnemyFinalOfMap { get => _isEnemyFinalOfMap; set => _isEnemyFinalOfMap = value; }
    void Start()
    {
        _theCharacters = new List<Charac>();
        _isALive = true;
        _theCharacters = GameData.GetInstance().GetRandomEnemyByMap(numberMap:1,type:"Golem",quality:"Muy Comun",1);
        _isEnemyFinalOfMap = false;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void DieThisEnemy()
    {
        StartCoroutine(ProcessDie());
    }

    IEnumerator ProcessDie()
    {
        Animator myAnim = transform.parent.gameObject.GetComponent<Animator>();
        myAnim.SetBool("IsDie",true);

        // Waiting for start animation
        while(!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            yield return null;
        }        
        while(myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }        

        myAnim.SetBool("IsDie",false);
        UserController.GetInstance().StateInBattle.IsDeadCharacter = false;
        Destroy(transform.parent.gameObject);
    }
}
