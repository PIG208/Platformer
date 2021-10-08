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
        EntityAnimator.SetInteger("Speed", Movement.Direction * (int)movement.x);
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

