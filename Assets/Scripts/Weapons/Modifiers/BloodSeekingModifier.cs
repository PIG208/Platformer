using UnityEngine;

public class BloodSeekingModifier : CommonKnifeModifier
{
    public float StealingRate = 0.3f;

    public override void HandleMeleeAttack(object weapon, BaseWeapon.FireEventArgs<MeleeManager> e)
    {
        foreach (RaycastHit2D raycast in FindHits(e))
        {
            Entity entity = raycast.collider.GetComponent<Entity>();
            if (entity != null && e.FireContext.Player.Group.IsHotileTo(entity.Group))
            {
                e.FireContext.Player.Health.Heal(Mathf.FloorToInt(e.WeaponManager.Power * Constants.DamageFactor * StealingRate));
            }
        }
    }
}
