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
        Debug.Log("Oket i gonna evaluate the existense of the character");
        Debug.Log("I trying get : " + _thePlayer);
        Debug.Log("I trying get 2: " + _thePlayer.RedSphereG);
        Debug.Log("I trying get 3: " + theSkill.red);

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
        _animator.SetBool("IsAttack", false);
    }

    public void AttackAnim()
    {
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsAttack", true);
    }

    public void ReturnStateOriginalAnim()
    {
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsAttack", false);
    }

    public bool CharacterIsAlive()
    {
        return life > 0;
    }
}
