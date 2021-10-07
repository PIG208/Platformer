using UnityEngine;

/// <summary>This is the prototype interface for all melee weapons</summary>
public class Gun : BaseWeapon
{
    public override string Type { get => "Gun"; }
    private GameObject _bulletPrefab;

    public Gun(GameObject weaponPrefab) : base(weaponPrefab)
    {
        GunManager gunManager = weaponPrefab.GetComponent<GunManager>();
        _bulletPrefab = gunManager.BulletPrefab;
    }
}
