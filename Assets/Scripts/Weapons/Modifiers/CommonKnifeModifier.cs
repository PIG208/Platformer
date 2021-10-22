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

    public void HandleMeleeAttack(object weapon, BaseWeapon.FireEventArgs<MeleeManager> e)
    {
        foreach (RaycastHit2D raycast in Physics2D.RaycastAll(
            e.WeaponManager.RaycastEndpointA.position,
            e.WeaponManager.RaycastEndpointB.position - e.WeaponManager.RaycastEndpointA.position,
            Vector2.Distance(e.WeaponManager.RaycastEndpointA.position, e.WeaponManager.RaycastEndpointB.position)
        ))
        {
            Entity entity = raycast.collider.GetComponent<Entity>();
            if (entity != null && e.FireContext.Player.Group.IsHotileTo(entity.Group))
            {
                e.WeaponManager.DealDamage(entity.Health);
            }
        }
    }
}
