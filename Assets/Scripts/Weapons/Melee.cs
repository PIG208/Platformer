using UnityEngine;

/// <summary>This is the prototype interface for all weapons</summary>
public class Melee : BaseWeapon
{
    public override string Type { get => "Melee"; }

    public Melee(string name, Rarity rarity, float power, GameObject[] slots) : base(name, rarity, power, slots) { }
    public override void OnFire() { }
}
