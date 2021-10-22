using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementManager : MonoBehaviour
{
    public float Speed = 4f;
    public float JumpForce = 100f;
    public int MaxJumps = 1;

    public int Direction { get => _direction; }

    // GroundCheck
    public GameObject GroundSphere;
    public float GroundSphereRadius = 0.3f;
    public LayerMask GroundLayer;
    public bool Grounded { get => _grounded; }

    private Rigidbody2D _rigidbody;
    private bool _grounded = false;
    private float _xSpeed = 0;
    private int _remainingJumps;
    private int _direction = 1;
    private bool _facingRight = true;

    private void Start()
    {
        _remainingJumps = MaxJumps;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.position += new Vector2(_xSpeed * Time.deltaTime, 0);
    }

    public bool GroundCheck()
    {
        if (GroundSphere == null) return true;
        return Physics2D.OverlapCircle(GroundSphere.transform.position, GroundSphereRadius, GroundLayer);
    }

    public void Move(Vector2 movement, bool flipOnly = false)
    {
        _xSpeed = movement.x * Speed;
        if (_xSpeed != 0)
        {
            _direction = (_xSpeed > 0) ? 1 : -1;
        }

        if (_direction != 0 && (_direction > 0 ^ _facingRight))
        {
            Flip();
        }

        _grounded = GroundCheck();

        if (_grounded)
        {
            _remainingJumps = MaxJumps;
        }

        if (movement.y > 0 && _remainingJumps > 0)
        {
            _remainingJumps--;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, JumpForce));
        }

        if (flipOnly)
        {
            Move(Vector2.zero, false);
        }
    }

    public void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(new Vector3(0, 180f, 0));
    }

    public void AddImpluse(Vector2 movement)
    {
        _rigidbody.AddForce(movement);
    }
}
