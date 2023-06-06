using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraMov : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 distanceFromHero;
    public GameObject objectToFollow;
    private HeroInMovement hm;
    private UserController userController;
    public bool cameraFree = false;
    void Start()
    {
        if (!cameraFree)
        {
            hm = HeroInMovement.GetInstance();
            userController = UserController.GetInstance();
            if (userController.StateInBattle.CurrentPosition != Vector3.zero)
            {

                transform.position = new Vector3(userController.StateInBattle.CurrentPosition.x, transform.position.y, userController.StateInBattle.CurrentPosition.z);
                // transform.position = UserController.GetInstance().StateInBattle.CurrentPosition;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraFree)
        {
            if (!hm.IsMovement)
            {
                var translation = GetInputTranslationDirectionA() * Time.deltaTime;
                translation *= 10.0f;

                // Check if i can movement
                bool iCanMov = true;

                RaycastHit objectHit;
                Debug.DrawRay(transform.position, translation, Color.green, 30, false);
                if (Physics.Raycast(transform.position, translation, out objectHit, 10.0f))
                {
                    iCanMov = false;

                    // if (objectHit.collider.transform.gameObject.tag == "Point")
                    // {
                    // }
                }
                if (iCanMov)
                {
                    this.transform.position += translation;
                }
            }
            else
            {
                transform.position = new Vector3(hm.gameObject.transform.position.x, transform.position.y, hm.gameObject.transform.position.z);
            }
        }
        else
        {
            var translation = GetInputTranslationDirectionFree() * Time.deltaTime;
            translation *= 10.0f;
            this.transform.position += translation;
        }

    }



    Vector3 GetInputTranslationDirectionA()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            // if (this.transform.position.z >= 44)
            // {
            // direction = new Vector3(0, 0, 0);
            // }
            // else
            // {
            direction += Vector3.forward;
            // }
        }
        if (Input.GetKey(KeyCode.S))
        {
            // if (this.transform.position.z <= 0)
            // {
            //     direction = new Vector3(0, 0, 0);
            // }
            // else
            // {
            direction += Vector3.back;
            // }

        }
        if (Input.GetKey(KeyCode.A))
        {
            // if (this.transform.position.x <= 0)
            // {
            //     direction = new Vector3(0, 0, 0);
            // }
            // else
            // {
            direction += Vector3.left;
            // }

        }
        if (Input.GetKey(KeyCode.D))
        {
            // if (this.transform.position.x >= 44)
            // {
            //     direction = new Vector3(0, 0, 0);
            // }
            // else
            // {
            direction += Vector3.right;
            // }
        }
        return direction;
    }
    Vector3 GetInputTranslationDirectionFree()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;

        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;

        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        return direction;
    }

    private void OnCollisionEnter(Collision other)
    {

    }
    private void OnCollisionExit(Collision other)
    {

    }
}
