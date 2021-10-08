using UnityEngine;

/// <summary>This is the prototype interface for all melee weapons</summary>
public class Melee : BaseWeapon
{
    public override string Type { get => "Melee"; }

    public Melee(WeaponRegistry registry) : base(registry) { }
}
