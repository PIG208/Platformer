using System.Collections.Generic;
using UnityEngine;

class MissileModifier : IModifier<Gun>
{
    public void Register(Gun weapon)
    {
        weapon.BulletCreate += HandleMissileCreate;
    }

    public void HandleMissileCollided(object sender, BulletManager.BulletCollideArgs e)
    {
        BulletManager bullet = ((BulletManager)sender);

        GameObject.Destroy(bullet.gameObject);
        e.Other.GetComponent<HealthManager>().Damage((int)bullet.damage);
        if ((e.Other.Group & bullet.Group) == 0)
        {
            GameObject.Destroy(bullet.gameObject);
        }
    }

    public void HandleMissileCreate(object sender, Gun.BulletEventArgs e)
    {
        BulletManager missile = GameObject.Instantiate(Resources.Load<GameObject>(Constants.MisslePrefab), e.WeaponManager.BulletSpawn.transform.position, e.WeaponManager.transform.rotation).GetComponent<BulletManager>();
        missile.Homing = true;
        missile.CollidedEntity += HandleMissileCollided;
        missile.Group = e.FireContext.Player.Group;
        missile.Speed = e.WeaponManager.BulletSpeed * 0.1f;
        missile.AngularSpeed = e.WeaponManager.BulletSpeed;

        e.BulletManagers.Add(missile);
        using (IEnumerator<Entity> entityEnum = e.FireContext.SurroundingTargets.GetEnumerator())
        {
            if (entityEnum.MoveNext()) missile.Target = entityEnum.Current.gameObject;
            else missile.Target = missile.gameObject;
        }

        /// Create a missile aiming upwards
        missile.GetComponent<Rigidbody2D>().AddForce(new Vector2(e.FireContext.Player.Movement.Direction, 2).normalized * e.WeaponManager.BulletSpeed * 0.5f);
    }
}
