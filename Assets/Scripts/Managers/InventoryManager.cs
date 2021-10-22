using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform EquipementPosition;
    public WeaponManager CurrentWeaponManager;
    public List<BaseWeapon> Weapons;
    public List<ExtensionRegistry> Extensions;
    public float SwitchInterval = Constants.SwitchWeaponInterval;
    [Tooltip("The weapons this inventory manager will start with")]
    public InventoryPreset Preset = InventoryPreset.Empty;

    [System.NonSerialized]
    public List<CollectableManager> SurroundingCollectables = new List<CollectableManager>();
    public Text PickupText;
    public bool PickupEnabled = false;

    private int _currentWeaponIndex = -1;
    private float _lastSwitch;

    public event Action<BaseWeapon, WeaponManager> WeaponSwitched;

    private void Start()
    {
        Weapons = Preset.GetItems();

        NextWeapon();
    }

    private void Update()
    {
        if (_lastSwitch > 0) _lastSwitch -= Time.deltaTime;
    }

    private CollectableManager getFirstCollectable()
    {
        return SurroundingCollectables.Find(
            collectable => !collectable.IsExtension || (
                collectable.Extension.IsGun() && CurrentWeaponManager.Weapon.Type == "Gun"
            ) || (
                collectable.Extension.IsMelee() && CurrentWeaponManager.Weapon.Type == "Melee"
            ) || (
                !collectable.Extension.IsGun() && !collectable.Extension.IsMelee()
            )
        );
    }

    private void UpdateUI()
    {
        CollectableManager collectable = getFirstCollectable();
        if (collectable != null)
        {
            PickupText.enabled = true;
            if (collectable.IsExtension)
            {
                PickupText.text = $"[E] Install {collectable.Name} on {CurrentWeaponManager.Name}";
            }
            else
            {
                PickupText.text = $"[E] Pick up {collectable.Name}";
            }
        }
        else
        {
            PickupText.enabled = false;
        }
    }

    public void AddCanPickup(CollectableManager collectableManager)
    {
        SurroundingCollectables.Add(collectableManager);
        UpdateUI();
    }

    public void RemoveCanPickup(CollectableManager collectableManager)
    {
        SurroundingCollectables.Remove(collectableManager);
        UpdateUI();
    }

    /// <summary>Will attempt to pick up the first surrounding collectables</summary>
    public void TryPickup()
    {
        CollectableManager collectable = getFirstCollectable();
        if (collectable == null) return;

        SurroundingCollectables.Remove(collectable);
        if (collectable.IsExtension)
        {
            if (collectable.Extension == ExtensionRegistry.None) throw new InvalidOperationException("Cannot pick up a null extension");
            Extensions.Add(collectable.Extension);
            if (collectable.Extension.IsGun())
            {
                ((Gun)CurrentWeaponManager.Weapon).RegisterModifier(collectable.Extension.Modifier<Gun>());
            }
            else if (collectable.Extension.IsMelee())
            {
                ((Melee)CurrentWeaponManager.Weapon).RegisterModifier(collectable.Extension.Modifier<Melee>());
            }
            else
            {
                CurrentWeaponManager.Weapon.RegisterModifier(collectable.Extension.Modifier<BaseWeapon>());
            }
        }
        else
        {
            if (collectable.Weapon == null) throw new InvalidOperationException("Cannot pick up a null weapon");
            Weapons.Add(collectable.Weapon);
            SwitchWeapon(Weapons.Count - 1);
        }
        Destroy(collectable.gameObject);
        UpdateUI();
    }

    public void SwitchWeapon(int index)
    {
        if (_lastSwitch > 0 || Weapons.Count == 0) return;
        _lastSwitch = SwitchInterval;

        if (CurrentWeaponManager != null) Destroy(CurrentWeaponManager.gameObject);

        _currentWeaponIndex = index % Weapons.Count;
        if (_currentWeaponIndex < 0) _currentWeaponIndex = Weapons.Count + _currentWeaponIndex;

        CurrentWeaponManager = Weapons[_currentWeaponIndex].Spawn(EquipementPosition);
        UpdateUI();

        if (WeaponSwitched != null) WeaponSwitched(Weapons[_currentWeaponIndex], CurrentWeaponManager);
    }

    public void NextWeapon() => SwitchWeapon(_currentWeaponIndex + 1);
    public void PrevWeapon() => SwitchWeapon(_currentWeaponIndex - 1);
}
