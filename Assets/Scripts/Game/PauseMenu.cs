using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
public GameObject pauseMenu;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(PublicVars.paused){
                PublicVars.paused = false;
                pauseMenu.SetActive(false);
            }
            else{
                PublicVars.paused = true;
                pauseMenu.SetActive(true);
            }
        }
    }
}
