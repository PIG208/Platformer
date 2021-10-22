using UnityEngine;

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

    protected RaycastHit2D[] FindHits(BaseWeapon.FireEventArgs<MeleeManager> e)
    {
        return Physics2D.RaycastAll(
            e.WeaponManager.RaycastEndpointA.position,
            e.WeaponManager.RaycastEndpointB.position - e.WeaponManager.RaycastEndpointA.position,
            Vector2.Distance(e.WeaponManager.RaycastEndpointA.position, e.WeaponManager.RaycastEndpointB.position)
        );
    }

    public virtual void HandleMeleeAttack(object weapon, BaseWeapon.FireEventArgs<MeleeManager> e)
    {
        foreach (RaycastHit2D raycast in FindHits(e))
        {
            Entity entity = raycast.collider.GetComponent<Entity>();
            if (entity != null && e.FireContext.Player.Group.IsHotileTo(entity.Group))
            {
                e.WeaponManager.DealDamage(entity.Health);
            }
        }
    }
}
