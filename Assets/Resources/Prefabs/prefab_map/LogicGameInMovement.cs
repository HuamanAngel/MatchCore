using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGameInMovement : MonoBehaviour
{
    public static LogicGameInMovement _instance;

    // Start is called before the first frame update
    public static LogicGameInMovement GetInstance()
    {
        return _instance;
    }

    private void Awake() {
        _instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
