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
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ToHomeUser()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public static void ToMapSelectionLvl()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void ToHeroes()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
    public static void ToTale1()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    // public void ToBattle()
    // {
    //     SceneManager.LoadScene(5,LoadSceneMode.Single);
    // }
    public static void ToBattle()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }
    public void QuitApllication()
    {
        Application.Quit();
    }

    public static List<int> ToMaps(int stage)
    {
        List<int> intMapas = new List<int>();
        switch (stage)
        {
            case 1:
                intMapas.Add(6);
                intMapas.Add(7);
                break;
            case 2:
                intMapas.Add(6);
                intMapas.Add(7);
                break;
            case 3:
                intMapas.Add(6);
                intMapas.Add(7);
                break;
            case 4:
                intMapas.Add(6);
                intMapas.Add(7);
                break;
            default:
                break;
        }
        return intMapas;
    }
    public static void ToSceneByNumber(int numberScene)
    {
        SceneManager.LoadScene(numberScene, LoadSceneMode.Single);
    }
}

