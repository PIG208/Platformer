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
        return GetWeaponPrefab(GetWeaponId(registry));
    }

    public static string GetWeaponId(WeaponRegistry registry)
    {
        switch (registry)
        {
            case WeaponRegistry.Knife:
                return "Knife";
            case WeaponRegistry.Pistol:
                return "Pistol";
            case WeaponRegistry.Rifle:
                return "Rifle";
            case WeaponRegistry.Bow:
                return "Bow";
        }
        throw new ArgumentException($"{registry} doesn't exist in the weapon registry");
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
}
