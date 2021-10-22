using UnityEngine;

public class RepulseModifier : CommonKnifeModifier
{
    public float RepulseForce = 400f;

    public override void HandleMeleeAttack(object weapon, BaseWeapon.FireEventArgs<MeleeManager> e)
    {
        foreach (RaycastHit2D raycast in FindHits(e))
        {
            Entity entity = raycast.collider.GetComponent<Entity>();
            if (entity != null && e.FireContext.Player.Group.IsHotileTo(entity.Group))
            {
                entity.Movement.AddImpluse((entity.transform.position - e.FireContext.Player.transform.position).normalized * RepulseForce);
            }
        }
    }
}
