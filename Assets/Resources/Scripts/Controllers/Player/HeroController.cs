using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
public class HeroController : Character
{
    // Start is called before the first frame update
    public int life = 200;
    // public int damage = 20;

    public int damageMax = 20;
    public int damageMin = 16;
    public int defense = 4;
    // public int armor = 100;
    public int perAbsortion = 10;
    private GameObject Lopt1;
    public GameObject player1Tiers;
    // Private
    private GameObject objLife;
    private GameObject objArmor;
    private GameObject objIcons;
    [Header("Variables Movimiento Del Personaje")]
    private Animator AController;
    private float rotacionSuave = 0.1f;
    public float velocidadRotacionSuave = 1.0f;
    public GameObject objectQuantitySpheresBlue;
    public GameObject objectQuantitySpheresYellow;
    public GameObject objectQuantitySpheresRed;
    private UserController _dataUserGameObject;
    public Texture textureIcon;
    private float speedTheMovement = 5.0f;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        allSKill = new List<Skill>();

        if (inBattle)
        {
            _dataGrid.Add("SelectPlayer", 0);
            _dataGrid.Add("SelectAttack", 0);
            _dataGrid.Add("SelectMovement", 0);
            _dataGrid.Add("IsMoving", 0);
            _dataGrid.Add("IsAttacking", 0);

            _dataGrid.Add("FirstSkillSelected", 0);
            _dataGrid.Add("SecondSkillSelected", 0);
            _dataGrid.Add("ThirdSkillSelected", 0);
            _dataGrid.Add("FourSkillSelected", 0);

            GameObject[] option1 = GameObject.FindGameObjectsWithTag("LOption1");
            foreach (GameObject obj in option1)
            {
                Lopt1 = obj;
                break;
            }


        }
    }
    void Start()
    {
        if (inBattle)
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("LogicGame");
            _logicGame = go[0].GetComponent<LogicGame>();
            _thePlayer = UserPlayerController.GetInstance();

            player1Tiers.SetActive(true);
            objLife = UtilitiesClass.FindChildWithTag(player1Tiers, "Life");
            objArmor = UtilitiesClass.FindChildWithTag(player1Tiers, "Armor");
            objIcons = UtilitiesClass.FindChildByName(player1Tiers, "Character");

            objLife.GetComponent<TMP_Text>().text = "" + life;
            objArmor.GetComponent<TMP_Text>().text = "" + defense;
            objIcons.GetComponent<RawImage>().texture = textureIcon;

            // End Data User 
            StartBattle();
        }
        else
        {
            speedTheMovement = 3.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inBattle)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized; //Normalized es para que al moverse en diagonal no vaya más rápido
                                                                                  // _animator.SetFloat("isRuning", (Mathf.Abs(vertical) + Mathf.Abs(horizontal)));

            if (direccion.magnitude >= 0.1f)
            {
                float anguloARotar = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
                float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloARotar, ref velocidadRotacionSuave, rotacionSuave);
                transform.rotation = Quaternion.Euler(0f, angulo, 0f);
                Vector3 direccionDelMovimiento;
                Vector3 positionCamera = Camera.main.transform.forward;
                if (Input.GetKey(KeyCode.W))
                {
                    direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.forward;
                    transform.Translate(new Vector3(positionCamera.x, 0, positionCamera.z) * Time.deltaTime * speedTheMovement);
                    // Debug.Log(Camera.main.transform.forward);
                }
                // if (Input.GetKey(KeyCode.S))
                // {
                //     direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.back;
                //     transform.Translate(direccionDelMovimiento * Time.deltaTime * 5);
                // }
                // if (Input.GetKey(KeyCode.A))
                // {
                //     direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.right;
                //     transform.Translate(direccionDelMovimiento * Time.deltaTime * 5);
                // }
                // if (Input.GetKey(KeyCode.D))
                // {
                //     direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.left;
                //     transform.Translate(Camera.main.transform.right * Time.deltaTime * 5);
                // }
            }
        }

    }
    public void CurrentClickedGameObject(GameObject gameObject)
    {
        Debug.Log(gameObject.tag);
        if (gameObject.tag == "LOption1")
        {
            Debug.Log("Click in option 1");
        }
    }

    public bool CheckIfSuccessAttack()
    {
        if (_thePlayer.RedSphereG >= firstSkill.red && _thePlayer.YellowSphereG >= firstSkill.yellow && _thePlayer.BlueSphereG >= firstSkill.blue)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
    public override void Attack()
    {
        _thePlayer.RedSphereG = _thePlayer.RedSphereG - firstSkill.red;
        _thePlayer.YellowSphereG = _thePlayer.YellowSphereG - firstSkill.yellow;
        _thePlayer.BlueSphereG = _thePlayer.BlueSphereG - firstSkill.blue;
        SetQuantitySpheres();
    }

    public override int CalculateDmg()
    {
        RandomHelper random = new RandomHelper();
        int randomDmg = random.RandomInt(damageMin, damageMax + 1);
        if (firstSkill.id == 1)
        {

        }
        if (firstSkill.id == 2)
        {
            int toLife = (int)(randomDmg * (firstSkill.value1 / 100));
            Debug.Log("Like life : " + toLife);
            life = life + toLife;
            UpdateAllStats();

        }
        return randomDmg;
    }

    public override void ReceivedDamage(int damageH)
    {
        life = life - ((damageH - defense) * (100 - perAbsortion) / 100);
        UpdateAllStats();
    }
    public override void UpdateAllStats()
    {
        objLife.GetComponent<TMP_Text>().text = "" + life;
        objArmor.GetComponent<TMP_Text>().text = "" + defense;
        objectQuantitySpheresRed.GetComponent<TMP_Text>().text = "x" + _thePlayer.RedSphereG;
        objectQuantitySpheresBlue.GetComponent<TMP_Text>().text = "x" + _thePlayer.BlueSphereG;
        objectQuantitySpheresYellow.GetComponent<TMP_Text>().text = "x" + _thePlayer.YellowSphereG;

    }
    public void Movement()
    {
        Debug.Log("Movement is current");
    }

    public override void StartBattle()
    {
        base.ResetSphere();
        base.RandomSphere(1, 4);
        SetQuantitySpheres();

        // Debug.Log("For enemy :  yellow : "+ YellowSphereG + " ; red : " + RedSphereG + " ; yellow : " + YellowSphereG);
    }

    public void BasicAttack()
    {

    }
    public void SetQuantitySpheres()
    {
        objectQuantitySpheresRed.GetComponent<TMP_Text>().text = "x" + _thePlayer.RedSphereG;
        objectQuantitySpheresBlue.GetComponent<TMP_Text>().text = "x" + _thePlayer.BlueSphereG;
        objectQuantitySpheresYellow.GetComponent<TMP_Text>().text = "x" + _thePlayer.YellowSphereG;
    }
    public void SpecialAttack1()
    {
        Debug.Log("Movement is special effect 1");
    }

    public void SpecialAttack2()
    {
        Debug.Log("Movement is special effect 2");
    }

    public void SpecialAttack3()
    {
        Debug.Log("Movement is special effect 3");
    }

    public void NextTurn()
    {
        Debug.Log("Next turn");
    }

    public void MovementOutBattle()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized; //Normalized es para que al moverse en diagonal no vaya más rápido
        // _animator.SetFloat("isRuning", (Mathf.Abs(vertical) + Mathf.Abs(horizontal)));

        if (direccion.magnitude >= 0.1f)
        {
            float anguloARotar = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloARotar, ref velocidadRotacionSuave, rotacionSuave);
            transform.rotation = Quaternion.Euler(0f, angulo, 0f);
            Vector3 direccionDelMovimiento;
            if (Input.GetKey(KeyCode.W))
            {
                direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.forward;
                transform.Translate(direccionDelMovimiento * Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.S))
            {
                direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.back;
                transform.Translate(direccionDelMovimiento * Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.A))
            {
                direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.right;
                transform.Translate(direccionDelMovimiento * Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.D))
            {
                direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.left;
                transform.Translate(direccionDelMovimiento * Time.deltaTime * 5);
            }
        }
    }

}
