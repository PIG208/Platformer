using System;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float damage;
    /// <summary>Whether the bullet is friendly to the player or not</summary>
    public Group Group;
    public GameObject Target;
    public bool Homing = false;
    public float LifeTime = 2f;

    public event EventHandler<BulletCollideArgs> CollideEntity;
    public event EventHandler<BulletCollideArgs> CollidedEntity;

    private void Start()
    {
        Destroy(this.gameObject, LifeTime);
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