using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>This is the prototype interface for all melee weapons</summary>
public class Melee : BaseWeapon, Modifiable<Melee>
{
    public override string Type { get => "Melee"; }
    public List<IModifier<Melee>> MeleeModifiers = new List<IModifier<Melee>>() { new CommonKnifeModifier() };

    public Melee(WeaponRegistry registry) : base(registry)
    {
        foreach (IModifier<Melee> meleeModifiers in MeleeModifiers)
        {
            meleeModifiers.Register(this);
        }
    }

    public void RegisterModifier(IModifier<Melee> modifier)
    {
        MeleeModifiers.Add(modifier);
        modifier.Register(this);
    }

    public event EventHandler<FireEventArgs<MeleeManager>> MeleeAttack;
    public void RaiseFire(MeleeManager meleeManager, FireContext context) => MeleeAttack?.Invoke(this, new FireEventArgs<MeleeManager>(meleeManager, context));
}
