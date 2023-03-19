using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hourglass_script : MonoBehaviour
{
    // Start is called before the first frame update
    private LogicGame _logicGame;
    void Start()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("LogicGame");
        _logicGame = go[0].GetComponent<LogicGame>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!GameManager.instance.GameOver)
        {
            _logicGame.NextTurn();
        }
    }
}
