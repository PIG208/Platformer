using UnityEngine;

public class JetpackModifier : CommonFireModifer
{
    public float JetpackImpulse = 100f;

    public override void HandleFire(object weapon, BaseWeapon.FireEventArgs<WeaponManager> e)
    {
        base.HandleFire(weapon, e);
        e.FireContext.Player.Movement.AddImpluse(Vector2.up * JetpackImpulse);
    }
}