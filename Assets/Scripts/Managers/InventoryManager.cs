using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform EquipementPosition;
    public WeaponManager CurrentWeapon;

    private void Start()
    {
        CurrentWeapon = GetGun();
    }

    public WeaponManager GetGun()
    {
        Gun weapon = WeaponPrototype.GetWeapon<Gun>(WeaponRegistry.Pistol);
        return Instantiate(weapon.WeaponPrefab, EquipementPosition.position, EquipementPosition.rotation, EquipementPosition).GetComponent<WeaponManager>();
    }
}
