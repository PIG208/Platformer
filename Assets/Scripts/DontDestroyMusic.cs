using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{
    public GameObject Music;
    void Awake()
    {
        DontDestroyOnLoad (Music);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 4)
        {
            Destroy(Music);
        }
    }
}
