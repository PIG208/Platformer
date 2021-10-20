using System;
using UnityEngine;

public enum ExtensionRegistry
{
    None = -1,
    MissileLauncher = 0,
}

public static class ExtensionRegistries
{
    public static bool IsMelee(this ExtensionRegistry extensionRegistry)
    {
        switch (extensionRegistry)
        {
            case ExtensionRegistry.MissileLauncher:
                return true;
            default:
                return false;
        }
    }

    public static bool IsGun(this ExtensionRegistry extensionRegistry)
    {
        switch (extensionRegistry)
        {
            case ExtensionRegistry.MissileLauncher:
                return true;
            default:
                return false;
        }
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
        }

        throw new ArgumentException($"{registry} doesn't exist in the extension registry");
    }
}