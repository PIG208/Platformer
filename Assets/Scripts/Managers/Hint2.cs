using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint2 : MonoBehaviour
{
    public Text Hint;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        IEnumerator Wait(){
            
            yield return new WaitForSeconds(2f);
            
            Hint.text = "";
        
        }
        if(other.gameObject.tag=="Player"){
            Hint.text = "Use Scroll Wheel";
            StartCoroutine(Wait());
        }
    }
}
