using UnityEngine;

/// <summary>To create a melee weapon, create a prefab for the weapon and drag the MeleeManager to it</summary>
public class MeleeManager : WeaponManager
{
    public Transform RaycastEndpointA;
    public Transform RaycastEndpointB;
    public AudioClip WeaponSound;
    public AudioSource AudioSource;

    public override void Fire(FireContext fireContext)
    {
        if (!CanFire()) return;

        base.Fire(fireContext);
        // Invoke Fire event on the weapon
        Weapon.RaiseFire(this, fireContext);
        ((Melee)Weapon).RaiseFire(this, fireContext);
        if (AudioSource != null) AudioSource.PlayOneShot(WeaponSound);
    }
}
