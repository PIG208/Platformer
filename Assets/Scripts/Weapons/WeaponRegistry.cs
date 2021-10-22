using System;

public enum WeaponRegistry
{
    None = -1,
    Knife = 0,
    Pistol = 1,
    Rifle = 2,
    Bow = 3,
    Bite = 4,
    Hammer = 5,
}

public static class WeaponRegistries
{
    public static bool IsMelee(this WeaponRegistry weaponRegistry)
    {
        switch (weaponRegistry)
        {
            case WeaponRegistry.Knife:
            case WeaponRegistry.Bite:
            case WeaponRegistry.Hammer:
                return true;
            default:
                return false;
        }
    }

    public static bool IsGun(this WeaponRegistry weaponRegistry)
    {
        switch (weaponRegistry)
        {
            case WeaponRegistry.Bow:
            case WeaponRegistry.Pistol:
            case WeaponRegistry.Rifle:
                return true;
            default:
                return false;
        }
    }

    public static string WeaponId(this WeaponRegistry registry)
    {
        switch (registry)
        {
            default:
                return registry.ToString();
        }

        throw new ArgumentException($"{registry} doesn't exist in the weapon registry");
    }
}