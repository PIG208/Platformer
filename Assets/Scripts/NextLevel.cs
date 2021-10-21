using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string levelToLoad = "1";
    public string difficulty = "E";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelManager.CurrentLevelManager.LoadLevel(difficulty + levelToLoad);
        }
    }
}
