using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyFinalMusic : MonoBehaviour
{
    public GameObject Music;
    void Awake()
    {
        DontDestroyOnLoad (Music);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0 || scene.buildIndex == 1 || scene.buildIndex == 2 || scene.buildIndex == 3 || scene.buildIndex == 4)
        {
            Destroy(Music);
        }
    }
}

