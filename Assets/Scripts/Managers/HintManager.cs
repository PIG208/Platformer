using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public Text Hint;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        IEnumerator Wait(){
            
            yield return new WaitForSeconds(2f);
            
            Hint.text = "";
        
        }
        if(other.gameObject.tag=="Player"){
            Hint.text = "Press Q or LB";
            StartCoroutine(Wait());
        }
    }
}
