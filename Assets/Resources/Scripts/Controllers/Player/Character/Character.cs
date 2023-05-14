using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool inBattle = true;
    protected int redSphere = 0;
    protected int blueSphere = 0;
    protected int yellowSphere = 0;
    public int costForMovement = 1;
    public Skill firstSkill;
    public Skill secondtSkill;
    public Skill threeSkill;
    public Skill fourSkill;
    public List<Skill> allSKill;
    protected Skill skillSelectedCurrent;
    protected Skill skillSelectedHover;
    public int YellowSphereG { get => yellowSphere; set => yellowSphere = value; }
    public int RedSphereG { get => redSphere; set => redSphere = value; }
    public int BlueSphereG { get => blueSphere; set => blueSphere = value; }
    public Skill SkillSelectedCurrent { get => skillSelectedCurrent; set => skillSelectedCurrent = value; }
    public Skill SkillSelectedHover { get => skillSelectedHover; set => skillSelectedHover = value; }
    public AttackGrid.TypeOfAttack moveType = AttackGrid.TypeOfAttack.GRID_CROSS;
    public AttackGrid.TypeOfAttack attackType = AttackGrid.TypeOfAttack.GRID_CROSS;
    protected Dictionary<string, int> _dataGrid = new Dictionary<string, int>();
    protected Animator _animator;
    public int life = 200;
    public Skill FirstSkill { get => firstSkill; }
    public Skill SecondtSkill { get => secondtSkill; }
    public Skill ThreeSkill { get => threeSkill; }
    public Skill FourSkill { get => fourSkill; }

    protected LogicGame _logicGame;
    protected PlayerBase _thePlayer;
    public PlayerBase ThePlayer { get => _thePlayer; set => _thePlayer = value; }
    // Coroutines
    protected Coroutine countingCoroutine;
    public void SetDataGrid(string key, int value)
    {
        if (_dataGrid.ContainsKey(key))
        {
            _dataGrid[key] = value;
        }
    }

    public int GetDataGrid(string key)
    {
        return _dataGrid[key];
    }
    public abstract void ReceivedDamage(int damageH);
    public abstract void Attack();
    // public abstract int CalculateDmg();

    public abstract void UpdateAllStats();
    public abstract void StartBattle();
    public virtual void ResetSphere()
    {
        redSphere = 0;
        blueSphere = 0;
        yellowSphere = 0;
    }

    public virtual void RandomSphere(int min, int max)
    {
        RandomHelper random = new RandomHelper();
        redSphere = random.RandomInt(min, max + 1);
        blueSphere = random.RandomInt(min, max + 1);
        yellowSphere = random.RandomInt(min, max + 1);
    }
    public virtual bool CheckIfCanMovementSpheres()
    {
        if (_thePlayer.BlueSphereG >= costForMovement)
        {
            _thePlayer.BlueSphereG = _thePlayer.BlueSphereG - costForMovement;
            return true;
        }
        return false;
    }
    public virtual bool CheckifCanAttackSpheres(Skill theSkill)
    {
        if (_thePlayer.RedSphereG >= theSkill.red && _thePlayer.YellowSphereG >= theSkill.yellow && _thePlayer.BlueSphereG >= theSkill.blue)
        {
            _thePlayer.BlueSphereG = _thePlayer.BlueSphereG - theSkill.blue;
            _thePlayer.RedSphereG = _thePlayer.RedSphereG - theSkill.red;
            _thePlayer.YellowSphereG = _thePlayer.YellowSphereG - theSkill.yellow;
            _thePlayer.SetQuantitySpheres();
            return true;
        }
        return false;
    }

    public bool OnlyCheckIfCanAttack(Skill theSkill)
    {
        if (_thePlayer.RedSphereG >= theSkill.red && _thePlayer.YellowSphereG >= theSkill.yellow && _thePlayer.BlueSphereG >= theSkill.blue)
        {
            return true;
        }
        return false;
    }

    public Skill GetSkillSelected(Character _character)
    {
        if (_character.GetDataGrid("FirstSkillSelected") == 1)
        {
            return allSKill[0];
        }
        else if (_character.GetDataGrid("SecondSkillSelected") == 1)
        {
            return allSKill[1];
        }
        else if (_character.GetDataGrid("ThirdSkillSelected") == 1)
        {
            return allSKill[2];
        }
        else if (_character.GetDataGrid("FourSkillSelected") == 1)
        {
            return allSKill[3];
        }
        else
        {
            return null;
        }

    }

    public void WalkingAnim()
    {
        _animator.SetBool("IsWalking", true);
        _animator.SetBool("Attack1", false);
        _animator.SetBool("Attack2", false);
        _animator.SetBool("Attack3", false);
    }

    public void AttackAnim(int numberSkill)
    {
        _animator.SetBool("IsWalking", false);
        switch (numberSkill)
        {
            case 1:
                _animator.SetBool("Attack1", true);
                break;
            case 2:
                _animator.SetBool("Attack2", true);
                break;
            case 3:
                _animator.SetBool("Attack3", true);
                break;
            default:
                _animator.SetBool("Attack1", true);
                break;
        }
    }

    public void ReturnStateOriginalAnim()
    {
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("Attack1", false);
        _animator.SetBool("Attack2", false);
        _animator.SetBool("Attack3", false);
        _animator.SetBool("IsDie", false);
    }

    public bool CharacterIsAlive()
    {
        return life > 0;
    }

    public void EvaluateIfCanFight()
    {
        bool isAlive = CharacterIsAlive();
        if (!isAlive)
        {
            _thePlayer.ChangeStateAliveCharacter(this.gameObject, false);
            StartCoroutine(AnimationDeadInBattle());
        }
    }

    public IEnumerator AnimationDeadInBattle()
    {
        Animator myAnim = _animator;
        myAnim.SetBool("IsDie", true);
        // Waiting for start animation
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            yield return null;
        }
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }
        myAnim.SetBool("IsDie", false);
        _logicGame.CheckIfEndBattle();
    }
}
