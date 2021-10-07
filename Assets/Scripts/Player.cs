using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementManager))]
public class Player : MonoBehaviour, InputControls.IPlayerActions, IMovable
{
    public GameObject BulletPrefab;
    public float BulletSpeed = 10f;
    public MovementManager Movement { get => _movement; }

    private Rigidbody2D _rigidbody;
    private MovementManager _movement;
    private int _direction = 1;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _movement = GetComponent<MovementManager>();
    }

    public void OnFire(InputAction.CallbackContext cb)
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(BulletSpeed * _direction, 0));
        Destroy(bullet, 2f);
    }

    public void OnMove(InputAction.CallbackContext cb)
    {
        Vector2 movement = cb.ReadValue<Vector2>();
        Movement.Move(movement);
    }
}
