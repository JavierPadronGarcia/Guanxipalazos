using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCManager : MonoBehaviour
{

    public static SCManager instance;
    private void Awake()
    {

        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAdd(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public bool SceneLoaded(string sceneName)
    {
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);

        if (loadedScene != null && loadedScene.isLoaded)
        {
            return true;
        }
        return false;
    }

}