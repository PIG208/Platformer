public class CommonFireModifer : IModifier<BaseWeapon>
{
    public void Register(BaseWeapon weapon)
    {
        weapon.Fire += HandleFireAnimation;
    }

    public void HandleFireAnimation(WeaponManager weaponManager, FireContext context)
    {
        if (weaponManager.WeaponAnimator != null)
            weaponManager.WeaponAnimator.Play("Attack");
    }
}
