using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrevMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendPreviousMap()
    {
        // Debug.Log("Number previeus map : " + _numberScene);
        SceneController.ToSceneByNumber(UserController.GetInstance().NumberScene);
    }

}
