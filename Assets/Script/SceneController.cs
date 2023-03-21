using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public void ToHomeUser()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
    public void ToMapSelectionLvl()
    {
        SceneManager.LoadScene(2,LoadSceneMode.Single);
    }
    public void ToHeroes()
    {
        SceneManager.LoadScene(3,LoadSceneMode.Single);
    }
    public void ToTale1()
    {
        SceneManager.LoadScene(4,LoadSceneMode.Single);
    }

    // public void ToBattle()
    // {
    //     SceneManager.LoadScene(5,LoadSceneMode.Single);
    // }
    public static void ToBattle()
    {
        SceneManager.LoadScene(5,LoadSceneMode.Single);
    }    
    public void QuitApllication()
    {
        Application.Quit();
    }
}    

