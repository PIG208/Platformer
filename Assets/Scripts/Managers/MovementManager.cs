using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementManager : MonoBehaviour
{
    public float Speed = 4f;
    public float JumpForce = 200f;
    public int MaxJumps = 1;

    // GroundCheck
    public GameObject GroundSphere;
    public float GroundSphereRadius = 0.3f;
    public LayerMask GroundLayer;
    public bool Grounded { get => _grounded; }

    private Rigidbody2D _rigidbody;
    private bool _grounded = false;
    private float _xSpeed = 0;
    private int _remainingJumps;

    private void Start()
    {
        _remainingJumps = MaxJumps;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_xSpeed, _rigidbody.velocity.y);
    }

    public bool GroundCheck()
    {
        if (GroundSphere == null) return true;
        return Physics2D.OverlapCircle(GroundSphere.transform.position, GroundSphereRadius, GroundLayer);
    }

    public void Move(Vector2 movement)
    {
        _xSpeed = movement.x * Speed;

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
    }

    public void AddImpluse(Vector2 movement)
    {
        _rigidbody.AddForce(movement);
    }
}
