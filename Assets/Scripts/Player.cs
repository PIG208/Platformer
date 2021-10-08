using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovementManager))]
public class Player : Entity, InputControls.IPlayerActions, IMovable
{
    public float MaxFireInterval = 0.21f;

    private Rigidbody2D _rigidbody;
    private bool _firing;
    private float _firingTimeOut;

    public PlayerHealthBar playerHealthBar;
    private int maxHealth = 100;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _firingTimeOut = MaxFireInterval;
        BindManagers();

        playerHealthBar = GameObject.FindWithTag("HealthBar").GetComponent<PlayerHealthBar>();
        playerHealthBar.setSliderMaxHealth(maxHealth);
        Health.setHealth(maxHealth);
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
            Health.damage(10);
            playerHealthBar.setHealth(Health.getHealth());
            print("Took 5 damage. Current health:" + Health.getHealth());
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

