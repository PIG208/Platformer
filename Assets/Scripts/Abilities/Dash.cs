using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    Rigidbody2D _rigidbody;
    MovementManager  _movementmanager;
    private bool isDashing = false;
    public int dashSpeed = 30;
    public float dashCooldown = 1f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _movementmanager = GetComponent<MovementManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton5) && !isDashing){
            isDashing = true;
            StartCoroutine(dash());
        }
    }

    IEnumerator dash(){
        _rigidbody.AddForce(new Vector2(5 * _movementmanager.Direction, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }

}
