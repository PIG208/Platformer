using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    Rigidbody2D _rigidbody;
    private bool isDashing = false;
    public int dashSpeed = 30;
    public float dashCooldown = 1f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing){
            isDashing = true;
            StartCoroutine(dash());
        }
    }

    IEnumerator dash(){
        _rigidbody.AddForce(transform.right * dashSpeed * Time.deltaTime, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }

}
