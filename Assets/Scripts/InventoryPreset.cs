using System;
using System.Collections.Generic;

/// <summary>A collection of weapon presets</summary>
public enum InventoryPreset
{
    Empty,
    Newbee,
    Gunner,
    Missile,
    Teeth,
    Hammer,
    Hunter,
    Debug
}

public static class InventoryPresets
{
    public static List<BaseWeapon> GetItems(this InventoryPreset preset)
    {
        List<BaseWeapon> items = new List<BaseWeapon>();
        switch (preset)
        {
            case InventoryPreset.Empty:
                return items;
            case InventoryPreset.Newbee:
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Knife));
                break;
            case InventoryPreset.Gunner:
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Pistol));
                break;
            case InventoryPreset.Missile:
                Gun launcher = WeaponPrototype.GetWeapon<Gun>(WeaponRegistry.Rifle);
                launcher.RegisterModifier(new MissileModifier());
                items.Add(launcher);
                break;
            case InventoryPreset.Teeth:
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Bite));
                break;
            case InventoryPreset.Hammer:
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Hammer));
                break;
            case InventoryPreset.Hunter:
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Rifle));
                break;
            case InventoryPreset.Debug:
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Knife));
                Gun pistol = WeaponPrototype.GetWeapon<Gun>(WeaponRegistry.Pistol);
                items.Add(pistol);
                pistol.RegisterModifier(new BouncyBulletModifier());
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Rifle));
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Bow));
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Bite));
                items.Add(WeaponPrototype.GetWeapon(WeaponRegistry.Hammer));
                break;
        }
        return items;
    }
}