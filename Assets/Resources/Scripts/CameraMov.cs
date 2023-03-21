using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraMov : MonoBehaviour
{
    // Start is called before the first frame update

    public int modeCamera = 1;
    private Vector3 distanceFromHero;
    public GameObject objectToFollow;

    void Start()
    {
        if(modeCamera == 2)
        {
            distanceFromHero = transform.position - objectToFollow.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(modeCamera == 1)
        {
            var translation = GetInputTranslationDirectionA() * Time.deltaTime;

            translation *= 10.0f;
            this.transform.position += translation;
        }else if(modeCamera == 2)
        {
            transform.position = objectToFollow.transform.position + distanceFromHero;        
        }
        
    }



    Vector3 GetInputTranslationDirectionA()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            if (this.transform.position.z >= 44)
            {
                direction = new Vector3(0, 0, 0);
            }
            else
            {
                direction += Vector3.forward;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (this.transform.position.z <= 0)
            {
                direction = new Vector3(0, 0, 0);
            }
            else
            {
                direction += Vector3.back;
            }

        }
        if (Input.GetKey(KeyCode.A))
        {
            if (this.transform.position.x <= 0)
            {
                direction = new Vector3(0, 0, 0);
            }
            else
            {
                direction += Vector3.left;
            }

        }
        if (Input.GetKey(KeyCode.D))
        {
            if (this.transform.position.x >= 44)
            {
                direction = new Vector3(0, 0, 0);
            }
            else
            {
                direction += Vector3.right;
            }
        }
        // if (Input.GetKey(KeyCode.Q))
        // {
        //     direction += Vector3.down;
        // }
        // if (Input.GetKey(KeyCode.E))
        // {
        //     direction += Vector3.up;
        // }
        return direction;
    }
}