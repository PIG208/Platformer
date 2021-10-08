using UnityEngine;

/// <summary>To create a melee weapon, create a prefab for the weapon and drag the GunManager to it</summary>
public class GunManager : WeaponManager
{
    public GameObject BulletPrefab;
    public GameObject BulletSpawn;
    public float BulletSpeed;

    public override void Fire(FireContext fireContext)
    {
        // Invoke Fire event on the weapon
        Weapon.RaiseFire(this, fireContext);

        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(BulletSpeed * fireContext.Player.Movement.Direction, 0));
        Destroy(bullet, 2f);
    }
}
