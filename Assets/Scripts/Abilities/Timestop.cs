using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timestop : MonoBehaviour
{
    bool frozen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !frozen)
        {
            frozen = true;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<MovementManager>().enabled = false;
                enemy.GetComponent<AIManager>().enabled = false;
                enemy.GetComponent<Animator>().enabled = false;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.Q) && frozen)
        {
            frozen = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<MovementManager>().enabled = true;
                enemy.GetComponent<AIManager>().enabled = true;
                enemy.GetComponent<Animator>().enabled =  true;
            }
        }
    }
}
