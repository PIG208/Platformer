class BigBulletsModifier : IModifier<Gun>
{
    public void Register(Gun weapon)
    {
        weapon.BulletCreated += HandleBulletCreated;
    }

    public void HandleBulletCreated(object sender, Gun.BulletEventArgs e)
    {
        foreach (BulletManager bullet in e.BulletManagers)
        {
            bullet.gameObject.transform.localScale *= 2;
        }
    }
}