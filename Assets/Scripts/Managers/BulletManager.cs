using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletManager : MonoBehaviour
{
    public float damage;
    /// <summary>Whether the bullet is friendly to the player or not</summary>
    public Group Group;
    public GameObject Target;
    public bool Homing = false;
    public float LifeTime = 2f;
    public float Speed;
    public float AngularSpeed;

    public event EventHandler<BulletCollideArgs> CollideEntity;
    public event EventHandler<BulletCollideArgs> CollidedEntity;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, LifeTime);
    }

    private void FixedUpdate()
    {
        if (Target != null && Homing)
        {
            float rotateAmount = Vector3.Cross(((Vector2)Target.transform.position - _rb.position).normalized, transform.right).z;
            _rb.angularVelocity = -AngularSpeed * rotateAmount;
            _rb.velocity = transform.right * Speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        if (otherEntity != null)
        {
            CollideEntity?.Invoke(this, new BulletCollideArgs(otherEntity));
            CollidedEntity?.Invoke(this, new BulletCollideArgs(otherEntity));
        }
    }

    public class BulletCollideArgs : EventArgs
    {
        public Entity Other;

        public BulletCollideArgs(Entity other)
        {
            Other = other;
        }
    }
}