using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>This is the prototype interface for all melee weapons</summary>
public class Gun : BaseWeapon
{
    public override string Type { get => "Gun"; }
    public List<IModifier<Gun>> GunModifiers = new List<IModifier<Gun>> { new CommonBulletModifier() };

    /// <summary>Invoked before the bullets are created</summary>
    public event EventHandler<BulletEventArgs> BulletCreate;
    public void RaiseBulletCreate(GunManager gunManager, FireContext fireContext, List<BulletManager> bulletManagers) => BulletCreate?.Invoke(this, new BulletEventArgs(gunManager, fireContext, bulletManagers));
    /// <summary>Invoked after all bullets are created</summary>
    public event EventHandler<BulletEventArgs> BulletCreated;
    public void RaiseBulletCreated(GunManager gunManager, FireContext fireContext, List<BulletManager> bulletManagers) => BulletCreated?.Invoke(this, new BulletEventArgs(gunManager, fireContext, bulletManagers));

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

    public class BulletEventArgs : FireEventArgs<GunManager>
    {
        public List<BulletManager> BulletManagers;

        public BulletEventArgs(GunManager gunManager, FireContext fireContext, List<BulletManager> bulletManagers) : base(gunManager, fireContext)
        {
            BulletManagers = bulletManagers;
        }
    }
}
