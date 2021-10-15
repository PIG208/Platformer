using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int levelToLoad = 1;
    public char difficulty = 'E';
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(difficulty + levelToLoad);
        }
    }
}
