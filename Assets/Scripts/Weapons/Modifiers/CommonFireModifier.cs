public class CommonFireModifer : IModifier<BaseWeapon>
{
    public void Register(BaseWeapon weapon)
    {
        weapon.Fire += HandleFireAnimation;
    }

    public void HandleFireAnimation(object weapon, BaseWeapon.FireEventArgs<WeaponManager> e)
    {
        if (e.WeaponManager.WeaponAnimator != null)
            e.WeaponManager.WeaponAnimator.Play("Attack");
    }
}
