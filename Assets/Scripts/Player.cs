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
    public override Group Group { get => Group.FriendlyToPlayer; }

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

        EntityAnimator.SetInteger("Speed", Movement.Direction * (int)movement.x);
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

    private void Update()
    {
        if (_firingTimeOut > 0)
        {
            _firingTimeOut -= Time.deltaTime;
        }

        if (_firing && _firingTimeOut <= 0)
        {
            _firingTimeOut = MaxFireInterval;
            this.Inventory.CurrentWeaponManager.Fire(new FireContext(this, new Entity[] { }));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            this.Health.Damage(10);
            if (Health.Health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}

