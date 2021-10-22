using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    //public GameObject quitBtn;

    private void Start(){

        #if UNITY_WEBGL
        //quitBtn.SetActive(false);
        #endif
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)){
            if(PublicVars.paused){
                Resume();
            }
            else{
                PublicVars.paused = true;
                pauseMenu.SetActive(true);
                Time.timeScale =0;

            }
        }
    }

    public void Resume(){
        PublicVars.paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit(){

        SceneManager.LoadScene("Start");

    }


}
