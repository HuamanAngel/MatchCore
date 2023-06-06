using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovertScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberPositionMapBelong = 0;
    void Start()
    {
        if (UserController.GetInstance().NumberPositionMap == numberPositionMapBelong)
        {
            this.gameObject.SetActive(false);
        }
        GetComponent<Collider>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
