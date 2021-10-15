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

    public event EventHandler<BulletCollideArgs> CollideEntity;
    public event EventHandler<BulletCollideArgs> CollidedEntity;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, LifeTime);
    }

    private void Update()
    {
        if (Target != null)
        {
            _rb.AddForce((Vector2)(Target.transform.position - transform.position) * Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        CollideEntity?.Invoke(this, new BulletCollideArgs(otherEntity));
        if (otherEntity != null)
        {
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