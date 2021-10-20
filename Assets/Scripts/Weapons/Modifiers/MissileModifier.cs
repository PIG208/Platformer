using System.Collections.Generic;
using UnityEngine;

class MissileModifier : CommonBulletModifier
{
    protected override BulletManager CreateBullet(Gun.BulletEventArgs e)
    {
        BulletManager missile = GameObject.Instantiate(Resources.Load<GameObject>(Constants.MisslePrefab), e.WeaponManager.BulletSpawn.transform.position, e.WeaponManager.transform.rotation).GetComponent<BulletManager>();
        missile.Homing = true;
        missile.Group = e.FireContext.Player.Group;
        missile.Speed = e.WeaponManager.BulletSpeed * 0.1f;
        missile.AngularSpeed = e.WeaponManager.BulletSpeed;

        // Find the first enemy in the surrounding targets
        using (IEnumerator<Entity> entityEnum = e.FireContext.SurroundingTargets.GetEnumerator())
        {
            if (entityEnum.MoveNext()) missile.Target = entityEnum.Current.gameObject;
            else missile.Target = missile.gameObject;
        }

        return missile;
    }
}
