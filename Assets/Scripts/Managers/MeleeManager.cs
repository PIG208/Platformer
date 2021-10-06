using UnityEngine;

/// <summary>To create a melee weapon, create a prefab for the weapon and drag the MeleeManager to it</summary>
public class MeleeManager : WeaponManager
{
    private void Start()
    {
        this.Weapon = new Melee(Name, Rarity, Power, Slots);
    }
}
