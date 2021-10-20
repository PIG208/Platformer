using System;
using UnityEngine;

class CommonBulletModifier : IModifier<Gun>
{
    public void Register(Gun weapon)
    {
        weapon.BulletCreate += HandleBulletCreate;
    }

    protected virtual BulletManager CreateBullet(Gun.BulletEventArgs e)
    {
        GameObject bullet = GameObject.Instantiate(e.WeaponManager.BulletPrefab, e.WeaponManager.BulletSpawn.transform.position, e.WeaponManager.transform.rotation);
        return bullet.GetComponent<BulletManager>();
    }

    public void HandleBulletCollided(object sender, BulletManager.BulletCollideArgs e)
    {
        BulletManager bullet = ((BulletManager)sender);
        if (bullet.Group.IsHotileTo(e.Other.Group))
        {
            e.Other.GetComponent<HealthManager>().Damage((int)bullet.damage);
            GameObject.Destroy(bullet.gameObject);
        }
    }

    public void HandleBulletCreate(object sender, Gun.BulletEventArgs e)
    {
        BulletManager bullet = CreateBullet(e);
        bullet.CollidedEntity += HandleBulletCollided;

        e.BulletManagers.Add(bullet);

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(e.WeaponManager.BulletSpeed * e.FireContext.Player.Movement.Direction, 0));
    }
}
