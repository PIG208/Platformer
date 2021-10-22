using UnityEngine;

class BouncyBulletModifier : CommonBulletModifier
{
    public float TriggerChance = 0.2f;
    public float Speed = 4f;

    public override void HandleBulletCollided(object sender, BulletManager.BulletCollideArgs e)
    {
        BulletManager bullet = ((BulletManager)sender);
        if (bullet.Group.IsHotileTo(e.Other.Group))
        {
            e.Other.GetComponent<HealthManager>().Damage((int)bullet.damage);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), 0.5f).normalized * Speed;
        }
    }

    protected override BulletManager CreateBullet(Gun.BulletEventArgs e)
    {
        BulletManager bullet = null;

        if (Random.Range(0, 1f) < TriggerChance) bullet = e.WeaponManager.SpawnBullet(Constants.BouncyPrefab);

        return bullet;
    }
}
