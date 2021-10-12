using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>This is the prototype interface for all melee weapons</summary>
public class Gun : BaseWeapon
{
    public override string Type { get => "Gun"; }
    public event Action<Gun, BulletManager[]> BulletCreated;
    public void RaiseBulletCreated(Gun gun, BulletManager[] manager) => BulletCreated.Invoke(gun, manager);
    public List<IModifier<Gun>> GunModifiers = new List<IModifier<Gun>> { new CommonBulletModifier() };

    public Gun(WeaponRegistry registry) : base(registry)
    {
        foreach (IModifier<Gun> gunModifier in GunModifiers)
        {
            gunModifier.Register(this);
        }
    }

    public void RegisterModifier(IModifier<Gun> modifier)
    {
        GunModifiers.Add(modifier);
        modifier.Register(this);
    }
}
