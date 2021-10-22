using System;
using UnityEngine;

public enum ExtensionRegistry
{
    None = -1,
    MissileLauncher = 0,
    BouncyBullet = 1,
    Jetpack = 2,
}

public static class ExtensionRegistries
{
    public static bool IsMelee(this ExtensionRegistry extensionRegistry)
    {
        switch (extensionRegistry)
        {
            default:
                return false;
        }
    }

    public static bool IsGun(this ExtensionRegistry extensionRegistry)
    {
        switch (extensionRegistry)
        {
            case ExtensionRegistry.BouncyBullet:
            case ExtensionRegistry.MissileLauncher:
                return true;
            default:
                return false;
        }
    }

    public static IModifier<T> Modifier<T>(this ExtensionRegistry registry) where T : BaseWeapon
    {
        if ((typeof(T) == typeof(Gun) && !registry.IsGun()) || (typeof(T) == typeof(Melee) && !registry.IsMelee()))
            throw new ArgumentException($"{typeof(T)} does not match the type of {registry}");

        switch (registry)
        {
            case ExtensionRegistry.MissileLauncher:
                return (IModifier<T>)new MissileModifier();
            case ExtensionRegistry.BouncyBullet:
                return (IModifier<T>)new BouncyBulletModifier();
            case ExtensionRegistry.Jetpack:
                return (IModifier<T>)new JetpackModifier();
        }
        throw new ArgumentException($"{registry} does not have a valid modifier");
    }

    public static GameObject ExtensionPrefab(this ExtensionRegistry id)
    {
        return Resources.Load<GameObject>($"Weapons/Extensions/{id}");
    }

    public static string ExtensionId(this ExtensionRegistry registry)
    {
        switch (registry)
        {
            case ExtensionRegistry.MissileLauncher:
                return "MissileLauncher";
            default:
                return registry.ToString();
        }

        throw new ArgumentException($"{registry} doesn't exist in the extension registry");
    }

    public static string Description(this ExtensionRegistry registry)
    {
        switch (registry)
        {
            case ExtensionRegistry.MissileLauncher:
                return "Fire an additional missile tracking the nearest enemy";
        }

        return "???";
    }
}