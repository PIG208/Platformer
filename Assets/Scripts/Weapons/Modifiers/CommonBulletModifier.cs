using System;
using UnityEngine;

class CommonBulletModifier : IModifier<Gun>
{
    public void Register(Gun weapon)
    {
        weapon.BulletCreate += HandleBulletCreate;
    }

    public void HandleBulletCreate(object sender, Gun.BulletEventArgs e)
    {
        GunManager gunManager = e.WeaponManager;

        GameObject bullet = GameObject.Instantiate(gunManager.BulletPrefab, gunManager.BulletSpawn.transform.position, gunManager.transform.rotation);
        BulletManager bulletManager = bullet.GetComponent<BulletManager>();
        e.BulletManagers.Add(bulletManager);

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(gunManager.BulletSpeed * e.FireContext.Player.Movement.Direction, 0));
        GameObject.Destroy(bullet, 2);
    }
}
