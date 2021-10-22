using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovementManager))]
[RequireComponent(typeof(HealthManager))]
public class Player : Entity, InputControls.IPlayerActions, IMovable
{
    public float MaxFireInterval = Constants.BaseAttackInterval;
    public override Group Group => Group.Friendly;
    public override bool IsPlayer => true;

    private Rigidbody2D _rigidbody;
    private bool _firing;
    private float _firingTimeout;
    private float _pickupTimeout = Constants.PickupInterval;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _firingTimeout = MaxFireInterval;
        BindManagers();
        Inventory.PickupEnabled = true;
        Health.Died += HandleDied;
    }

    public void OnFire(InputAction.CallbackContext cb)
    {
        _firing = cb.performed;
    }

    public void OnMove(InputAction.CallbackContext cb)
    {
        Vector2 movement = cb.ReadValue<Vector2>();
        Movement.Move(movement);

        EntityAnimator.SetInteger("Speed", Movement.Direction * (int)movement.x);
    }

    public void OnPickup(InputAction.CallbackContext cb)
    {
        if (_pickupTimeout > 0) return;
        _pickupTimeout = Constants.PickupInterval;

        Inventory.TryPickup();
    }

    public void OnSwitchWeapon(InputAction.CallbackContext cb)
    {
        bool scrollUp = (cb.ReadValue<Vector2>().y > 0);
        if (scrollUp)
            Inventory.PrevWeapon();
        else
            Inventory.NextWeapon();
    }

    public void OnSwitchWeapon2(InputAction.CallbackContext cb)
    {
        Inventory.SwitchWeapon(int.Parse(cb.control.name) - 1);
    }

    public void OnDash(InputAction.CallbackContext cb)
    {
        GetComponent<Dash>().HandleDash();
    }

    public void OnTimestop(InputAction.CallbackContext cb)
    {
        GetComponent<Timestop>().HandleToggle();
    }

    public void HandleDied(HealthManager healthManager)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        healthManager.Respawn();
    }

    private void Update()
    {
        if (PublicVars.paused) return;
        if (_firingTimeout > 0)
        {
            _firingTimeout -= Time.deltaTime;
        }

        if (_pickupTimeout > 0)
        {
            _pickupTimeout -= Time.deltaTime;
        }

        if (_firing && _firingTimeout <= 0)
        {
            _firingTimeout = MaxFireInterval;
            Fire();
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -30)
        {
            Health.Damage((int)(Health.MaxHealth * 0.3));
        }
    }
}

