using UnityEngine;

class CommonBulletModifier : IModifier<Gun>
{
    public void Register(Gun weapon)
    {
        weapon.BulletCreated += HandleBulletCreated;
    }

    public void HandleBulletCreated(Gun gun, BulletManager[] managers)
    {
        Debug.Log("Fired bullet");
        Debug.Log(managers[0].damage);
    }
}
