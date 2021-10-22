public class CommonFireModifer : IModifier<BaseWeapon>
{
    public void Register(BaseWeapon weapon)
    {
        weapon.Fire += HandleFire;
    }

    public virtual void HandleFire(object weapon, BaseWeapon.FireEventArgs<WeaponManager> e)
    {
        if (e.WeaponManager.WeaponAnimator != null)
            e.WeaponManager.WeaponAnimator.Play("Attack");
    }
}
