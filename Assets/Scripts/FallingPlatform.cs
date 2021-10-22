using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")&& !isFalling){
            isFalling = true;
            StartCoroutine(Fall());
            StartCoroutine(Destroy());
        }
    }


    IEnumerator Fall(){
        yield return new WaitForSeconds(.5f);
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
    IEnumerator Destroy(){
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}



