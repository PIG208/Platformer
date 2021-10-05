using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 4f;
    public float JumpForce = 200f;

    public GameObject BulletPrefab;
    public float BulletSpeed = 10f;

    // GroundCheck
    public GameObject GroundSphere;
    public float GroundSphereRadius = 0.3f;
    public LayerMask GroundLayer;
    public bool Grounded { get => _grounded; }

    private Rigidbody2D _rigidbody;
    private bool _grounded = false;
    private int _direction = 1;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * Speed;
        _direction = (xSpeed > 0) ? 1 : -1;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
    }

    private void Update()
    {
        _grounded = GroundCheck();

        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, JumpForce));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(BulletSpeed * _direction, 0));
            Destroy(bullet, 2f);
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(GroundSphere.transform.position, GroundSphereRadius, GroundLayer);
    }
}
