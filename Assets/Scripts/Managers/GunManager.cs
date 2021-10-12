using System.Collections.Generic;
using UnityEngine;

/// <summary>To create a melee weapon, create a prefab for the weapon and drag the GunManager to it</summary>
public class GunManager : WeaponManager
{
    public GameObject BulletPrefab;
    public GameObject BulletSpawn;
    public float BulletSpeed;

    public override void Fire(FireContext fireContext)
    {
        base.Fire(fireContext);
        // Invoke Fire event on the weapon
        Weapon.RaiseFire(this, fireContext);

        List<BulletManager> bulletManagers = new List<BulletManager>();

        GetWeapon<Gun>().RaiseBulletCreate(this, fireContext, bulletManagers);
        GetWeapon<Gun>().RaiseBulletCreated(this, fireContext, bulletManagers);
    }
}
