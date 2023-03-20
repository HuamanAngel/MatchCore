using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelInfoNivel;
    public GameObject canvas;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNivelInfo()
    {
        panelInfoNivel = Instantiate(panelInfoNivel);
        panelInfoNivel.transform.SetParent(canvas.transform);
        // panelInfoNivel.transform.position = canvas.transform.position;
    }

    public void DestroyNivelInfo()
    {
        Destroy(panelInfoNivel);
    }
}
