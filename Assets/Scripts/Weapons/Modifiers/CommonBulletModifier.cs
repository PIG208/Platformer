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

    public void HandleBulletCollide(object sender, BulletManager.BulletCollideArgs e)
    {
        BulletManager bullet = ((BulletManager)sender);
        if ((e.Other.Group & bullet.Group) == 0)
        {
            Debug.Log("Destroyed");
            GameObject.Destroy(e.Other);
        }
    }

    public void HandleBulletCreate(object sender, Gun.BulletEventArgs e)
    {
        BulletManager bullet = CreateBullet(e.WeaponManager);
        bullet.CollideEntity += HandleBulletCollide;

        e.BulletManagers.Add(bullet);

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(e.WeaponManager.BulletSpeed * e.FireContext.Player.Movement.Direction, 0));
        GameObject.Destroy(bullet, 2);
    }
}
