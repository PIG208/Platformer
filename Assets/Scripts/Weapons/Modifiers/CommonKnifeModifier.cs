public class CommonKnifeModifier : IModifier<Melee>
{
    public void Register(Melee weapon)
    {
        weapon.Update += HandleUpdateDamage;
        weapon.MeleeAttack += HandleMeleeAttack;
    }

    public void HandleUpdateDamage(object sender, WeaponManager manager)
    {

    }

    public void HandleMeleeAttack(object weapon, BaseWeapon.FireEventArgs<MeleeManager> e)
    {
        if (e.WeaponManager.WeaponAnimator != null)
            e.WeaponManager.WeaponAnimator.Play("Attack");
    }
}
