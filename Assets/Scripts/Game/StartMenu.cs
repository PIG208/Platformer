using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private void Start(){

        #if UNITY_WEBGL
        quitBtn.SetActive(false);
        #endif
    }
    public void play(){
        SceneManager.LoadScene("Intro");
    }

    public void quit(){
        Application.Quit();
    }

}
