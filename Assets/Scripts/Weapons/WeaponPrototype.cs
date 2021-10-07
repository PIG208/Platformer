using System;
using UnityEngine;

/// <summary>Provide a registry for all available weapon types</summary>
public static class WeaponPrototype
{
    public static GameObject GetWeaponPrefabById(string id)
    {
        return Resources.Load<GameObject>($"Weapons/{id}");
    }

    public static string GetWeaponId(WeaponRegistry registry)
    {
        switch (registry)
        {
            case WeaponRegistry.Knife:
                return "Knife";
            case WeaponRegistry.Pistol:
                return "Pistol";
        }
        throw new ArgumentException($"{registry} doesn't exist in the weapon registry");
    }

    public static T GetWeapon<T>(string id) where T : BaseWeapon
    {
        GameObject weaponPrefab = GetWeaponPrefabById(id);
        if (typeof(T) == typeof(Melee))
        {
            return (T)Convert.ChangeType(new Melee(weaponPrefab), typeof(T));
        }
        else if (typeof(T) == typeof(Gun))
        {
            return (T)Convert.ChangeType(new Gun(weaponPrefab), typeof(T));
        }
        throw new ArgumentException($"{id} is not a valid weapon");
    }

    public static T GetWeapon<T>(WeaponRegistry registry) where T : BaseWeapon
    {
        return GetWeapon<T>(GetWeaponId(registry));
    }
}
