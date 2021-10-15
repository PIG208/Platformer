using System;
using UnityEngine;

class CommonBulletModifier : IModifier<Gun>
{
    public void Register(Gun weapon)
    {
        weapon.BulletCreate += HandleBulletCreate;
    }

    protected BulletManager CreateBullet(GunManager gunManager)
    {
        GameObject bullet = GameObject.Instantiate(gunManager.BulletPrefab, gunManager.BulletSpawn.transform.position, gunManager.transform.rotation);
        return bullet.GetComponent<BulletManager>();
    }

    public void HandleBulletCollided(object sender, BulletManager.BulletCollideArgs e)
    {
        BulletManager bullet = ((BulletManager)sender);
        if ((e.Other.Group & bullet.Group) == 0)
        {
            e.Other.GetComponent<HealthManager>().Damage((int)bullet.damage);
            GameObject.Destroy(bullet.gameObject);
        }
    }

    public void HandleBulletCreate(object sender, Gun.BulletEventArgs e)
    {
        BulletManager bullet = CreateBullet(e.WeaponManager);
        bullet.CollidedEntity += HandleBulletCollided;

        e.BulletManagers.Add(bullet);

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(e.WeaponManager.BulletSpeed * e.FireContext.Player.Movement.Direction, 0));
    }
}
