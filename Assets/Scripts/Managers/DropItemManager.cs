using System;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    public DropEntry[] entries;

    private void Start()
    {
        GetComponent<HealthManager>().Die += HandleDrop;
    }

    public void HandleDrop(HealthManager manager)
    {
        foreach (DropEntry entry in entries)
        {
            float rand = UnityEngine.Random.Range(0f, 1f);
            if (rand < entry.DropChance)
            {
                BaseWeapon weapon = WeaponPrototype.GetWeapon(entry.WeaponRegistry);
                WeaponManager weaponManager = weapon.Spawn(transform.position, transform.rotation);
                Debug.Log($"Spawned {weaponManager}");

                foreach (ExtensionRegistry extensionRegistry in entry.Extensions)
                {
                    if ((extensionRegistry.IsGun() && weapon.GetType() != typeof(Gun)) || (extensionRegistry.IsMelee() && weapon.GetType() != typeof(Melee)))
                        throw new InvalidOperationException($"The weapon type of {extensionRegistry} does not match the weapon type of {weapon.GetType()}");

                    if (extensionRegistry.IsGun())
                    {
                        ((Gun)weapon).RegisterModifier(extensionRegistry.Modifier<Gun>());
                    }
                    else if (extensionRegistry.IsMelee())
                    {
                        ((Melee)weapon).RegisterModifier(extensionRegistry.Modifier<Melee>());
                    }
                    else
                    {
                        // The extension is applicable to BaseWeapon
                        weapon.RegisterModifier(extensionRegistry.Modifier<BaseWeapon>());
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class DropEntry
    {
        public int Count = 1;
        [Range(0, 1)]
        public float DropChance;
        public WeaponRegistry WeaponRegistry;
        public ExtensionRegistry[] Extensions;
    }
}