using System.Collections.Generic;
using UnityEngine;

class MissileModifier : CommonBulletModifier
{
    public float SpeedFactor = 0.01f;
    public float AngularSpeedFactor = 1f;

    protected override BulletManager CreateBullet(Gun.BulletEventArgs e)
    {
        BulletManager missile = GameObject.Instantiate(Resources.Load<GameObject>(Constants.MisslePrefab), e.WeaponManager.BulletSpawn.transform.position, e.WeaponManager.transform.rotation).GetComponent<BulletManager>();
        missile.Homing = true;
        missile.Group = e.FireContext.Player.Group;
        missile.Speed = e.WeaponManager.BulletSpeed * SpeedFactor;
        missile.AngularSpeed = e.WeaponManager.BulletSpeed * AngularSpeedFactor;

        // Find the first enemy in the surrounding targets
        using (IEnumerator<Entity> entityEnum = e.FireContext.SurroundingTargets.GetEnumerator())
        {
            if (entityEnum.MoveNext()) missile.Target = entityEnum.Current.gameObject;
            else missile.Target = missile.gameObject;
        }

        return missile;
    }
}
