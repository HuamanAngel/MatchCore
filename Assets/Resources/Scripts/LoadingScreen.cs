using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen
{
    public static IEnumerator LoadAsyncScene(int numberScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(numberScene);
        Debug.Log("Cargando");
        while (!asyncLoad.isDone)
        {
            Debug.Log("Termino de cargar");
            yield return null;
        }
    }
}
