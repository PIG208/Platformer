using UnityEngine;

/// <summary>This is the prototype interface for all melee weapons</summary>
public class Gun : BaseWeapon
{
    public override string Type { get => "Gun"; }

    public Gun(WeaponRegistry registry) : base(registry)
    {
        GunManager gunManager = WeaponPrefab.GetComponent<GunManager>();
    }
}
