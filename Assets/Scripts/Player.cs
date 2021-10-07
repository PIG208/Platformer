using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementManager))]
public class Player : Entity, InputControls.IPlayerActions, IMovable
{
    public float MaxFireInterval = 0.21f;

    private Rigidbody2D _rigidbody;
    private bool _firing;
    private float _firingTimeOut;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _firingTimeOut = MaxFireInterval;
        BindManagers();
    }

    public void OnFire(InputAction.CallbackContext cb)
    {
        _firing = cb.performed;
    }

    public void OnMove(InputAction.CallbackContext cb)
    {
        Vector2 movement = cb.ReadValue<Vector2>();
        Movement.Move(movement);

        Vector3 scale = this.transform.localScale;
        this.transform.localScale = new Vector3(Movement.Direction, scale.y, scale.z);
    }

    private void Update()
    {
        if (_firingTimeOut > 0)
        {
            _firingTimeOut -= Time.deltaTime;
        }

        if (_firing && _firingTimeOut <= 0)
        {
            _firingTimeOut = MaxFireInterval;
            this.Inventory.CurrentWeapon.Fire(new WeaponManager.FireContext(this, new Entity[] { }));
        }
    }
}
