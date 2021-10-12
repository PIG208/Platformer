using UnityEngine;

/// <summary>To create a melee weapon, create a prefab for the weapon and drag the MeleeManager to it</summary>
public class MeleeManager : WeaponManager
{
    public override void Fire(FireContext fireContext)
    {
        base.Fire(fireContext);
        // Invoke Fire event on the weapon
        Weapon.RaiseFire(this, fireContext);
    }
}
