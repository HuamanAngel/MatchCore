using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControllerInSelectMap : MonoBehaviour
{
    // Start is called before the first frame update
    private float rotacionSuave = 0.1f;
    public float velocidadRotacionSuave = 1.0f;
    public Transform cam;
    private static HeroControllerInSelectMap _instance;
    private void Awake()
    {
        _instance = this;
    }
    public static HeroControllerInSelectMap GetInstance()
    {
        return _instance;
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (LogicSelectTale.GetInstance().goInformationMap.activeSelf == false && LogicSelectTale.GetInstance().goConfirmationLvl.activeSelf == false)
        {
            MovementOutBattle();
        }
    }

    public void MovementOutBattle()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            velocidadRotacionSuave = 1.0f;
            rotacionSuave = 1.0f;
            float anguloARotar = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloARotar, ref velocidadRotacionSuave, rotacionSuave);
            transform.rotation = Quaternion.Euler(0f, angulo, 0f);


            Vector3 direccionDelMovimiento;
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(direction.x, 0, direction.z) * Time.deltaTime * 4);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(direction.x, 0, direction.z) * Time.deltaTime * 4);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -5, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, 5, 0);
            }
        }
    }

}
