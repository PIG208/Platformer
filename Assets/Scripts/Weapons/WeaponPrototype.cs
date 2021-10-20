using System;
using UnityEngine;

/// <summary>Provide a registry for all available weapon types</summary>
public static class WeaponPrototype
{
    public static GameObject GetWeaponPrefab(string id)
    {
        return Resources.Load<GameObject>($"Weapons/{id}");
    }

    public static GameObject GetWeaponPrefab(WeaponRegistry registry)
    {
        return GetWeaponPrefab(registry.WeaponId());
    }

    public static T GetWeapon<T>(WeaponRegistry registry) where T : BaseWeapon
    {
        if (typeof(T) == typeof(Melee))
        {
            return (T)Convert.ChangeType(new Melee(registry), typeof(T));
        }
        else if (typeof(T) == typeof(Gun))
        {
            return (T)Convert.ChangeType(new Gun(registry), typeof(T));
        }
        throw new ArgumentException($"{registry} is not a valid weapon");
    }

    public static BaseWeapon GetWeapon(WeaponRegistry registry)
    {
        if (registry.IsGun())
        {
            return new Gun(registry);
        }
        else if (registry.IsMelee())
        {
            return new Melee(registry);
        }
        throw new ArgumentException($"{registry} is not a valid weapon");
    }
}
