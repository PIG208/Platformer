using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyFinalMusic : MonoBehaviour
{
    public GameObject Music;
    void Awake()
    {
        DontDestroyOnLoad(Music);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 5 && scene.buildIndex != 6)
        {
            Destroy(Music);
        }
    }
}

